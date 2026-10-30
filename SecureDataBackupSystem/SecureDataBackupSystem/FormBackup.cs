using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SecureDataBackupSystem
{
    public partial class FormBackup : Form
    {
        private bool isAutoBackup = false;
        private bool isAutoRunning = false;
        public FormBackup()
        {
            InitializeComponent();
            // Cho phép kéo thả vào FormBackup
            this.AllowDrop = true;

            // Gắn sự kiện kéo thả cho FormBackup
            this.DragEnter += FormBackup_DragEnter;
            this.DragDrop += FormBackup_DragDrop;

            // Cho phép kéo thả vào ô đường dẫn
            txtFile.AllowDrop = true;

            // Gắn sự kiện kéo thả cho ô đường dẫn
            txtFile.DragEnter += FormBackup_DragEnter;
            txtFile.DragDrop += FormBackup_DragDrop;
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

        private void AddHistory(string mode, string status)
        {
            string fileName = txtFile.Text.TrimEnd('\\', '/');

            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            dgvHistory.Rows.Add(
                time,
                mode,
                fileName,
                status
            );

            System.IO.Directory.CreateDirectory(
                System.IO.Path.GetDirectoryName(historyPath)
            );

            string line = string.Join("\t",
                time,
                mode,
                fileName,
                status
            );

            System.IO.File.AppendAllText(
                historyPath,
                line + Environment.NewLine
            );
        }

        private void LoadHistory()
        {
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

        private void BackupFolder(
        string sourceFolder,
        string backupFolder,
        bool isFull,
        bool isIncremental,
        bool encrypt,
        string password,
        IProgress<int> progress)
        {
            System.IO.Directory.CreateDirectory(backupFolder);

            string[] files = System.IO.Directory.GetFiles(
                sourceFolder, "*", System.IO.SearchOption.AllDirectories
            );
            string historyFile = System.IO.Path.GetFullPath(historyPath);  // Lấy đường dẫn đầy đủ của file lịch sử
            // Loại bỏ file lịch sử khỏi danh sách sao lưu
            files = files.Where(file =>
                    !string.Equals(
                        System.IO.Path.GetFullPath(file),
                        historyFile,
                        StringComparison.OrdinalIgnoreCase
                    )
            ).ToArray();          

            
            int totalFiles = files.Length;
            int completedFiles = 0;

            foreach (string file in files)
            {
                string relativePath = file.Substring(sourceFolder.Length)
                    .TrimStart('\\', '/');

                string destination = System.IO.Path.Combine(
                    backupFolder, relativePath
                );

                if (encrypt)
                {
                    destination += ".enc";
                }

                System.IO.Directory.CreateDirectory(
                    System.IO.Path.GetDirectoryName(destination)
                );

                bool shouldCopy = isFull;


                if (isIncremental)
                {
                    shouldCopy = !System.IO.File.Exists(destination);

                    if (!shouldCopy)
                    {
                        if (encrypt)
                        {
                            // Giải mã vào file tạm để so sánh nội dung
                            string tempFile = System.IO.Path.Combine(
                                System.IO.Path.GetTempPath(),
                                Guid.NewGuid().ToString("N") + ".tmp"
                            );

                            try
                            {
                                DecryptFile(destination, tempFile, password);

                                using (var sha =
                                    System.Security.Cryptography.SHA256.Create())
                                using (var sourceStream =
                                    System.IO.File.OpenRead(file))
                                using (var backupStream =
                                    System.IO.File.OpenRead(tempFile))
                                {
                                    byte[] hash1 = sha.ComputeHash(sourceStream);
                                    byte[] hash2 = sha.ComputeHash(backupStream);

                                    shouldCopy = !hash1.SequenceEqual(hash2);
                                }
                            }
                            finally
                            {
                                if (System.IO.File.Exists(tempFile))
                                    System.IO.File.Delete(tempFile);
                            }
                        }
                        else
                        {
                            using (var sha =
                                System.Security.Cryptography.SHA256.Create())
                            using (var sourceStream =
                                System.IO.File.OpenRead(file))
                            using (var backupStream =
                                System.IO.File.OpenRead(destination))
                            {
                                byte[] hash1 = sha.ComputeHash(sourceStream);
                                byte[] hash2 = sha.ComputeHash(backupStream);

                                shouldCopy = !hash1.SequenceEqual(hash2);
                            }
                        }
                    }
                }



                if (shouldCopy)
                {
                    if (encrypt)
                    {
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            throw new Exception("Vui lòng nhập mật khẩu mã hóa!");
                        }

                        // Mã hóa ra file tạm trước
                        string tempEncrypted = destination + "." +
                            Guid.NewGuid().ToString("N") + ".tmp";

                        try
                        {
                            EncryptFile(file, tempEncrypted, password);

                            // Chỉ thay bản Backup cũ khi mã hóa đã thành công
                            if (System.IO.File.Exists(destination))
                            {
                                string oldBackup = destination + ".old";

                                if (System.IO.File.Exists(oldBackup))
                                    System.IO.File.Delete(oldBackup);

                                System.IO.File.Replace(
                                    tempEncrypted, destination, oldBackup);
                            }
                            else
                            {
                                System.IO.File.Move(tempEncrypted, destination);
                            }
                        }
                        finally
                        {
                            if (System.IO.File.Exists(tempEncrypted))
                                System.IO.File.Delete(tempEncrypted);
                        }
                    }
                    else
                    {
                        System.IO.File.Copy(file, destination, true);
                    }
                }


                completedFiles++;

                progress.Report(completedFiles * 100 / totalFiles);
            }

            progress.Report(100);
        }

        private void EncryptFile(string source, string destination, string password)
        {
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(iv);
            }

            using (var derive = new System.Security.Cryptography.Rfc2898DeriveBytes(
                password, salt, 100000))
            {
                byte[] keyMaterial = derive.GetBytes(64);

                byte[] encryptionKey = new byte[32];
                byte[] macKey = new byte[32];

                Array.Copy(keyMaterial, 0, encryptionKey, 0, 32);
                Array.Copy(keyMaterial, 32, macKey, 0, 32);

                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.Key = encryptionKey;
                    aes.IV = iv;
                    aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                    aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                    // Tạo file mã hóa tạm
                    string temp = destination + ".tmp";

                    try
                    {
                        using (var output = System.IO.File.Create(temp))
                        {
                            output.Write(salt, 0, salt.Length);
                            output.Write(iv, 0, iv.Length);

                            using (var crypto = new System.Security.Cryptography.CryptoStream(
                                output, aes.CreateEncryptor(),
                                System.Security.Cryptography.CryptoStreamMode.Write))
                            using (var input = System.IO.File.OpenRead(source))
                            {
                                input.CopyTo(crypto);
                            }
                        }

                        byte[] encryptedData = System.IO.File.ReadAllBytes(temp);

                        using (var hmac = new System.Security.Cryptography.HMACSHA256(macKey))
                        {
                            byte[] tag = hmac.ComputeHash(encryptedData);

                            using (var output = System.IO.File.Open(temp, System.IO.FileMode.Append))
                            {
                                output.Write(tag, 0, tag.Length);
                            }
                        }

                        if (System.IO.File.Exists(destination))
                        {
                            throw new Exception("File mã hóa đã tồn tại!");
                        }

                        System.IO.File.Move(temp, destination);
                    }
                    finally
                    {
                        if (System.IO.File.Exists(temp))
                            System.IO.File.Delete(temp);
                    }
                }
            }
        }

        private void DecryptFile(string source, string destination, string password)
        {
            byte[] data = System.IO.File.ReadAllBytes(source);

            if (data.Length < 64)
            {
                throw new Exception("File mã hóa không hợp lệ!");
            }

            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            byte[] savedTag = new byte[32];

            Array.Copy(data, 0, salt, 0, 16);
            Array.Copy(data, 16, iv, 0, 16);
            Array.Copy(data, data.Length - 32, savedTag, 0, 32);

            using (var derive =
                new System.Security.Cryptography.Rfc2898DeriveBytes(
                    password, salt, 100000))
            {
                byte[] keyMaterial = derive.GetBytes(64);

                byte[] encryptionKey = new byte[32];
                byte[] macKey = new byte[32];

                Array.Copy(keyMaterial, 0, encryptionKey, 0, 32);
                Array.Copy(keyMaterial, 32, macKey, 0, 32);

                int encryptedLength = data.Length - 32;

                byte[] actualTag;

                using (var hmac =
                    new System.Security.Cryptography.HMACSHA256(macKey))
                {
                    actualTag = hmac.ComputeHash(
                        data, 0, encryptedLength);
                }

                // So sánh HMAC trước khi giải mã
                int difference = 0;

                for (int i = 0; i < 32; i++)
                {
                    difference |= savedTag[i] ^ actualTag[i];
                }

                if (difference != 0)
                {
                    throw new Exception(
                        "Mật khẩu sai hoặc file mã hóa đã bị thay đổi!");
                }

                using (var aes =
                    System.Security.Cryptography.Aes.Create())
                {
                    aes.Key = encryptionKey;
                    aes.IV = iv;
                    aes.Mode =
                        System.Security.Cryptography.CipherMode.CBC;
                    aes.Padding =
                        System.Security.Cryptography.PaddingMode.PKCS7;

                    using (var input = new System.IO.MemoryStream(
                        data, 32, data.Length - 64))
                    using (var crypto =
                        new System.Security.Cryptography.CryptoStream(
                            input,
                            aes.CreateDecryptor(),
                            System.Security.Cryptography.CryptoStreamMode.Read))
                    using (var output =
                        System.IO.File.Create(destination))
                    {
                        crypto.CopyTo(output);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại để người dùng chọn file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // Hiển thị các loại file có thể chọn
                dialog.Filter =
                    "Tất cả file (*.*)|*.*|" +
                    "PDF (*.pdf)|*.pdf|" +
                    "Word (*.doc;*.docx)|*.doc;*.docx|" +
                    "Excel (*.xls;*.xlsx)|*.xls;*.xlsx|" +
                    "Hình ảnh (*.jpg;*.png)|*.jpg;*.png|" +
                    "Video (*.mp4)|*.mp4";

                // Mặc định hiển thị tất cả file
                dialog.FilterIndex = 1;

                // Mở hộp thoại chọn file
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Đưa đường dẫn file đã chọn vào TextBox
                    txtFile.Text = dialog.FileName;

                    // Thông báo đã chọn file thành công
                    lblStatus.Text = "Đã chọn file";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void radFull_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radIncremental_CheckedChanged(object sender, EventArgs e)
        {
        }

        // Sự kiện khi người dùng nhấn nút Sao lưu thủ công
        private async void btnBackup_Click(object sender, EventArgs e)
        {
            await RunBackupAsync(); // Gọi hàm sao lưu và chờ hoàn thành
        }

        // Hàm thực hiện toàn bộ quá trình sao lưu
        private async Task RunBackupAsync()
        {
            btnBackup.Enabled = false;
            // Kiểm tra người dùng đã chọn dữ liệu chưa
            if (string.IsNullOrWhiteSpace(txtFile.Text))
            {
                MessageBox.Show("Vui lòng chọn file hoặc thư mục!");
                btnBackup.Enabled = true;
                return;
            }
            // Lấy thư mục lưu các bản sao lưu của chương trình
            string backupRoot = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureDataBackup"
            );

            string sourcePath = System.IO.Path.GetFullPath(txtFile.Text); // Lấy đường dẫn nguồn đã chọn
            string rootPath = System.IO.Path.GetFullPath(backupRoot); // Kiểm tra có chọn chính thư mục lưu Backup hoặc thư mục cha không

            if (System.IO.Directory.Exists(sourcePath) &&
                (rootPath.Equals(sourcePath, StringComparison.OrdinalIgnoreCase) ||
                 rootPath.StartsWith(
                     sourcePath.TrimEnd(
                         System.IO.Path.DirectorySeparatorChar,
                         System.IO.Path.AltDirectorySeparatorChar
                     ) + System.IO.Path.DirectorySeparatorChar,
                     StringComparison.OrdinalIgnoreCase)))
            {
                // Không cho sao lưu thư mục chứa dữ liệu Backup
                MessageBox.Show("Không thể sao lưu thư mục chứa dữ liệu Backup của chương trình!");
                btnBackup.Enabled = true;
                return;
            }
            // Không cho chọn trực tiếp file lịch sử để sao lưu
            if (string.Equals(
                System.IO.Path.GetFullPath(txtFile.Text),
                System.IO.Path.GetFullPath(historyPath),
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể sao lưu file lịch sử của chương trình!");
                btnBackup.Enabled = true;
                return;
            }
            try
            {
                if (System.IO.Directory.Exists(txtFile.Text))
                {
                    if (!radFull.Checked && !radIncremental.Checked)
                    {
                        MessageBox.Show("Vui lòng chọn chế độ sao lưu!");
                        return;
                    }
                    try
                    {
                        string folderName = new System.IO.DirectoryInfo(
                            txtFile.Text).Name;

                        string destination = System.IO.Path.Combine(
                            backupRoot, folderName
                        );

                        bool isFull = radFull.Checked;
                        bool isIncremental = radIncremental.Checked;
                        bool encrypt = chkEncrypt.Checked;
                        string password = txtPassword.Text;
                        string sourceFolder = txtFile.Text;

                        if (encrypt && string.IsNullOrWhiteSpace(password))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu mã hóa!");
                            return;
                        }

                        progressBar1.Value = 0;
                        lblStatus.Text = "Đang sao lưu...";

                        var progress = new Progress<int>(value =>
                        {
                            progressBar1.Value = value;
                            lblStatus.Text = "Đang sao lưu: " + value + "%";
                        });

                        await Task.Run(() =>
                        {
                            BackupFolder(
                                sourceFolder,
                                destination,
                                isFull,
                                isIncremental,
                                encrypt,
                                password,
                                progress
                            );
                        });
                        AddHistory(
                        radFull.Checked ? "Full Backup" : "Incremental",
                        "Thành công"
                    );
                        progressBar1.Value = 100;
                        lblStatus.Text = "Sao lưu thư mục thành công!";

                        if (!isAutoRunning)
                            MessageBox.Show("Backup thư mục thành công!");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Lỗi Backup: " + ex.Message;

                        if (!isAutoRunning)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }
                    }

                    return;
                }

                if (!System.IO.File.Exists(txtFile.Text))
                {
                    MessageBox.Show("Vui lòng chọn file trước!");
                    return;
                }

                try
                {
                    string folder = System.IO.Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments),
                        "SecureDataBackup"
                    );

                    System.IO.Directory.CreateDirectory(folder);

                    string source = txtFile.Text;

                    string destination = System.IO.Path.Combine(
                        folder,
                        System.IO.Path.GetFileName(source)
                    );


                    if (radFull.Checked)
                    {
                        if (chkEncrypt.Checked)
                        {
                            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                            {
                                MessageBox.Show("Vui lòng nhập mật khẩu mã hóa!");
                                return;
                            }

                            string encryptedPath = destination + ".enc";

                            if (System.IO.File.Exists(encryptedPath))
                            {
                                MessageBox.Show(
                                    "Đã có bản mã hóa. Hãy đổi tên hoặc chuyển bản cũ trước khi sao lưu!");
                                return;
                            }

                            string password = txtPassword.Text;

                            await Task.Run(() =>
                            {
                                EncryptFile(source, encryptedPath, password);
                            });

                            lblStatus.Text = "Đã tạo bản Backup mã hóa!";
                        }
                        else
                        {
                            await Task.Run(() =>
                            {
                                System.IO.File.Copy(source, destination, true);
                            });

                            lblStatus.Text = "Full Backup thành công!";
                        }

                        AddHistory("Full Backup", "Thành công");

                        progressBar1.Value = 100;
                        if(!isAutoRunning)
                            MessageBox.Show("Backup thành công!");
                                            }


                    else if (radIncremental.Checked)
                    {
                        bool encrypt = chkEncrypt.Checked;
                        string password = txtPassword.Text;

                        if (encrypt && string.IsNullOrWhiteSpace(password))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu mã hóa!");
                            return;
                        }

                        string backupPath = encrypt
                            ? destination + ".enc"
                            : destination;

                        bool changed = !System.IO.File.Exists(backupPath);

                        if (!changed)
                        {
                            changed = await Task.Run(() =>
                            {
                                string compareFile = backupPath;
                                string tempFile = null;

                                try
                                {
                                    if (encrypt)
                                    {
                                        tempFile = System.IO.Path.Combine(
                                            System.IO.Path.GetTempPath(),
                                            Guid.NewGuid().ToString("N") + ".tmp"
                                        );

                                        DecryptFile(backupPath, tempFile, password);
                                        compareFile = tempFile;
                                    }

                                    using (var sha = SHA256.Create())
                                    using (var input1 = System.IO.File.OpenRead(source))
                                    using (var input2 = System.IO.File.OpenRead(compareFile))
                                    {
                                        byte[] hash1 = sha.ComputeHash(input1);
                                        byte[] hash2 = sha.ComputeHash(input2);

                                        return !hash1.SequenceEqual(hash2);
                                    }
                                }
                                finally
                                {
                                    if (tempFile != null && System.IO.File.Exists(tempFile))
                                        System.IO.File.Delete(tempFile);
                                }
                            });
                        }

                        if (changed)
                        {
                            await Task.Run(() =>
                            {
                                if (encrypt)
                                {
                                    string tempEncrypted = backupPath + "." +
                                        Guid.NewGuid().ToString("N") + ".tmp";

                                    try
                                    {
                                        EncryptFile(source, tempEncrypted, password);

                                        if (System.IO.File.Exists(backupPath))
                                        {
                                            System.IO.File.Replace(
                                                tempEncrypted, backupPath, null);
                                        }
                                        else
                                        {
                                            System.IO.File.Move(tempEncrypted, backupPath);
                                        }
                                    }
                                    finally
                                    {
                                        if (System.IO.File.Exists(tempEncrypted))
                                            System.IO.File.Delete(tempEncrypted);
                                    }
                                }
                                else
                                {
                                    System.IO.File.Copy(source, backupPath, true);
                                }
                            });

                            AddHistory("Incremental", "Thành công");
                            lblStatus.Text = "Incremental Backup thành công!";

                            if (!isAutoRunning)
                                MessageBox.Show("Đã sao lưu file thay đổi!");
                        }
                        else
                        {
                            lblStatus.Text = "File chưa thay đổi";

                            if (!isAutoRunning)
                                MessageBox.Show("Không cần sao lưu lại!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn chế độ sao lưu!");
                        return;
                    }

                    progressBar1.Value = 100;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Lỗi Backup: " + ex.Message;

                    if (!isAutoRunning)
                        MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            finally
            {
                btnBackup.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string folder = System.IO.Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "SecureDataBackup"
            );

            if (!System.IO.Directory.Exists(folder))
            {
                MessageBox.Show("Chưa có dữ liệu sao lưu!");
                return;
            }
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = folder;
                dialog.Title = "Chọn file cần khôi phục";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (SaveFileDialog save = new SaveFileDialog())
                    {
                        save.Title = "Chọn nơi khôi phục";
                        string fileName = System.IO.Path.GetFileName(dialog.FileName);

                        if (fileName.EndsWith(".enc", StringComparison.OrdinalIgnoreCase))
                        {
                            fileName = fileName.Substring(0, fileName.Length - 4);
                        }

                        save.FileName = fileName;

                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                if (System.IO.Path.GetFullPath(dialog.FileName)
                                    == System.IO.Path.GetFullPath(save.FileName))
                                {
                                    MessageBox.Show("Hãy chọn vị trí khác file sao lưu!");
                                    return;
                                }


                                if (dialog.FileName.EndsWith(
                                    ".enc", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                                    {
                                        MessageBox.Show("Vui lòng nhập mật khẩu giải mã!");
                                        return;
                                    }

                                    // Giải mã vào file tạm trước để tránh tạo file đích bị lỗi
                                    string tempFile = System.IO.Path.Combine(
                                        System.IO.Path.GetTempPath(),
                                        Guid.NewGuid().ToString("N") + ".tmp"
                                    );

                                    try
                                    {
                                        DecryptFile(dialog.FileName, tempFile, txtPassword.Text);

                                        System.IO.File.Move(tempFile, save.FileName);
                                    }
                                    finally
                                    {
                                        if (System.IO.File.Exists(tempFile))
                                        {
                                            System.IO.File.Delete(tempFile);
                                        }
                                    }
                                }
                                else
                                {
                                    System.IO.File.Copy(
                                        dialog.FileName,
                                        save.FileName,
                                        false
                                    );
                                }

                                lblStatus.Text = "Khôi phục thành công!";
                                MessageBox.Show("Restore thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi: " + ex.Message);
                            }
                        }
                    }
                }
            }

        }

        private void btnChonThuMuc_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục cần sao lưu";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = dialog.SelectedPath;
                    lblStatus.Text = "Đã chọn thư mục";
                }
            }

        }

        private void RestoreFolder(
            string sourceFolder,
            string destination,
            string password,
            IProgress<int> progress)
        {
            System.IO.Directory.CreateDirectory(destination);

            string[] files = System.IO.Directory.GetFiles(
                sourceFolder, "*",
                System.IO.SearchOption.AllDirectories);

            int completed = 0;

            foreach (string file in files)
            {
                string relativePath = file.Substring(sourceFolder.Length)
                    .TrimStart('\\', '/');

                if (relativePath.EndsWith(".enc.old",
                    StringComparison.OrdinalIgnoreCase))
                {
                    completed++;
                    progress.Report(completed * 100 / files.Length);
                    continue;
                }

                string target = System.IO.Path.Combine(
                    destination, relativePath);

                if (file.EndsWith(".enc",
                    StringComparison.OrdinalIgnoreCase))
                {
                    target = target.Substring(0, target.Length - 4);
                }

                System.IO.Directory.CreateDirectory(
                    System.IO.Path.GetDirectoryName(target));

                if (file.EndsWith(".enc",
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(password))
                        throw new Exception("Vui lòng nhập mật khẩu giải mã!");

                    DecryptFile(file, target, password);
                }
                else
                {
                    System.IO.File.Copy(file, target, false);
                }
                completed++;
                progress.Report(completed * 100 / files.Length);
            }
            progress.Report(100);
        }
        private async void btnRestoreFolder_Click(object sender, EventArgs e)
        {

            string backupRoot = System.IO.Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "SecureDataBackup"
            );
            if (!System.IO.Directory.Exists(backupRoot))
            {
                MessageBox.Show("Chưa có dữ liệu sao lưu!");
                return;
            }

            // Chọn thư mục đã được sao lưu
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục Backup cần khôi phục";
                dialog.SelectedPath = backupRoot;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                string sourceFolder = dialog.SelectedPath;

                // Chỉ cho phép chọn thư mục nằm trong vùng Backup
                string rootPath = System.IO.Path.GetFullPath(backupRoot)
                    .TrimEnd('\\') + "\\";

                string sourcePath = System.IO.Path.GetFullPath(sourceFolder)
                    .TrimEnd('\\') + "\\";

                if (!sourcePath.StartsWith(
                    rootPath, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Hãy chọn thư mục bên trong SecureDataBackup!");
                    return;
                }

                // Chọn nơi khôi phục
                using (FolderBrowserDialog save = new FolderBrowserDialog())
                {
                    save.Description = "Chọn nơi lưu thư mục khôi phục";

                    if (save.ShowDialog() != DialogResult.OK)
                        return;

                    string destination = System.IO.Path.Combine(
                        save.SelectedPath,
                        new System.IO.DirectoryInfo(sourceFolder).Name
                    );

                    // Không cho khôi phục đè lên dữ liệu có sẵn
                    if (System.IO.Directory.Exists(destination))
                    {
                        MessageBox.Show(
                            "Thư mục đích đã tồn tại. Hãy chọn nơi khác!");
                        return;
                    }


                    try
                    {
                        string password = txtPassword.Text;

                        progressBar1.Value = 0;
                        lblStatus.Text = "Đang khôi phục thư mục...";

                        var progress = new Progress<int>(value =>
                        {
                            progressBar1.Value = value;
                            lblStatus.Text = "Đang khôi phục: " + value + "%";
                        });

                        await Task.Run(() =>
                        {
                            RestoreFolder(
                                sourceFolder,
                                destination,
                                password,
                                progress
                            );
                        });

                        progressBar1.Value = 100;
                        lblStatus.Text = "Khôi phục thư mục thành công!";

                        MessageBox.Show(
                            "Restore thành công!\nĐã lưu tại: " + destination);
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Lỗi Restore: " + ex.Message;

                        try
                        {
                            if (System.IO.Directory.Exists(destination))
                            {
                                await Task.Run(() =>
                                    System.IO.Directory.Delete(destination, true));
                            }
                        }
                        catch (Exception cleanupEx)
                        {
                            MessageBox.Show(
                                "Không thể dọn thư mục Restore lỗi: " +
                                cleanupEx.Message);
                        }

                        MessageBox.Show("Lỗi Restore: " + ex.Message);
                    }
                }
            }

        }

        private void btnXoaHistory_Click(object sender, EventArgs e)
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
                lblStatus.Text = "Đã xóa lịch sử";
            }
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        // Bật sao lưu tự động với thư mục được chọn trong Settings
        // Bật Auto Backup cho cả file và thư mục
        public void StartAutoBackup(int minutes, string folderPath)
        {
            // Kiểm tra đường dẫn có phải file hoặc thư mục hợp lệ
            if (!System.IO.File.Exists(folderPath) &&
                !System.IO.Directory.Exists(folderPath))
            {
                MessageBox.Show("Vui lòng chọn file hoặc thư mục hợp lệ!");
                return;
            }
            // Kiểm tra thời gian sao lưu
            if (minutes <= 0 || minutes > int.MaxValue / 60000)
            {
                MessageBox.Show("Thời gian sao lưu không hợp lệ!");
                return;
            }
            txtFile.Text = folderPath; // Đưa đường dẫn file hoặc thư mục vào FormBackup
            radFull.Checked = true; // Chọn chế độ sao lưu Full
            timerBackup.Interval = minutes * 60000;   // Chuyển phút sang mili giây
            timerBackup.Start();  // Bật bộ hẹn giờ sao lưu tự động
            isAutoBackup = true; // Đánh dấu Auto Backup đang bật
            lblStatus.Text = "Đã bật Auto Backup: " + folderPath; // Hiển thị trạng thái
        }

        // Hàm tắt chế độ sao lưu tự động
        public void StopAutoBackup()
        {
            timerBackup.Stop();  // Dừng bộ hẹn giờ sao lưu
            isAutoBackup = false; // Đánh dấu chế độ sao lưu tự động đã tắt
            lblStatus.Text = "Đã tắt sao lưu tự động";   // Cập nhật trạng thái trên giao diện
        }
        private void btnStartAuto_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtFile.Text) &&
                !System.IO.Directory.Exists(txtFile.Text))
            {
                MessageBox.Show("Vui lòng chọn file hoặc thư mục!");
                return;
            }
            if (!radFull.Checked && !radIncremental.Checked)
            {
                MessageBox.Show("Vui lòng chọn chế độ Backup!");
                return;
            }

            timerBackup.Interval = (int)numMinutes.Value * 60 * 1000;
            timerBackup.Start();
            isAutoBackup = true;

            lblStatus.Text = "Đã bật Backup tự động";
            MessageBox.Show("Đã bật sao lưu tự động!");

        }

        private void btnStopAuto_Click(object sender, EventArgs e)
        {
            timerBackup.Stop();
            isAutoBackup = false;
            lblStatus.Text = "Đã tắt Backup tự động";

            MessageBox.Show("Đã tắt sao lưu tự động!");
        }

        private async void timerBackup_Tick(object sender, EventArgs e)
        {
            timerBackup.Stop();   // Dừng Timer để tránh chạy trùng
            if (!isAutoBackup) // Nếu Auto Backup đã tắt thì thoát
                return;
            if (isAutoRunning)   // Nếu đang sao lưu thì không chạy thêm
                return;

            try
            {
                isAutoRunning = true;  // Đánh dấu đang sao lưu tự động
                lblStatus.Text = "Đang sao lưu tự động..."; // Hiển thị trạng thái
                await RunBackupAsync();  // Gọi trực tiếp hàm sao lưu và chờ hoàn thành
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi Auto Backup: " + ex.Message;   // Hiển thị lỗi nếu sao lưu thất bại
            }
            finally
            {
                isAutoRunning = false;  // Đánh dấu kết thúc sao lưu
                if (isAutoBackup && !IsDisposed)   // Tiếp tục hẹn giờ nếu Auto Backup vẫn bật
                {
                    timerBackup.Start();
                }
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void chkEncrypt_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {
        }

        // Hàm bật theo dõi file hoặc thư mục
        private void btnTheoDoi_Click(object sender, EventArgs e)
        {

            if (!System.IO.Directory.Exists(txtFile.Text))
            {
                MessageBox.Show("Vui lòng chọn thư mục trước!");
                return;
            }
            fileSystemWatcher1.Created -= FileChanged;
            fileSystemWatcher1.Changed -= FileChanged;
            fileSystemWatcher1.Deleted -= FileChanged;

            fileSystemWatcher1.Created += FileChanged;
            fileSystemWatcher1.Changed += FileChanged;
            fileSystemWatcher1.Deleted += FileChanged;
            fileSystemWatcher1.EnableRaisingEvents = false;

            fileSystemWatcher1.Path = txtFile.Text;
            fileSystemWatcher1.IncludeSubdirectories = true;

            fileSystemWatcher1.NotifyFilter =
                System.IO.NotifyFilters.FileName |
                System.IO.NotifyFilters.DirectoryName |
                System.IO.NotifyFilters.LastWrite;

            fileSystemWatcher1.EnableRaisingEvents = true;

            lblStatus.Text = "Đang theo dõi thư mục";

            MessageBox.Show("Đã bật theo dõi thư mục!");

        }
        // Hàm tắt theo dõi file hoặc thư mục
        public void StopFolderMonitoring()
        {
            fileSystemWatcher1.EnableRaisingEvents = false;// Dừng nhận các sự kiện thay đổi
            lblStatus.Text = "Đã tắt theo dõi";// Cập nhật trạng thái
        }

        // Hàm theo dõi thay đổi của file hoặc thư mục
        public void StartFolderMonitoring(string path)
        {
            fileSystemWatcher1.EnableRaisingEvents = false;  // Tạm dừng theo dõi trước khi cấu hình

            // Kiểm tra người dùng chọn một file
            if (System.IO.File.Exists(path))
            {
                // Lấy thư mục chứa file
                fileSystemWatcher1.Path =
                    System.IO.Path.GetDirectoryName(path);

                // Chỉ theo dõi file đã chọn
                fileSystemWatcher1.Filter =
                    System.IO.Path.GetFileName(path);

                // Không theo dõi thư mục con
                fileSystemWatcher1.IncludeSubdirectories = false;
            }
            // Kiểm tra người dùng chọn một thư mục
            else if (System.IO.Directory.Exists(path))
            {
                fileSystemWatcher1.Path = path;  // Thiết lập thư mục cần theo dõi
                fileSystemWatcher1.Filter = "*.*";  // Theo dõi tất cả file trong thư mục
                fileSystemWatcher1.IncludeSubdirectories = true;  // Theo dõi cả thư mục con
            }
            else
            {
                // Thông báo khi đường dẫn không hợp lệ
                MessageBox.Show("File hoặc thư mục không tồn tại!");
                return;
            }

            // Theo dõi thay đổi tên, nội dung và thời gian chỉnh sửa
            fileSystemWatcher1.NotifyFilter =
                System.IO.NotifyFilters.FileName |
                System.IO.NotifyFilters.DirectoryName |
                System.IO.NotifyFilters.LastWrite;

            fileSystemWatcher1.EnableRaisingEvents = true;  // Bật chức năng theo dõi
            lblStatus.Text = "Đang theo dõi: " + path;   // Cập nhật trạng thái
        }

        private void FileChanged(
            object sender,
            System.IO.FileSystemEventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
                return;

            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    string action = "";

                    if (e.ChangeType == System.IO.WatcherChangeTypes.Created)
                        action = "Đã thêm";

                    else if (e.ChangeType == System.IO.WatcherChangeTypes.Changed)
                        action = "Đã sửa";

                    else if (e.ChangeType == System.IO.WatcherChangeTypes.Deleted)
                        action = "Đã xóa";

                    lblStatus.Text = action + ": " +
                        System.IO.Path.GetFileName(e.FullPath);
                }));
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtFile_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại để chọn file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // Cho phép lọc các định dạng file
                dialog.Filter =
                    "Tất cả file (*.*)|*.*|" +
                    "PDF (*.pdf)|*.pdf|" +
                    "Word (*.doc;*.docx)|*.doc;*.docx|" +
                    "Excel (*.xls;*.xlsx)|*.xls;*.xlsx|" +
                    "Hình ảnh (*.jpg;*.png)|*.jpg;*.png|" +
                    "Video (*.mp4)|*.mp4";

                // Hiển thị hộp thoại chọn file
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = dialog.FileName;   // Đưa đường dẫn file đã chọn vào ô txtFile
                    lblStatus.Text = "Đã chọn file";   // Cập nhật trạng thái
                }
            }
        }

        private void lblAutoBackup_Click(object sender, EventArgs e)
        {
        }

        private void numMinutes_ValueChanged(object sender, EventArgs e)
        {
        }

        // Hàm nhận cài đặt mã hóa từ FormSettings
        public void SetEncryption(bool enabled, string password)
        {
            chkEncrypt.Checked = enabled; // Bật hoặc tắt mã hóa trên FormBackup
            txtPassword.Text = enabled ? password : ""; // Cập nhật mật khẩu dùng khi sao lưu
        }


        // Hàm kiểm tra dữ liệu khi kéo file hoặc thư mục vào app
        private void FormBackup_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra dữ liệu có phải file hoặc thư mục không
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Cho phép thả dữ liệu
            }
            else
            {
                e.Effect = DragDropEffects.None;  // Không cho phép thả dữ liệu khác
            }
        }

        // Hàm nhận file hoặc thư mục khi thả vào app
        private void FormBackup_DragDrop(object sender, DragEventArgs e)
        {
            // Lấy danh sách đường dẫn được kéo vào
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];

            // Kiểm tra danh sách có dữ liệu không
            if (paths == null || paths.Length == 0)
                return;

            string path = paths[0];    // Lấy đường dẫn đầu tiên

            // Kiểm tra file hoặc thư mục có tồn tại không
            if (System.IO.File.Exists(path) ||
                System.IO.Directory.Exists(path))
            {
                txtFile.Text = path;// Hiển thị đường dẫn trong ô txtFile
                lblStatus.Text = "Đã kéo thả dữ liệu thành công!";    // Cập nhật trạng thái
            }
        }


    }
}
