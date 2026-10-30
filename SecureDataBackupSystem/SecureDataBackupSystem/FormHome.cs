
using System;
using System.Windows.Forms;

namespace SecureDataBackupSystem
{
    public partial class FormHome : Form
    {
        private FormSchedule scheduleForm;  // Tạo một FormSchedule dùng chung để giữ trạng thái lập lịch
        private FormBackup backupForm = new FormBackup();
        private FormSettings settingsForm;  // Tạo một FormSettings dùng chung để giữ trạng thái các nút
        public FormHome()
        {
            InitializeComponent(); // Khởi tạo giao diện trang chủ
            settingsForm = new FormSettings(backupForm); // Tạo FormSettings một lần và kết nối với FormBackup
            panelContent.Dock = DockStyle.Fill;  // Sử dụng FormSchedule đã tạo trước đó
            scheduleForm = new FormSchedule();// Khởi tạo FormSchedule một lần
        }


        private void OpenForm(Form f)
        {
            // Xóa giao diện đang hiển thị
            panelContent.Controls.Clear();

            // Đưa Form con vào Panel
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;

            panelContent.Controls.Add(f);
            f.Show();
        }


        private void FormHome_Load(object sender, EventArgs e)
        {
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            OpenForm(backupForm);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            OpenForm(new FormRestore());
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            OpenForm(new FormHistory());
        }

        // Hiển thị lại FormSettings đã tạo trước đó
        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenForm(settingsForm); // Giữ nguyên trạng thái Bật/Tắt theo dõi khi chuyển trang
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();

            panelContent.Controls.Add(lblWelcome);
            panelContent.Controls.Add(lblDescription);
            panelContent.Controls.Add(picHome); ;
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        // Mở giao diện lập lịch trong panelContent
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            OpenForm(scheduleForm);  // Sử dụng FormSchedule đã tạo trước đó
        }
    }
}
