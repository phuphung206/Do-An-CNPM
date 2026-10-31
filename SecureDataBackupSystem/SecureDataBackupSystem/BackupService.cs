using System;
using System.IO;

namespace SecureDataBackupSystem
{
    public static class BackupService
    {
        private static readonly string backupRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "SecureDataBackup");

        // Hàm thực hiện Full Backup theo lịch
        public static void RunScheduledBackup(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new Exception("Chưa chọn đường dẫn sao lưu!");

            sourcePath = Path.GetFullPath(sourcePath);
            string root = Path.GetFullPath(backupRoot)
                .TrimEnd(Path.DirectorySeparatorChar);

            if (sourcePath.Equals(root, StringComparison.OrdinalIgnoreCase) ||
                root.StartsWith(
                    sourcePath.TrimEnd(Path.DirectorySeparatorChar) +
                    Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Không thể sao lưu thư mục chứa dữ liệu Backup!");
            }

            bool isDirectory = Directory.Exists(sourcePath);
            if (!isDirectory && !File.Exists(sourcePath))
                throw new Exception("File hoặc thư mục không tồn tại!");

            string destinationRoot = isDirectory
                ? Path.Combine(backupRoot, new DirectoryInfo(sourcePath).Name)
                : backupRoot;

            if (!isDirectory && sourcePath.Equals(
                Path.GetFullPath(Path.Combine(
                    destinationRoot, Path.GetFileName(sourcePath))),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Nguồn và đích trùng nhau!");
            }

            // Lịch hiện tại vẫn là Full Backup không mã hóa. Chỉ việc liệt kê,
            // ánh xạ đích và xử lý file được dùng chung với backup thủ công.
            BackupPipeline.Run(
                sourcePath,
                destinationRoot,
                null,
                false,
                (source, destination) => true,
                (source, destination) => File.Copy(source, destination, true),
                null);

            string historyPath = Path.Combine(backupRoot, "backup_history.txt");
            string line = string.Join("\t",
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                "Scheduled Full Backup",
                sourcePath,
                "Thành công");
            File.AppendAllText(historyPath, line + Environment.NewLine);
        }
    }
}
