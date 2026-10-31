using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureDataBackupSystem
{
    /// <summary>
    /// Stores one random AES-256 key as a DPAPI-protected blob for the current
    /// Windows user. The raw key is only exposed inside a callback and cleared
    /// from this service's buffer when the callback finishes.
    /// </summary>
    public static class KeyProtectionService
    {
        private const int KeySizeBytes = 32;
        private const string KeyFileName = "client-key.dpapi";
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes(
            "SecureDataBackupSystem:ClientKey:v1");

        /// <summary>
        /// Runs an operation with the local key and clears the service-owned
        /// key buffer after the operation completes.
        /// </summary>
        public static T UseOrCreateKey<T>(Func<byte[], T> operation)
        {
            return UseKey(operation, true);
        }

        public static T UseExistingKey<T>(Func<byte[], T> operation)
        {
            return UseKey(operation, false);
        }

        private static T UseKey<T>(Func<byte[], T> operation, bool createIfMissing)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            byte[] key = createIfMissing
                ? LoadOrCreateKey()
                : LoadExistingKey();
            try
            {
                return operation(key);
            }
            finally
            {
                Array.Clear(key, 0, key.Length);
            }
        }

        private static string GetKeyPath()
        {
            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SecureDataBackupSystem");
            return Path.Combine(directory, KeyFileName);
        }

        private static byte[] LoadExistingKey()
        {
            string keyPath = GetKeyPath();
            if (!File.Exists(keyPath))
                throw new FileNotFoundException(
                    "No protected client key exists for this Windows user.", keyPath);

            return UnprotectKey(File.ReadAllBytes(keyPath));
        }

        private static byte[] LoadOrCreateKey()
        {
            string keyPath = GetKeyPath();
            if (File.Exists(keyPath))
                return UnprotectKey(File.ReadAllBytes(keyPath));

            string directory = Path.GetDirectoryName(keyPath);
            Directory.CreateDirectory(directory);
            byte[] key = new byte[KeySizeBytes];
            byte[] protectedKey = null;
            string temporaryPath = null;
            bool saved = false;

            try
            {
                using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                    random.GetBytes(key);

                protectedKey = ProtectedData.Protect(
                    key, Entropy, DataProtectionScope.CurrentUser);
                temporaryPath = keyPath + "." +
                    Guid.NewGuid().ToString("N") + ".tmp";

                using (FileStream output = new FileStream(
                    temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    output.Write(protectedKey, 0, protectedKey.Length);
                    output.Flush();
                }

                try
                {
                    File.Move(temporaryPath, keyPath);
                    saved = true;
                    return key;
                }
                catch (IOException)
                {
                    // Another process may have initialized the key first.
                    if (!File.Exists(keyPath))
                        throw;

                    Array.Clear(key, 0, key.Length);
                    saved = true;
                    return UnprotectKey(File.ReadAllBytes(keyPath));
                }
            }
            finally
            {
                if (temporaryPath != null && File.Exists(temporaryPath))
                    File.Delete(temporaryPath);
                if (!saved)
                    Array.Clear(key, 0, key.Length);
                if (protectedKey != null)
                    Array.Clear(protectedKey, 0, protectedKey.Length);
            }
        }

        private static byte[] UnprotectKey(byte[] protectedKey)
        {
            try
            {
                byte[] key = ProtectedData.Unprotect(
                    protectedKey, Entropy, DataProtectionScope.CurrentUser);
                if (key.Length != KeySizeBytes)
                {
                    Array.Clear(key, 0, key.Length);
                    throw new CryptographicException(
                        "The protected client key has an invalid length.");
                }
                return key;
            }
            finally
            {
                Array.Clear(protectedKey, 0, protectedKey.Length);
            }
        }
    }
}
