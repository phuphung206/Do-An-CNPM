namespace SecureDataBackupSystem
{
    partial class FormRestore
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.cboBackup = new System.Windows.Forms.ComboBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.progressRestore = new System.Windows.Forms.ProgressBar();
            this.txtRestorePath = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.lblBackupList = new System.Windows.Forms.Label();
            this.lblRestorePath = new System.Windows.Forms.Label();
            this.lblDecryptPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblTitle.Location = new System.Drawing.Point(291, 26);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(451, 73);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KHÔI PHỤC DỮ LIỆU";
            // 
            // cboBackup
            // 
            this.cboBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBackup.FormattingEnabled = true;
            this.cboBackup.Location = new System.Drawing.Point(65, 85);
            this.cboBackup.Margin = new System.Windows.Forms.Padding(4);
            this.cboBackup.Name = "cboBackup";
            this.cboBackup.Size = new System.Drawing.Size(705, 36);
            this.cboBackup.TabIndex = 1;
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseFolder.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseFolder.Location = new System.Drawing.Point(590, 140);
            this.btnChooseFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(180, 45);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "Chọn nơi lưu";
            this.btnChooseFolder.UseVisualStyleBackColor = false;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click_1);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(265, 305);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(310, 55);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.Text = "Bắt đầu khôi phục";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // progressRestore
            // 
            this.progressRestore.Location = new System.Drawing.Point(65, 390);
            this.progressRestore.Margin = new System.Windows.Forms.Padding(4);
            this.progressRestore.Name = "progressRestore";
            this.progressRestore.Size = new System.Drawing.Size(710, 25);
            this.progressRestore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressRestore.TabIndex = 4;
            // 
            // txtRestorePath
            // 
            this.txtRestorePath.Location = new System.Drawing.Point(65, 145);
            this.txtRestorePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtRestorePath.Name = "txtRestorePath";
            this.txtRestorePath.Size = new System.Drawing.Size(510, 34);
            this.txtRestorePath.TabIndex = 5;
            this.txtRestorePath.TextChanged += new System.EventHandler(this.txtRestorePath_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(65, 215);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(710, 34);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblStatus.Location = new System.Drawing.Point(65, 430);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(181, 28);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Sẵn sàng khôi phục";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.chkShowPassword.Location = new System.Drawing.Point(65, 255);
            this.chkShowPassword.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(159, 32);
            this.chkShowPassword.TabIndex = 8;
            this.chkShowPassword.Text = "Hiện mật khẩu";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // lblBackupList
            // 
            this.lblBackupList.AutoSize = true;
            this.lblBackupList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblBackupList.Location = new System.Drawing.Point(65, 58);
            this.lblBackupList.Name = "lblBackupList";
            this.lblBackupList.Size = new System.Drawing.Size(176, 28);
            this.lblBackupList.TabIndex = 9;
            this.lblBackupList.Text = "Chọn bản sao lưu";
            this.lblBackupList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRestorePath
            // 
            this.lblRestorePath.AutoSize = true;
            this.lblRestorePath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestorePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblRestorePath.Location = new System.Drawing.Point(65, 120);
            this.lblRestorePath.Name = "lblRestorePath";
            this.lblRestorePath.Size = new System.Drawing.Size(158, 28);
            this.lblRestorePath.TabIndex = 10;
            this.lblRestorePath.Text = "Vị trí khôi phục";
            this.lblRestorePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDecryptPassword
            // 
            this.lblDecryptPassword.AutoSize = true;
            this.lblDecryptPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecryptPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblDecryptPassword.Location = new System.Drawing.Point(65, 190);
            this.lblDecryptPassword.Name = "lblDecryptPassword";
            this.lblDecryptPassword.Size = new System.Drawing.Size(178, 28);
            this.lblDecryptPassword.TabIndex = 11;
            this.lblDecryptPassword.Text = "Mật khẩu giải mã";
            this.lblDecryptPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 500);
            this.Controls.Add(this.lblDecryptPassword);
            this.Controls.Add(this.lblRestorePath);
            this.Controls.Add(this.lblBackupList);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtRestorePath);
            this.Controls.Add(this.progressRestore);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.cboBackup);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormRestore";
            this.Text = "FormRestore";
            this.Load += new System.EventHandler(this.FormRestore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cboBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.ProgressBar progressRestore;
        private System.Windows.Forms.TextBox txtRestorePath;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblBackupList;
        private System.Windows.Forms.Label lblRestorePath;
        private System.Windows.Forms.Label lblDecryptPassword;
    }
}