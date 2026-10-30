
using System;
using System.IO;

namespace SecureDataBackupSystem
{
    public static class BackupService
    {
        // Thư mục mặc định chứa dữ liệu sao lưu
        private static readonly string backupRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "SecureDataBackup"
        );

        // Hàm thực hiện Full Backup theo lịch
        public static void RunScheduledBackup(string sourcePath)
        {
            // Kiểm tra đường dẫn đầu vào
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new Exception("Chưa chọn đường dẫn sao lưu!");

            sourcePath = Path.GetFullPath(sourcePath);

            // Không cho sao lưu chính thư mục chứa bản Backup
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

            Directory.CreateDirectory(backupRoot);

            // Kiểm tra nguồn là file hay thư mục
            if (File.Exists(sourcePath))
            {
                // Sao lưu một file
                string destination = Path.Combine(
                    backupRoot, Path.GetFileName(sourcePath));

                if (sourcePath.Equals(
                    Path.GetFullPath(destination),
                    StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Nguồn và đích trùng nhau!");

                File.Copy(sourcePath, destination, true);
            }
            else if (Directory.Exists(sourcePath))
            {
                // Sao lưu toàn bộ thư mục
                string folderName = new DirectoryInfo(sourcePath).Name;
                string destinationFolder = Path.Combine(
                    backupRoot, folderName);

                foreach (string file in Directory.GetFiles(
                    sourcePath, "*", SearchOption.AllDirectories))
                {
                    // Giữ cấu trúc thư mục con
                    string relativePath = file.Substring(
                        sourcePath.TrimEnd('\\', '/').Length)
                        .TrimStart('\\', '/');

                    string destination = Path.Combine(
                        destinationFolder, relativePath);

                    Directory.CreateDirectory(
                        Path.GetDirectoryName(destination));

                    // Ghi đè bản sao lưu cũ
                    File.Copy(file, destination, true);
                }
            }
            else
            {
                throw new Exception("File hoặc thư mục không tồn tại!");
            }

            // Ghi lịch sử khi sao lưu thành công
            string historyPath = Path.Combine(
                backupRoot, "backup_history.txt");

            string line = string.Join("\t",
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                "Scheduled Full Backup",
                sourcePath,
                "Thành công"
            );

            File.AppendAllText(
                historyPath, line + Environment.NewLine);
        }
    }
}
