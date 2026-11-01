using System;
using System.Collections.Generic;
using System.IO;

namespace SecureDataBackupSystem.CoreEngine
{
    public static class FileScanner
    {
        // Scans a single file or all files in a directory and its subdirectories.
        // Step 1: fail loudly if a file is inaccessible, instead of silently
        // treating an incomplete scan as a successful backup snapshot.
        public static List<BackupFileMetadata> Scan(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException("Source path is required.", "sourcePath");

            string fullSourcePath = Path.GetFullPath(sourcePath);
            var results = new List<BackupFileMetadata>();

            if (File.Exists(fullSourcePath))
            {
                results.Add(ReadFile(fullSourcePath, Path.GetFileName(fullSourcePath)));
                return results;
            }

            if (!Directory.Exists(fullSourcePath))
                throw new DirectoryNotFoundException("Source does not exist: " + fullSourcePath);

            string prefix = fullSourcePath;
            if (!prefix.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                prefix += Path.DirectorySeparatorChar;

            foreach (string path in Directory.EnumerateFiles(
                fullSourcePath, "*", SearchOption.AllDirectories))
            {
                string relativePath = path.Substring(prefix.Length);
                results.Add(ReadFile(path, relativePath));
            }

            results.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(
                a.RelativePath, b.RelativePath));
            return results;
        }

        private static BackupFileMetadata ReadFile(string path, string relativePath)
        {
            var before = new FileInfo(path);
            long length = before.Length;
            DateTime modified = before.LastWriteTimeUtc;
            string hash = HashCalculator.CalculateSha256(path);

            // Do not use inconsistent metadata if the file changed mid-scan.
            before.Refresh();
            if (before.Length != length || before.LastWriteTimeUtc != modified)
                throw new IOException("File changed while scanning: " + path);

            return new BackupFileMetadata
            {
                FullPath = path,
                RelativePath = relativePath,
                SizeBytes = length,
                LastWriteTimeUtc = modified,
                Sha256 = hash
            };
        }
    }
}
