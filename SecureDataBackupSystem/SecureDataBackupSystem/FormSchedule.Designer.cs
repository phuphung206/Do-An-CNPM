namespace SecureDataBackupSystem
{
    partial class FormSchedule
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
            this.lblPath = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtSchedulePath = new System.Windows.Forms.TextBox();
            this.cboFrequency = new System.Windows.Forms.ComboBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.clbDays = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblTitle.Location = new System.Drawing.Point(291, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(321, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LẬP LỊCH SAO LƯU";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPath.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblPath.Location = new System.Drawing.Point(55, 72);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(311, 32);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "File / thư mục cần sao lưu";
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFrequency.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFrequency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblFrequency.Location = new System.Drawing.Point(55, 165);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(202, 32);
            this.lblFrequency.TabIndex = 2;
            this.lblFrequency.Text = "Tần suất sao lưu";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblTime.Location = new System.Drawing.Point(310, 165);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(144, 32);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "Giờ sao lưu";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblDay.Location = new System.Drawing.Point(550, 165);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(204, 32);
            this.lblDay.TabIndex = 4;
            this.lblDay.Text = "Ngày trong tuần";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblStatus.Location = new System.Drawing.Point(100, 380);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(213, 30);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Chưa bật lịch sao lưu";
            // 
            // txtSchedulePath
            // 
            this.txtSchedulePath.Location = new System.Drawing.Point(55, 105);
            this.txtSchedulePath.Name = "txtSchedulePath";
            this.txtSchedulePath.Size = new System.Drawing.Size(480, 34);
            this.txtSchedulePath.TabIndex = 6;
            // 
            // cboFrequency
            // 
            this.cboFrequency.FormattingEnabled = true;
            this.cboFrequency.Items.AddRange(new object[] {
            "Hằng ngày",
            "Mỗi 2 ngày",
            "Mỗi 3 ngày",
            "Hằng tuần"});
            this.cboFrequency.Location = new System.Drawing.Point(55, 195);
            this.cboFrequency.Name = "cboFrequency";
            this.cboFrequency.Size = new System.Drawing.Size(220, 36);
            this.cboFrequency.TabIndex = 7;
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(310, 195);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(200, 34);
            this.dtpTime.TabIndex = 8;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChooseFile.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseFile.Location = new System.Drawing.Point(540, 100);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(105, 45);
            this.btnChooseFile.TabIndex = 10;
            this.btnChooseFile.Text = "Chọn file";
            this.btnChooseFile.UseVisualStyleBackColor = false;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChooseFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChooseFolder.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnChooseFolder.Location = new System.Drawing.Point(647, 100);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(161, 45);
            this.btnChooseFolder.TabIndex = 11;
            this.btnChooseFolder.Text = "Chọn thư mục";
            this.btnChooseFolder.UseVisualStyleBackColor = false;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnSchedule
            // 
            this.btnSchedule.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSchedule.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSchedule.ForeColor = System.Drawing.Color.White;
            this.btnSchedule.Location = new System.Drawing.Point(315, 315);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(210, 60);
            this.btnSchedule.TabIndex = 12;
            this.btnSchedule.Text = "Bật lịch";
            this.btnSchedule.UseVisualStyleBackColor = false;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // clbDays
            // 
            this.clbDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.clbDays.FormattingEnabled = true;
            this.clbDays.Items.AddRange(new object[] {
            "Thứ 2",
            "Thứ 3",
            "Thứ 4",
            "Thứ 5",
            "Thứ 6",
            "Thứ 7",
            "Chủ nhật"});
            this.clbDays.Location = new System.Drawing.Point(555, 197);
            this.clbDays.Name = "clbDays";
            this.clbDays.Size = new System.Drawing.Size(250, 128);
            this.clbDays.TabIndex = 13;
            // 
            // FormSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(837, 500);
            this.Controls.Add(this.clbDays);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.cboFrequency);
            this.Controls.Add(this.txtSchedulePath);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSchedule";
            this.Text = "FormSchedule";
            this.Load += new System.EventHandler(this.FormSchedule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSchedulePath;
        private System.Windows.Forms.ComboBox cboFrequency;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.CheckedListBox clbDays;
    }
}