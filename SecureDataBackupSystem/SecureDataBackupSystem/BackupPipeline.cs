using System;
using System.IO;

namespace SecureDataBackupSystem
{
    // Shared file enumeration, destination mapping, and progress reporting for
    // manual and scheduled backups. Backup policy stays with the caller.
    internal static class BackupPipeline
    {
        internal static int Run(
            string sourcePath,
            string destinationRoot,
            string excludedFile,
            bool appendEncryptedExtension,
            Func<string, string, bool> shouldBackup,
            Action<string, string> writeBackup,
            IProgress<int> progress)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new Exception("Chưa chọn đường dẫn sao lưu!");
            if (string.IsNullOrWhiteSpace(destinationRoot))
                throw new Exception("Chưa chọn thư mục lưu Backup!");
            if (shouldBackup == null)
                throw new ArgumentNullException("shouldBackup");
            if (writeBackup == null)
                throw new ArgumentNullException("writeBackup");

            sourcePath = Path.GetFullPath(sourcePath);
            destinationRoot = Path.GetFullPath(destinationRoot);

            bool isDirectory = Directory.Exists(sourcePath);
            if (!isDirectory && !File.Exists(sourcePath))
                throw new Exception("File hoặc thư mục không tồn tại!");

            string[] files;
            string sourceRoot = null;
            if (isDirectory)
            {
                sourceRoot = sourcePath.TrimEnd(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar);
                files = Directory.GetFiles(
                    sourcePath, "*", SearchOption.AllDirectories);
            }
            else
            {
                files = new[] { sourcePath };
            }

            if (!string.IsNullOrWhiteSpace(excludedFile))
            {
                string excludedPath = Path.GetFullPath(excludedFile);
                files = Array.FindAll(files, file =>
                    !string.Equals(
                        Path.GetFullPath(file),
                        excludedPath,
                        StringComparison.OrdinalIgnoreCase));
            }

            Directory.CreateDirectory(destinationRoot);
            int copiedFiles = 0;
            int totalFiles = files.Length;

            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
            {
                string file = files[fileIndex];
                string relativePath = isDirectory
                    ? file.Substring(sourceRoot.Length)
                        .TrimStart(Path.DirectorySeparatorChar,
                                   Path.AltDirectorySeparatorChar)
                    : Path.GetFileName(file);

                string destination = Path.Combine(destinationRoot, relativePath);
                if (appendEncryptedExtension)
                    destination += ".enc";

                string destinationDirectory = Path.GetDirectoryName(destination);
                if (!string.IsNullOrEmpty(destinationDirectory))
                    Directory.CreateDirectory(destinationDirectory);

                if (shouldBackup(file, destination))
                {
                    writeBackup(file, destination);
                    copiedFiles++;
                }

                if (progress != null && totalFiles > 0)
                    progress.Report((fileIndex + 1) * 100 / totalFiles);
            }

            if (progress != null)
                progress.Report(100);

            return copiedFiles;
        }
    }
}
