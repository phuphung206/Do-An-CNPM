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
    public partial class FormHistory : Form
    {
        public FormHistory()
        {
            InitializeComponent();
            timerHistory.Start(); // Bắt đầu tự động kiểm tra lịch sử mỗi 5 giây
            dgvHistory.Columns.Add("Time", "Thời gian");
            dgvHistory.Columns.Add("Mode", "Chế độ");
            dgvHistory.Columns.Add("File", "File / Thư mục");
            dgvHistory.Columns.Add("Status", "Trạng thái");

            dgvHistory.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToAddRows = false;

            LoadHistory();
        }


        private string historyPath = System.IO.Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments),
            "SecureDataBackup",
            "backup_history.txt"
        );


        /// <summary>
        /// hàm đọc ls
        /// </summary>
        private void LoadHistory()
        {
            dgvHistory.Rows.Clear();

            if (!System.IO.File.Exists(historyPath))
                return;

            foreach (string line in System.IO.File.ReadAllLines(historyPath))
            {
                string[] data = line.Split('\t');

                if (data.Length == 4)
                {
                    dgvHistory.Rows.Add(
                        data[0],
                        data[1],
                        data[2],
                        data[3]
                    );
                }
            }
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {

        }

        // nút lm mới
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadHistory(); // Tải lại lịch sử ngay lập tức
        }

        // xóa ls 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc muốn xóa lịch sử?",
        "Xác nhận",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                dgvHistory.Rows.Clear();

                if (System.IO.File.Exists(historyPath))
                {
                    System.IO.File.Delete(historyPath);
                }

                MessageBox.Show("Đã xóa lịch sử!");
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string folder = System.IO.Path.GetDirectoryName(historyPath);

            System.IO.Directory.CreateDirectory(folder);

            System.Diagnostics.Process.Start("explorer.exe", folder);
        }

        // Tự động cập nhật lịch sử mỗi 5 giây
        private void timerHistory_Tick(object sender, EventArgs e)
        {
            LoadHistory(); // Đọc lại lịch sử từ file
        }
    }
}
