namespace SecureDataBackupSystem
{
    partial class FormSettings
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStartAuto = new System.Windows.Forms.Button();
            this.btnStopAuto = new System.Windows.Forms.Button();
            this.btnTheoDoi = new System.Windows.Forms.Button();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.btnChooseAutoFolder = new System.Windows.Forms.Button();
            this.txtAutoPath = new System.Windows.Forms.TextBox();
            this.lblFolderMonitor = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.StartFolderMonitoring = new System.Windows.Forms.Button();
            this.btnChooseMonitorFile = new System.Windows.Forms.Button();
            this.btnChooseAutoFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblTitle.Location = new System.Drawing.Point(240, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(340, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CÀI ĐẶT HỆ THỐNG";
            // 
            // lblMinutes
            // 
            this.lblMinutes.BackColor = System.Drawing.Color.White;
            this.lblMinutes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMinutes.Location = new System.Drawing.Point(20, 35);
            this.lblMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(222, 25);
            this.lblMinutes.TabIndex = 1;
            this.lblMinutes.Text = "Thời gian sao lưu (phút)";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblStatus.Location = new System.Drawing.Point(35, 466);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(500, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Trạng thái: Chưa kích hoạt";
            // 
            // btnStartAuto
            // 
            this.btnStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnStartAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartAuto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartAuto.ForeColor = System.Drawing.Color.White;
            this.btnStartAuto.Location = new System.Drawing.Point(510, 30);
            this.btnStartAuto.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartAuto.Name = "btnStartAuto";
            this.btnStartAuto.Size = new System.Drawing.Size(100, 35);
            this.btnStartAuto.TabIndex = 3;
            this.btnStartAuto.Text = "Bật ";
            this.btnStartAuto.UseVisualStyleBackColor = false;
            this.btnStartAuto.Click += new System.EventHandler(this.btnStartAuto_Click);
            // 
            // btnStopAuto
            // 
            this.btnStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnStopAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopAuto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopAuto.ForeColor = System.Drawing.Color.White;
            this.btnStopAuto.Location = new System.Drawing.Point(625, 30);
            this.btnStopAuto.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopAuto.Name = "btnStopAuto";
            this.btnStopAuto.Size = new System.Drawing.Size(100, 35);
            this.btnStopAuto.TabIndex = 4;
            this.btnStopAuto.Text = "Tắt ";
            this.btnStopAuto.UseVisualStyleBackColor = false;
            this.btnStopAuto.Click += new System.EventHandler(this.btnStopAuto_Click);
            // 
            // btnTheoDoi
            // 
            this.btnTheoDoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTheoDoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTheoDoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheoDoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTheoDoi.ForeColor = System.Drawing.Color.White;
            this.btnTheoDoi.Location = new System.Drawing.Point(510, 105);
            this.btnTheoDoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnTheoDoi.Name = "btnTheoDoi";
            this.btnTheoDoi.Size = new System.Drawing.Size(220, 40);
            this.btnTheoDoi.TabIndex = 5;
            this.btnTheoDoi.Text = "Bật theo dõi ";
            this.btnTheoDoi.UseVisualStyleBackColor = false;
            this.btnTheoDoi.Click += new System.EventHandler(this.btnTheoDoi_Click);
            // 
            // numMinutes
            // 
            this.numMinutes.Location = new System.Drawing.Point(260, 30);
            this.numMinutes.Margin = new System.Windows.Forms.Padding(4);
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
            this.numMinutes.Size = new System.Drawing.Size(110, 37);
            this.numMinutes.TabIndex = 6;
            this.numMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // timerBackup
            // 
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // btnChooseAutoFolder
            // 
            this.btnChooseAutoFolder.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseAutoFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseAutoFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAutoFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseAutoFolder.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseAutoFolder.Location = new System.Drawing.Point(20, 120);
            this.btnChooseAutoFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseAutoFolder.Name = "btnChooseAutoFolder";
            this.btnChooseAutoFolder.Size = new System.Drawing.Size(220, 40);
            this.btnChooseAutoFolder.TabIndex = 7;
            this.btnChooseAutoFolder.Text = "Chọn thư mục ";
            this.btnChooseAutoFolder.UseVisualStyleBackColor = false;
            this.btnChooseAutoFolder.Click += new System.EventHandler(this.btnChooseAutoFolder_Click);
            // 
            // txtAutoPath
            // 
            this.txtAutoPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAutoPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAutoPath.Location = new System.Drawing.Point(20, 80);
            this.txtAutoPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoPath.Name = "txtAutoPath";
            this.txtAutoPath.Size = new System.Drawing.Size(720, 31);
            this.txtAutoPath.TabIndex = 8;
            // 
            // lblFolderMonitor
            // 
            this.lblFolderMonitor.AutoSize = true;
            this.lblFolderMonitor.BackColor = System.Drawing.Color.White;
            this.lblFolderMonitor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFolderMonitor.Location = new System.Drawing.Point(20, 30);
            this.lblFolderMonitor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolderMonitor.Name = "lblFolderMonitor";
            this.lblFolderMonitor.Size = new System.Drawing.Size(166, 28);
            this.lblFolderMonitor.TabIndex = 9;
            this.lblFolderMonitor.Text = "Thư mục theo dõi";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFolderPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtFolderPath.Location = new System.Drawing.Point(20, 60);
            this.txtFolderPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(720, 31);
            this.txtFolderPath.TabIndex = 10;
            // 
            // StartFolderMonitoring
            // 
            this.StartFolderMonitoring.BackColor = System.Drawing.Color.LightSteelBlue;
            this.StartFolderMonitoring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartFolderMonitoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartFolderMonitoring.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.StartFolderMonitoring.ForeColor = System.Drawing.Color.MidnightBlue;
            this.StartFolderMonitoring.Location = new System.Drawing.Point(20, 105);
            this.StartFolderMonitoring.Margin = new System.Windows.Forms.Padding(4);
            this.StartFolderMonitoring.Name = "StartFolderMonitoring";
            this.StartFolderMonitoring.Size = new System.Drawing.Size(220, 40);
            this.StartFolderMonitoring.TabIndex = 11;
            this.StartFolderMonitoring.Text = "Chọn thư mục";
            this.StartFolderMonitoring.UseVisualStyleBackColor = false;
            this.StartFolderMonitoring.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnChooseMonitorFile
            // 
            this.btnChooseMonitorFile.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseMonitorFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseMonitorFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseMonitorFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChooseMonitorFile.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseMonitorFile.Location = new System.Drawing.Point(255, 105);
            this.btnChooseMonitorFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseMonitorFile.Name = "btnChooseMonitorFile";
            this.btnChooseMonitorFile.Size = new System.Drawing.Size(220, 40);
            this.btnChooseMonitorFile.TabIndex = 12;
            this.btnChooseMonitorFile.Text = "chọn file";
            this.btnChooseMonitorFile.UseVisualStyleBackColor = false;
            this.btnChooseMonitorFile.Click += new System.EventHandler(this.btnChooseMonitorFile_Click);
            // 
            // btnChooseAutoFile
            // 
            this.btnChooseAutoFile.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseAutoFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseAutoFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAutoFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseAutoFile.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseAutoFile.Location = new System.Drawing.Point(255, 120);
            this.btnChooseAutoFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseAutoFile.Name = "btnChooseAutoFile";
            this.btnChooseAutoFile.Size = new System.Drawing.Size(200, 40);
            this.btnChooseAutoFile.TabIndex = 13;
            this.btnChooseAutoFile.Text = "Chọn file ";
            this.btnChooseAutoFile.UseVisualStyleBackColor = false;
            this.btnChooseAutoFile.Click += new System.EventHandler(this.btnChooseAutoFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnChooseAutoFile);
            this.groupBox1.Controls.Add(this.lblMinutes);
            this.groupBox1.Controls.Add(this.numMinutes);
            this.groupBox1.Controls.Add(this.btnStartAuto);
            this.groupBox1.Controls.Add(this.btnStopAuto);
            this.groupBox1.Controls.Add(this.txtAutoPath);
            this.groupBox1.Controls.Add(this.btnChooseAutoFolder);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.groupBox1.Location = new System.Drawing.Point(20, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 190);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SAO LƯU TỰ ĐỘNG";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnTheoDoi);
            this.groupBox2.Controls.Add(this.btnChooseMonitorFile);
            this.groupBox2.Controls.Add(this.lblFolderMonitor);
            this.groupBox2.Controls.Add(this.StartFolderMonitoring);
            this.groupBox2.Controls.Add(this.txtFolderPath);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.groupBox2.Location = new System.Drawing.Point(20, 287);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 168);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "THEO DÕI DỮ LIỆU";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(950, 500);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStartAuto;
        private System.Windows.Forms.Button btnStopAuto;
        private System.Windows.Forms.Button btnTheoDoi;
        private System.Windows.Forms.NumericUpDown numMinutes;
        private System.Windows.Forms.Timer timerBackup;
        private System.Windows.Forms.Button btnChooseAutoFolder;
        private System.Windows.Forms.TextBox txtAutoPath;
        private System.Windows.Forms.Label lblFolderMonitor;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button StartFolderMonitoring;
        private System.Windows.Forms.Button btnChooseMonitorFile;
        private System.Windows.Forms.Button btnChooseAutoFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}