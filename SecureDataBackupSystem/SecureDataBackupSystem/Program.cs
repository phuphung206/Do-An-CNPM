
using System;
using System.IO;
using System.Windows.Forms;

namespace SecureDataBackupSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Kiểm tra chương trình có chạy theo lịch không
            if (args.Length > 0 && args[0] == "--scheduled-backup")
            {
                try
                {
                    // Đọc đường dẫn nguồn từ file cấu hình
                    string configPath = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.ApplicationData),
                        "SecureDataBackupSystem",
                        "schedule_path.txt"
                    );

                    // Kiểm tra file cấu hình có tồn tại không
                    if (!File.Exists(configPath))
                        throw new Exception("Chưa có cấu hình lịch sao lưu!");

                    // Lấy đường dẫn file hoặc thư mục cần Backup
                    string sourcePath = File.ReadAllText(configPath).Trim();

                    // Gọi chức năng sao lưu không cần mở Form
                    BackupService.RunScheduledBackup(sourcePath);
                }
                catch (Exception ex)
                {
                    // Ghi lỗi vào file để kiểm tra khi chạy ngầm
                    string logFolder = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.ApplicationData),
                        "SecureDataBackupSystem"
                    );

                    Directory.CreateDirectory(logFolder);

                    File.AppendAllText(
                        Path.Combine(logFolder, "schedule_error.log"),
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                        + " - " + ex.Message + Environment.NewLine
                    );

                    // Trả mã lỗi cho Windows Task Scheduler
                    Environment.ExitCode = 1;
                }

                return;
            }

            // Chạy giao diện bình thường
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormHome());
        }
    }
}
