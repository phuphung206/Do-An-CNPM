using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureDataBackupSystem
{
    public partial class FormSettings : Form
    {

        // Khởi tạo FormSettings và nhận FormBackup
        private FormBackup backupForm;
        public FormSettings(FormBackup backup)
        {
            InitializeComponent();
            backupForm = backup;
            // Cho phép kéo thả file hoặc thư mục vào hai ô đường dẫn
            txtAutoPath.AllowDrop = true;
            txtFolderPath.AllowDrop = true;
            // Gắn sự kiện khi kéo và thả vào ô sao lưu tự động
            txtAutoPath.DragEnter += Path_DragEnter;
            txtAutoPath.DragDrop += Path_DragDrop;
            // Gắn sự kiện khi kéo và thả vào ô theo dõi
            txtFolderPath.DragEnter += Path_DragEnter;
            txtFolderPath.DragDrop += Path_DragDrop;
        }
        // Kiểm tra dữ liệu đang kéo vào có phải file hoặc thư mục không
        private void Path_DragEnter(object sender, DragEventArgs e)
        {
            // Nếu dữ liệu hợp lệ thì cho phép thả
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Xử lý khi người dùng thả file hoặc thư mục vào ô đường dẫn
        private void Path_DragDrop(object sender, DragEventArgs e)
        {
            // Lấy danh sách đường dẫn được kéo vào
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];

            // Nếu không có dữ liệu thì dừng
            if (paths == null || paths.Length == 0)
                return;

            // Lấy đường dẫn đầu tiên
            string path = paths[0];

            // Kiểm tra file hoặc thư mục có tồn tại không
            if (System.IO.File.Exists(path) ||
                System.IO.Directory.Exists(path))
            {
                // Xác định ô TextBox mà người dùng vừa thả vào
                TextBox txt = sender as TextBox;

                // Hiển thị đường dẫn vào đúng ô TextBox
                if (txt != null)
                    txt.Text = path;
            }
        }

        // Lưu trạng thái bật/tắt theo dõi
        private bool isMonitoring = false;
        // Sự kiện khi người dùng nhấn nút bật Auto Backup
        // Sự kiện nhấn nút Bật Auto Backup
        private void btnStartAuto_Click(object sender, EventArgs e)
        {
            string folderPath = txtAutoPath.Text; // Lấy đường dẫn thư mục từ FormSettings
            if (!System.IO.Directory.Exists(folderPath) &&
            !System.IO.File.Exists(folderPath))// Kiểm tra người dùng đã chọn thư mục và file chưa
            {
                MessageBox.Show("Vui lòng chọn file hoặc thư mục sao lưu tự động!");
                return;
            }

            // Gửi thời gian và đường dẫn sang FormBackup
            backupForm.StartAutoBackup(
                (int)numMinutes.Value,
                folderPath
            );

            lblStatus.Text = "Đã gửi yêu cầu bật Auto Backup";// Hiển thị thông báo
        }

        // Sự kiện khi người dùng nhấn nút Tắt Auto Backup
        private void btnStopAuto_Click(object sender, EventArgs e)
        {
            backupForm.StopAutoBackup();// Gọi hàm tắt sao lưu tự động từ FormBackup

            lblStatus.Text = "Đã tắt sao lưu tự động";// Hiển thị thông báo trên FormSettings
        }

        // Sự kiện bật hoặc tắt theo dõi bằng một nút
        private void btnTheoDoi_Click(object sender, EventArgs e)
        {
            // Nếu đang theo dõi thì tắt
            if (isMonitoring)
            {
                backupForm.StopFolderMonitoring();// Dừng theo dõi trong FormBackup
                isMonitoring = false;   // Cập nhật trạng thái
                btnTheoDoi.Text = "Bật theo dõi";  // Đổi tên nút thành Bật theo dõi
                lblStatus.Text = "Đã tắt theo dõi";  // Hiển thị trạng thái
            }
            else
            {
                string path = txtFolderPath.Text;// Lấy đường dẫn file hoặc thư mục đã chọn

                // Kiểm tra đường dẫn hợp lệ
                if (!System.IO.File.Exists(path) &&
                    !System.IO.Directory.Exists(path))
                {
                    MessageBox.Show("Vui lòng chọn file hoặc thư mục!");
                    return;
                }
                backupForm.StartFolderMonitoring(path);// Bắt đầu theo dõi
                isMonitoring = true; // Cập nhật trạng thái
                btnTheoDoi.Text = "Tắt theo dõi";    // Đổi tên nút thành Tắt theo dõi
                lblStatus.Text = "Đang theo dõi: " + path;// Hiển thị trạng thái
            }
        }

        private void timerBackup_Tick(object sender, EventArgs e)
        {

        }

        // Sự kiện chọn thư mục cần sao lưu tự động
        private void btnChooseAutoFolder_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn thư mục
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)// Khi người dùng chọn thành công
                {
                    txtAutoPath.Text = dialog.SelectedPath;// Hiển thị đường dẫn thư mục đã chọn
                }
            }
        }

        // Sự kiện chọn thư mục cần theo dõi
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại chọn thư mục
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                // Kiểm tra người dùng đã chọn thư mục
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị đường dẫn lên TextBox
                    txtFolderPath.Text = dialog.SelectedPath;
                }
            }
        }

        // Sự kiện chọn một file để theo dõi
        private void btnChooseMonitorFile_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // Cho phép chọn tất cả loại file
                dialog.Filter = "Tất cả file (*.*)|*.*";

                // Kiểm tra người dùng đã chọn file
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị đường dẫn file vào ô theo dõi
                    txtFolderPath.Text = dialog.FileName;
                }
            }
        }

        //chọn file sao lưu tự động
        private void btnChooseAutoFile_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn file để sao lưu tự động
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // Cho phép chọn tất cả loại file
                dialog.Filter = "Tất cả file (*.*)|*.*";

                // Nếu người dùng chọn file thành công
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị đường dẫn file đã chọn
                    txtAutoPath.Text = dialog.FileName;
                }
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
