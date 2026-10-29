namespace SecureDataBackupSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnChonFile = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblChonFile = new System.Windows.Forms.Label();
            this.lblCheDo = new System.Windows.Forms.Label();
            this.lblTienTrinh = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.radFull = new System.Windows.Forms.RadioButton();
            this.radIncremental = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnChonThuMuc = new System.Windows.Forms.Button();
            this.btnRestoreFolder = new System.Windows.Forms.Button();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnXoaHistory = new System.Windows.Forms.Button();
            this.lblAutoBackup = new System.Windows.Forms.Label();
            this.btnStartAuto = new System.Windows.Forms.Button();
            this.btnStopAuto = new System.Windows.Forms.Button();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.btnTheoDoi = new System.Windows.Forms.Button();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChonFile
            // 
            this.btnChonFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnChonFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonFile.FlatAppearance.BorderSize = 0;
            this.btnChonFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChonFile.ForeColor = System.Drawing.Color.White;
            this.btnChonFile.Location = new System.Drawing.Point(705, 102);
            this.btnChonFile.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(130, 42);
            this.btnChonFile.TabIndex = 12;
            this.btnChonFile.Text = "chọn file";
            this.btnChonFile.UseVisualStyleBackColor = false;
            this.btnChonFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 20);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1000, 55);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "SECURE DATA BACKUP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblChonFile
            // 
            this.lblChonFile.AutoSize = true;
            this.lblChonFile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChonFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblChonFile.Location = new System.Drawing.Point(40, 77);
            this.lblChonFile.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblChonFile.Name = "lblChonFile";
            this.lblChonFile.Size = new System.Drawing.Size(267, 30);
            this.lblChonFile.TabIndex = 2;
            this.lblChonFile.Text = "Chọn dữ liệu cần sao lưu";
            // 
            // lblCheDo
            // 
            this.lblCheDo.AutoSize = true;
            this.lblCheDo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCheDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblCheDo.Location = new System.Drawing.Point(40, 159);
            this.lblCheDo.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblCheDo.Name = "lblCheDo";
            this.lblCheDo.Size = new System.Drawing.Size(166, 30);
            this.lblCheDo.TabIndex = 3;
            this.lblCheDo.Text = "Chế độ sao lưu";
            this.lblCheDo.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblTienTrinh
            // 
            this.lblTienTrinh.AutoSize = true;
            this.lblTienTrinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTienTrinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblTienTrinh.Location = new System.Drawing.Point(40, 442);
            this.lblTienTrinh.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTienTrinh.Name = "lblTienTrinh";
            this.lblTienTrinh.Size = new System.Drawing.Size(194, 30);
            this.lblTienTrinh.TabIndex = 4;
            this.lblTienTrinh.Text = "Tiến trình sao lưu";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblStatus.Location = new System.Drawing.Point(40, 500);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(185, 28);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Trạng thái: Sẵn sàng";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.Snow;
            this.btnBackup.Location = new System.Drawing.Point(40, 335);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(300, 50);
            this.btnBackup.TabIndex = 6;
            this.btnBackup.Text = "\tBackup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestore.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRestore.Location = new System.Drawing.Point(365, 335);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRestore.Size = new System.Drawing.Size(300, 50);
            this.btnRestore.TabIndex = 7;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // radFull
            // 
            this.radFull.AutoSize = true;
            this.radFull.Checked = true;
            this.radFull.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radFull.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.radFull.Location = new System.Drawing.Point(40, 187);
            this.radFull.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.radFull.Name = "radFull";
            this.radFull.Size = new System.Drawing.Size(147, 34);
            this.radFull.TabIndex = 8;
            this.radFull.TabStop = true;
            this.radFull.Text = "Full Backup";
            this.radFull.UseVisualStyleBackColor = true;
            this.radFull.CheckedChanged += new System.EventHandler(this.radFull_CheckedChanged);
            // 
            // radIncremental
            // 
            this.radIncremental.AutoSize = true;
            this.radIncremental.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radIncremental.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.radIncremental.Location = new System.Drawing.Point(220, 187);
            this.radIncremental.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.radIncremental.Name = "radIncremental";
            this.radIncremental.Size = new System.Drawing.Size(152, 34);
            this.radIncremental.TabIndex = 9;
            this.radIncremental.Text = "Incremental";
            this.radIncremental.UseVisualStyleBackColor = true;
            this.radIncremental.CheckedChanged += new System.EventHandler(this.radIncremental_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(40, 475);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(950, 25);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.White;
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFile.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtFile.Location = new System.Drawing.Point(40, 109);
            this.txtFile.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(650, 38);
            this.txtFile.TabIndex = 11;
            this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
            // 
            // btnChonThuMuc
            // 
            this.btnChonThuMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnChonThuMuc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonThuMuc.FlatAppearance.BorderSize = 0;
            this.btnChonThuMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonThuMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChonThuMuc.ForeColor = System.Drawing.Color.White;
            this.btnChonThuMuc.Location = new System.Drawing.Point(845, 102);
            this.btnChonThuMuc.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnChonThuMuc.Name = "btnChonThuMuc";
            this.btnChonThuMuc.Size = new System.Drawing.Size(195, 42);
            this.btnChonThuMuc.TabIndex = 12;
            this.btnChonThuMuc.Text = "Chọn thư mục";
            this.btnChonThuMuc.UseVisualStyleBackColor = false;
            this.btnChonThuMuc.Click += new System.EventHandler(this.btnChonThuMuc_Click);
            // 
            // btnRestoreFolder
            // 
            this.btnRestoreFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnRestoreFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestoreFolder.FlatAppearance.BorderSize = 0;
            this.btnRestoreFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreFolder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRestoreFolder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRestoreFolder.Location = new System.Drawing.Point(690, 335);
            this.btnRestoreFolder.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnRestoreFolder.Name = "btnRestoreFolder";
            this.btnRestoreFolder.Size = new System.Drawing.Size(300, 50);
            this.btnRestoreFolder.TabIndex = 13;
            this.btnRestoreFolder.Text = "Restore thư mục";
            this.btnRestoreFolder.UseVisualStyleBackColor = false;
            this.btnRestoreFolder.Click += new System.EventHandler(this.btnRestoreFolder_Click);
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.dgvHistory);
            this.grpHistory.Controls.Add(this.btnXoaHistory);
            this.grpHistory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.grpHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.grpHistory.Location = new System.Drawing.Point(40, 530);
            this.grpHistory.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Padding = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.grpHistory.Size = new System.Drawing.Size(950, 157);
            this.grpHistory.TabIndex = 14;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "LỊCH SỬ SAO LƯU";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(8, 30);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowHeadersWidth = 62;
            this.dgvHistory.RowTemplate.Height = 28;
            this.dgvHistory.Size = new System.Drawing.Size(925, 90);
            this.dgvHistory.TabIndex = 15;
            this.dgvHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellContentClick);
            // 
            // btnXoaHistory
            // 
            this.btnXoaHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaHistory.FlatAppearance.BorderSize = 0;
            this.btnXoaHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoaHistory.ForeColor = System.Drawing.Color.Snow;
            this.btnXoaHistory.Location = new System.Drawing.Point(810, 122);
            this.btnXoaHistory.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnXoaHistory.Name = "btnXoaHistory";
            this.btnXoaHistory.Size = new System.Drawing.Size(140, 35);
            this.btnXoaHistory.TabIndex = 15;
            this.btnXoaHistory.Text = "\tXóa lịch sử";
            this.btnXoaHistory.UseVisualStyleBackColor = false;
            this.btnXoaHistory.Click += new System.EventHandler(this.btnXoaHistory_Click);
            // 
            // lblAutoBackup
            // 
            this.lblAutoBackup.AutoSize = true;
            this.lblAutoBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAutoBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblAutoBackup.Location = new System.Drawing.Point(40, 400);
            this.lblAutoBackup.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblAutoBackup.Name = "lblAutoBackup";
            this.lblAutoBackup.Size = new System.Drawing.Size(180, 30);
            this.lblAutoBackup.TabIndex = 15;
            this.lblAutoBackup.Text = "Sao lưu tự động";
            this.lblAutoBackup.Click += new System.EventHandler(this.lblAutoBackup_Click);
            // 
            // btnStartAuto
            // 
            this.btnStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnStartAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartAuto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartAuto.ForeColor = System.Drawing.Color.White;
            this.btnStartAuto.Location = new System.Drawing.Point(335, 395);
            this.btnStartAuto.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnStartAuto.Name = "btnStartAuto";
            this.btnStartAuto.Size = new System.Drawing.Size(150, 42);
            this.btnStartAuto.TabIndex = 16;
            this.btnStartAuto.Text = "Bật tự động";
            this.btnStartAuto.UseVisualStyleBackColor = false;
            this.btnStartAuto.Click += new System.EventHandler(this.btnStartAuto_Click);
            // 
            // btnStopAuto
            // 
            this.btnStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnStopAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopAuto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopAuto.ForeColor = System.Drawing.Color.Snow;
            this.btnStopAuto.Location = new System.Drawing.Point(500, 395);
            this.btnStopAuto.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnStopAuto.Name = "btnStopAuto";
            this.btnStopAuto.Size = new System.Drawing.Size(150, 42);
            this.btnStopAuto.TabIndex = 16;
            this.btnStopAuto.Text = "\tTắt tự động";
            this.btnStopAuto.UseVisualStyleBackColor = false;
            this.btnStopAuto.Click += new System.EventHandler(this.btnStopAuto_Click);
            // 
            // numMinutes
            // 
            this.numMinutes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numMinutes.Location = new System.Drawing.Point(220, 400);
            this.numMinutes.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.numMinutes.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(90, 34);
            this.numMinutes.TabIndex = 0;
            this.numMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMinutes.ValueChanged += new System.EventHandler(this.numMinutes_ValueChanged);
            // 
            // timerBackup
            // 
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.lblPassword.Location = new System.Drawing.Point(40, 225);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(193, 30);
            this.lblPassword.TabIndex = 17;
            this.lblPassword.Text = "Mật khẩu mã hóa";
            this.lblPassword.Click += new System.EventHandler(this.lblPassword_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPassword.Location = new System.Drawing.Point(40, 258);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(450, 38);
            this.txtPassword.TabIndex = 18;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chkEncrypt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(52)))), ((int)(((byte)(92)))));
            this.chkEncrypt.Location = new System.Drawing.Point(40, 296);
            this.chkEncrypt.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(184, 34);
            this.chkEncrypt.TabIndex = 19;
            this.chkEncrypt.Text = "Mã hóa dữ liệu";
            this.chkEncrypt.UseVisualStyleBackColor = true;
            this.chkEncrypt.CheckedChanged += new System.EventHandler(this.chkEncrypt_CheckedChanged);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.IncludeSubdirectories = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // btnTheoDoi
            // 
            this.btnTheoDoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTheoDoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTheoDoi.FlatAppearance.BorderSize = 0;
            this.btnTheoDoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheoDoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTheoDoi.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTheoDoi.Location = new System.Drawing.Point(665, 395);
            this.btnTheoDoi.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnTheoDoi.Name = "btnTheoDoi";
            this.btnTheoDoi.Size = new System.Drawing.Size(180, 42);
            this.btnTheoDoi.TabIndex = 20;
            this.btnTheoDoi.Text = "Bật theo dõi";
            this.btnTheoDoi.UseVisualStyleBackColor = false;
            this.btnTheoDoi.Click += new System.EventHandler(this.btnTheoDoi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(22F, 54F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1078, 724);
            this.Controls.Add(this.btnTheoDoi);
            this.Controls.Add(this.chkEncrypt);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.numMinutes);
            this.Controls.Add(this.btnStopAuto);
            this.Controls.Add(this.btnStartAuto);
            this.Controls.Add(this.lblAutoBackup);
            this.Controls.Add(this.grpHistory);
            this.Controls.Add(this.btnRestoreFolder);
            this.Controls.Add(this.btnChonThuMuc);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.radIncremental);
            this.Controls.Add(this.radFull);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTienTrinh);
            this.Controls.Add(this.lblCheDo);
            this.Controls.Add(this.lblChonFile);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnChonFile);
            this.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.MaximumSize = new System.Drawing.Size(1100, 780);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblChonFile;
        private System.Windows.Forms.Label lblCheDo;
        private System.Windows.Forms.Label lblTienTrinh;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.RadioButton radFull;
        private System.Windows.Forms.RadioButton radIncremental;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnChonThuMuc;
        private System.Windows.Forms.Button btnRestoreFolder;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.Button btnXoaHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label lblAutoBackup;
        private System.Windows.Forms.Button btnStartAuto;
        private System.Windows.Forms.Button btnStopAuto;
        private System.Windows.Forms.NumericUpDown numMinutes;
        private System.Windows.Forms.Timer timerBackup;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.TextBox txtPassword;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button btnTheoDoi;
    }
}

