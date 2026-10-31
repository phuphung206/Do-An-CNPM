using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace SecureDataBackupSystem
{
 
    public static class EncryptionService
    {
        private static readonly byte[] Magic = { (byte)'S', (byte)'D', (byte)'B', (byte)'E' };
        private const byte FormatVersion = 1;
        private const int KeySizeBytes = 32;
        private const int NonceSizeBytes = 12;
        private const int TagSizeBytes = 16;
        private const int HeaderSizeBytes = 5 + NonceSizeBytes;
        public const int MaxChunkSizeBytes = 4 * 1024 * 1024;

        public static byte[] EncryptChunk(
            byte[] plaintext,
            byte[] key,
            byte[] associatedData)
        {
            ValidateKey(key);
            if (plaintext == null)
                throw new ArgumentNullException("plaintext");
            if (plaintext.Length > MaxChunkSizeBytes)
                throw new ArgumentOutOfRangeException(
                    "plaintext", "A chunk cannot exceed 4 MiB.");

            byte[] nonce = new byte[NonceSizeBytes];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(nonce);

            GcmBlockCipher cipher = CreateCipher(true, key, nonce, associatedData);
            byte[] encrypted = new byte[cipher.GetOutputSize(plaintext.Length)];
            int written = cipher.ProcessBytes(
                plaintext, 0, plaintext.Length, encrypted, 0);
            written += cipher.DoFinal(encrypted, written);
            if (written != encrypted.Length)
                Array.Resize(ref encrypted, written);

            byte[] envelope = new byte[HeaderSizeBytes + encrypted.Length];
            Buffer.BlockCopy(Magic, 0, envelope, 0, Magic.Length);
            envelope[4] = FormatVersion;
            Buffer.BlockCopy(nonce, 0, envelope, 5, nonce.Length);
            Buffer.BlockCopy(
                encrypted, 0, envelope, HeaderSizeBytes, encrypted.Length);
            return envelope;
        }

        public static byte[] DecryptChunk(
            byte[] envelope,
            byte[] key,
            byte[] associatedData)
        {
            ValidateKey(key);
            if (envelope == null)
                throw new ArgumentNullException("envelope");
            if (envelope.Length < HeaderSizeBytes + TagSizeBytes ||
                envelope.Length > HeaderSizeBytes + MaxChunkSizeBytes + TagSizeBytes)
            {
                throw new InvalidDataException("Encrypted chunk has an invalid length.");
            }

            for (int i = 0; i < Magic.Length; i++)
            {
                if (envelope[i] != Magic[i])
                    throw new InvalidDataException("Encrypted chunk header is invalid.");
            }
            if (envelope[4] != FormatVersion)
                throw new InvalidDataException("Encrypted chunk version is not supported.");

            byte[] nonce = new byte[NonceSizeBytes];
            Buffer.BlockCopy(envelope, 5, nonce, 0, nonce.Length);
            int encryptedLength = envelope.Length - HeaderSizeBytes;
            byte[] encrypted = new byte[encryptedLength];
            Buffer.BlockCopy(
                envelope, HeaderSizeBytes, encrypted, 0, encryptedLength);

            GcmBlockCipher cipher = CreateCipher(false, key, nonce, associatedData);
            byte[] plaintext = new byte[cipher.GetOutputSize(encrypted.Length)];
            bool authenticated = false;
            try
            {
                int written = cipher.ProcessBytes(
                    encrypted, 0, encrypted.Length, plaintext, 0);
                written += cipher.DoFinal(plaintext, written);
                if (written != plaintext.Length)
                    Array.Resize(ref plaintext, written);
                authenticated = true;
                return plaintext;
            }
            catch (InvalidCipherTextException ex)
            {
                throw new CryptographicException(
                    "Encrypted chunk authentication failed.", ex);
            }
            finally
            {
                if (!authenticated)
                    Array.Clear(plaintext, 0, plaintext.Length);
            }
        }

        private static GcmBlockCipher CreateCipher(
            bool forEncryption,
            byte[] key,
            byte[] nonce,
            byte[] associatedData)
        {
            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            cipher.Init(
                forEncryption,
                new AeadParameters(
                    new KeyParameter(key),
                    TagSizeBytes * 8,
                    nonce,
                    associatedData ?? new byte[0]));
            return cipher;
        }

        private static void ValidateKey(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (key.Length != KeySizeBytes)
                throw new ArgumentException(
                    "AES-256 requires a 32-byte key.", "key");
        }
    }
}
