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
    public partial class Form1 : Form
    {
        private bool isAutoBackup = false;
        private bool isAutoRunning = false;
        public Form1()
        {
            InitializeComponent();
            dgvHistory.Columns.Add("Time", "Thời gian");
            dgvHistory.Columns.Add("Mode", "Chế độ");
            dgvHistory.Columns.Add("File", "File / Thư mục");
            dgvHistory.Columns.Add("Status", "Trạng thái");

            dgvHistory.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToAddRows = false;

            LoadHistory();
            // Các nút
            BoGoc(btnBackup, 12);
            BoGoc(btnRestore, 12);
            BoGoc(btnRestoreFolder, 12);
            BoGoc(btnStartAuto, 12);
            BoGoc(btnStopAuto, 12);
            BoGoc(btnTheoDoi, 12);
            BoGoc(btnXoaHistory, 12);

            // Ô nhập
            BoGoc(txtFile, 8);
            BoGoc(txtPassword, 8);

            // Bảng lịch sử
            BoGoc(dgvHistory, 10);

            BoGoc(btnChonFile, 12);       // Nút Chọn file
            BoGoc(btnChonThuMuc, 12); // Nút Chọn thư mục
            BoGoc(progressBar1, 10);

        }

        private void BoGoc(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            System.Drawing.Drawing2D.GraphicsPath path =
                new System.Drawing.Drawing2D.GraphicsPath();

            int d = Math.Min(radius * 2,
                Math.Min(control.Width, control.Height));

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(control.Width - d, 0, d, d, 270, 90);
            path.AddArc(control.Width - d, control.Height - d, d, d, 0, 90);
            path.AddArc(0, control.Height - d, d, d, 90, 90);
            path.CloseFigure();

            var oldRegion = control.Region;
            control.Region = new Region(path);
            oldRegion?.Dispose();
            path.Dispose();
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
                sourceFolder,
                "*",
                System.IO.SearchOption.AllDirectories
            );

            
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
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = dialog.FileName;
                lblStatus.Text = "Đã chọn file";
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

        private async void btnBackup_Click(object sender, EventArgs e)
        {
            btnBackup.Enabled = false;

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
                        string backupRoot = System.IO.Path.Combine(
                            Environment.GetFolderPath(
                                Environment.SpecialFolder.MyDocuments),
                            "SecureDataBackup"
                        );

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
            timerBackup.Stop();

            if (!isAutoBackup)
                return;

            isAutoRunning = true;

            try
            {
                btnBackup.PerformClick();

                while (!btnBackup.Enabled)
                {
                    await Task.Delay(200);
                }
            }
            finally
            {
                isAutoRunning = false;

                if (isAutoBackup && !this.IsDisposed)
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
                // Form đang đóng
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAutoBackup_Click(object sender, EventArgs e)
        {

        }

        private void numMinutes_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
