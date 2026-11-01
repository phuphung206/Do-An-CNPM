using System;
using System.IO;
using System.Security.Cryptography;

namespace SecureDataBackupSystem.CoreEngine
{
    public static class HashCalculator
    {
        // Compatible with .NET Framework 4.7.2 and modern .NET.
        // Streams the file instead of loading it entirely into memory.
        public static string CalculateSha256(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.", "filePath");

            using (var sha = SHA256.Create())
            using (var stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] bytes = sha.ComputeHash(stream);
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}
