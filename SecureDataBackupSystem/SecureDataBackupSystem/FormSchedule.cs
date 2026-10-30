using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SecureDataBackupSystem
{
    public partial class FormSchedule : Form
    {
        public FormSchedule()
        {
            InitializeComponent();
            
            cboFrequency.SelectedIndex = 0;// Mặc định chọn Hằng ngày
            cboFrequency.SelectedIndexChanged += cboFrequency_SelectedIndexChanged;// Gắn sự kiện khi thay đổi tần suất
            UpdateDayVisibility();// Cập nhật giao diện ngay khi mở Form
            EnableDragDrop(this);  // Kích hoạt kéo thả cho Form và toàn bộ control bên trong
            isScheduleEnabled = CheckScheduleExists();   // Kiểm tra lịch đã được tạo trước đó chưa
            btnSchedule.Text = isScheduleEnabled ? "Tắt lịch" : "Bật lịch";  // Cập nhật nút theo trạng thái lịch thực tế
            lblStatus.Text = isScheduleEnabled // Hiển thị trạng thái lịch
                ? "Đã bật lịch sao lưu"
                : "Chưa bật lịch sao lưu";
        }

        private const string TaskName = "SecureDataBackupSchedule";

        // Tạo lịch sao lưu trong Windows Task Scheduler
        private void CreateSchedule()
        {
            string sourcePath = txtSchedulePath.Text.Trim();

            // Kiểm tra đường dẫn nguồn
            if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
                throw new Exception("Vui lòng chọn file hoặc thư mục hợp lệ!");

            // Kiểm tra loại lịch

            string frequency = cboFrequency.Text;

            // Kiểm tra người dùng đã chọn tần suất chưa
            if (string.IsNullOrWhiteSpace(frequency))
                throw new Exception("Vui lòng chọn tần suất sao lưu!");

            // Lấy giờ sao lưu
            string time = dtpTime.Value.ToString("HH:mm");

            // Xác định loại lịch và số ngày lặp lại
            string scheduleType;
            int interval = 1;

            switch (frequency)
            {
                case "Hằng ngày":
                    scheduleType = "DAILY";
                    break;

                case "Mỗi 2 ngày":
                    scheduleType = "DAILY";
                    interval = 2;
                    break;

                case "Mỗi 3 ngày":
                    scheduleType = "DAILY";
                    interval = 3;
                    break;

                case "Hằng tuần":
                    scheduleType = "WEEKLY";
                    break;

                default:
                    throw new Exception("Tần suất không hợp lệ!");
            }


            // Nếu lịch hằng tuần, lấy thứ trong tuần
            string dayArgument = "";


            if (scheduleType == "WEEKLY")
            {
                // Các thứ tương ứng với mã ngày của Windows Task Scheduler
                string[] days = {
        "MON", "TUE", "WED", "THU",
        "FRI", "SAT", "SUN"
    };

                // Kiểm tra đã chọn ít nhất một thứ chưa
                if (clbDays.CheckedIndices.Count == 0)
                    throw new Exception("Vui lòng chọn ít nhất một thứ!");

                // Lưu danh sách các thứ đã chọn
                List<string> selectedDays = new List<string>();

                foreach (int index in clbDays.CheckedIndices)
                {
                    selectedDays.Add(days[index]);
                }

                // Ví dụ: /D MON,WED,FRI
                dayArgument = " /D " + string.Join(",", selectedDays);
            }


            // Lấy đường dẫn file EXE của ứng dụng
            string exePath = Application.ExecutablePath;

            // Lưu đường dẫn nguồn vào file cấu hình
            string configFolder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "SecureDataBackupSystem"
            );

            Directory.CreateDirectory(configFolder);

            string configPath = Path.Combine(
                configFolder, "schedule_path.txt"
            );

            // Tạo lệnh chạy Backup ngầm
            string taskCommand = "\\\"" + exePath +
                "\\\" --scheduled-backup";

            // Tạo lệnh đăng ký lịch
            string arguments =
                "/Create /F /TN \"" + TaskName + "\"" +
                " /SC " + scheduleType +
                " /MO " + interval + // Số ngày giữa các lần sao lưu
                dayArgument +
                " /ST " + time +
                " /TR \"" + taskCommand + "\"";

            // Gọi Windows Task Scheduler
            RunSchtasks(arguments);

            // Chỉ lưu cấu hình sau khi tạo lịch thành công
            File.WriteAllText(configPath, sourcePath);
        }

        // Xóa lịch sao lưu trong Windows Task Scheduler
        private void DeleteSchedule()
        {
            RunSchtasks("/Delete /F /TN \"" + TaskName + "\"");
        }

        // Chạy lệnh schtasks và kiểm tra kết quả
        private void RunSchtasks(string arguments)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = Process.Start(info))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Nếu Windows không tạo/xóa lịch được
                if (process.ExitCode != 0)
                {
                    throw new Exception(
                        "Task Scheduler báo lỗi: " +
                        (string.IsNullOrWhiteSpace(error) ? output : error)
                    );
                }
            }
        }

        // Biến lưu trạng thái bật/tắt lịch
        private bool isScheduleEnabled = false;
        // Bật kéo thả cho Form và tất cả control con
        private void EnableDragDrop(Control control)
        {
            // Cho phép nhận file hoặc thư mục kéo vào
            control.AllowDrop = true;

            // Gỡ sự kiện cũ để tránh chạy trùng
            control.DragEnter -= Schedule_DragEnter;
            control.DragDrop -= Schedule_DragDrop;

            // Gắn sự kiện kéo thả
            control.DragEnter += Schedule_DragEnter;
            control.DragDrop += Schedule_DragDrop;

            // Duyệt tất cả control nằm bên trong
            foreach (Control child in control.Controls)
            {
                EnableDragDrop(child);
            }
        }

        // Kiểm tra dữ liệu được kéo vào
        private void Schedule_DragEnter(object sender, DragEventArgs e)
        {
            // Chỉ chấp nhận file hoặc thư mục
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Xử lý khi thả file hoặc thư mục
        private void Schedule_DragDrop(object sender, DragEventArgs e)
        {
            // Lấy danh sách đường dẫn được kéo vào
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (paths == null || paths.Length == 0)
                return;
            string path = paths[0];   // Lấy file hoặc thư mục đầu tiên

            // Kiểm tra đường dẫn có tồn tại
            if (System.IO.File.Exists(path) ||
                System.IO.Directory.Exists(path))
            {
                txtSchedulePath.Text = path;   // Hiển thị đường dẫn
                lblStatus.Text = "Đã kéo thả thành công!";// Cập nhật trạng thái
            }
        }

        // Sự kiện khi nhấn nút Bật/Tắt lịch

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (isScheduleEnabled)
                {
                    // Xóa lịch đã tạo trong Windows
                    DeleteSchedule();

                    isScheduleEnabled = false;
                    btnSchedule.Text = "Bật lịch";
                    lblStatus.Text = "Đã tắt lịch sao lưu";
                }
                else
                {
                    // Đăng ký lịch tự động
                    CreateSchedule();

                    isScheduleEnabled = true;
                    btnSchedule.Text = "Tắt lịch";
                    lblStatus.Text = "Đã bật lịch sao lưu";

                    MessageBox.Show("Đã tạo lịch sao lưu thành công!");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu không thể tạo/xóa lịch
                MessageBox.Show("Lỗi lập lịch: " + ex.Message);
            }
        }


        // Chọn file cần sao lưu theo lịch
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            // Mở cửa sổ chọn file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Tất cả file (*.*)|*.*";

                // Nếu người dùng chọn file thành công
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị đường dẫn file đã chọn
                    txtSchedulePath.Text = dialog.FileName;
                }
            }
        }

        // Chọn thư mục cần sao lưu theo lịch
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            // Mở cửa sổ chọn thư mục
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục cần sao lưu";
                if (dialog.ShowDialog() == DialogResult.OK) // Nếu người dùng chọn thư mục thành công
                {
                    txtSchedulePath.Text = dialog.SelectedPath; // Hiển thị đường dẫn thư mục đã chọn
                }
            }
        }

        // Khi người dùng thay đổi tần suất sao lưu
        private void cboFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDayVisibility();
        }

        // Chỉ hiện danh sách chọn thứ khi chọn Hằng tuần
        private void UpdateDayVisibility()
        {
            bool isWeekly = cboFrequency.Text == "Hằng tuần";

            // Ẩn hoặc hiện nhãn và danh sách các thứ
            lblDay.Visible = isWeekly;
            clbDays.Visible = isWeekly;
        }
        // Kiểm tra lịch đã tồn tại trong Windows Task Scheduler chưa
        private bool CheckScheduleExists()
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = "/Query /TN \"" + TaskName + "\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = Process.Start(info))
            {
                process.StandardOutput.ReadToEnd();
                process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Mã 0 nghĩa là Windows tìm thấy tác vụ
                return process.ExitCode == 0;
            }
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {

        }
    }
}
