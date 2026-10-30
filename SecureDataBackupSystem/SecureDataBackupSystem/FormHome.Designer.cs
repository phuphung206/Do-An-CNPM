namespace SecureDataBackupSystem
{
    partial class FormHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.panelMenu.Controls.Add(this.btnSchedule);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Controls.Add(this.btnHistory);
            this.panelMenu.Controls.Add(this.btnBackup);
            this.panelMenu.Controls.Add(this.btnRestore);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(199, 529);
            this.panelMenu.TabIndex = 0;
            // 
            // btnSchedule
            // 
            this.btnSchedule.BackColor = System.Drawing.Color.White;
            this.btnSchedule.FlatAppearance.BorderSize = 0;
            this.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSchedule.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnSchedule.Location = new System.Drawing.Point(20, 383);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(159, 52);
            this.btnSchedule.TabIndex = 5;
            this.btnSchedule.Text = "Lập lịch";
            this.btnSchedule.UseVisualStyleBackColor = false;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.White;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnSettings.Location = new System.Drawing.Point(20, 315);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(159, 52);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Cài đặt";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.White;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnHome.Location = new System.Drawing.Point(20, 42);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(159, 47);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Trang chủ";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.White;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnHistory.Location = new System.Drawing.Point(20, 247);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(159, 47);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.White;
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnBackup.Location = new System.Drawing.Point(20, 110);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(159, 47);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Sao lưu";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnRestore.Location = new System.Drawing.Point(20, 178);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(159, 47);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Khôi phục";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.picHome);
            this.panelContent.Controls.Add(this.lblDescription);
            this.panelContent.Controls.Add(this.lblWelcome);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(199, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(818, 529);
            this.panelContent.TabIndex = 1;
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.Color.Transparent;
            this.picHome.Image = global::SecureDataBackupSystem.Properties.Resources.saved_image_file_00000000c4d08207820be3c2a187f5aa;
            this.picHome.Location = new System.Drawing.Point(160, 164);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(500, 285);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHome.TabIndex = 2;
            this.picHome.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(105)))));
            this.lblDescription.Location = new System.Drawing.Point(255, 87);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(450, 30);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Quản lý, sao lưu và khôi phục dữ liệu an toàn.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescription.Click += new System.EventHandler(this.lblDescription_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblWelcome.Location = new System.Drawing.Point(147, 23);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(756, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "CHÀO MỪNG ĐẾN HỆ THỐNG SAO LƯU DỮ LIỆU";
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1017, 529);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Data Backup System";
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Button btnSchedule;
    }
}