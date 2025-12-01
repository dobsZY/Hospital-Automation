namespace HospitalAutomation.Views.Reports
{
    partial class DailyAppointmentsReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBoxStatistics = new System.Windows.Forms.GroupBox();
            this.txtDoctorStats = new System.Windows.Forms.TextBox();
            this.lblDoctorStatsTitle = new System.Windows.Forms.Label();
            this.txtDepartmentStats = new System.Windows.Forms.TextBox();
            this.lblDepartmentStatsTitle = new System.Windows.Forms.Label();
            this.lblScheduledAppointments = new System.Windows.Forms.Label();
            this.lblCancelledAppointments = new System.Windows.Forms.Label();
            this.lblCompletedAppointments = new System.Windows.Forms.Label();
            this.lblTotalAppointments = new System.Windows.Forms.Label();
            this.groupBoxAppointments = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.groupBoxFilter.SuspendLayout();
            this.groupBoxStatistics.SuspendLayout();
            this.groupBoxAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(253, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Günlük Randevu Raporu";
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.btnYesterday);
            this.groupBoxFilter.Controls.Add(this.btnToday);
            this.groupBoxFilter.Controls.Add(this.btnRefresh);
            this.groupBoxFilter.Controls.Add(this.dtpReportDate);
            this.groupBoxFilter.Controls.Add(this.lblDate);
            this.groupBoxFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxFilter.Location = new System.Drawing.Point(20, 60);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(950, 80);
            this.groupBoxFilter.TabIndex = 1;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Tarih Seçimi";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDate.Location = new System.Drawing.Point(20, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Tarih:";
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportDate.Location = new System.Drawing.Point(70, 27);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(120, 21);
            this.dtpReportDate.TabIndex = 1;
            this.dtpReportDate.ValueChanged += new System.EventHandler(this.DtpReportDate_ValueChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(210, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 25);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(310, 27);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(80, 25);
            this.btnToday.TabIndex = 3;
            this.btnToday.Text = "Bugün";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.BtnToday_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesterday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYesterday.ForeColor = System.Drawing.Color.White;
            this.btnYesterday.Location = new System.Drawing.Point(410, 27);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(80, 25);
            this.btnYesterday.TabIndex = 4;
            this.btnYesterday.Text = "Dün";
            this.btnYesterday.UseVisualStyleBackColor = false;
            this.btnYesterday.Click += new System.EventHandler(this.BtnYesterday_Click);
            // 
            // groupBoxStatistics
            // 
            this.groupBoxStatistics.Controls.Add(this.txtDoctorStats);
            this.groupBoxStatistics.Controls.Add(this.lblDoctorStatsTitle);
            this.groupBoxStatistics.Controls.Add(this.txtDepartmentStats);
            this.groupBoxStatistics.Controls.Add(this.lblDepartmentStatsTitle);
            this.groupBoxStatistics.Controls.Add(this.lblScheduledAppointments);
            this.groupBoxStatistics.Controls.Add(this.lblCancelledAppointments);
            this.groupBoxStatistics.Controls.Add(this.lblCompletedAppointments);
            this.groupBoxStatistics.Controls.Add(this.lblTotalAppointments);
            this.groupBoxStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxStatistics.Location = new System.Drawing.Point(20, 160);
            this.groupBoxStatistics.Name = "groupBoxStatistics";
            this.groupBoxStatistics.Size = new System.Drawing.Size(950, 140);
            this.groupBoxStatistics.TabIndex = 2;
            this.groupBoxStatistics.TabStop = false;
            this.groupBoxStatistics.Text = "Ýstatistikler";
            // 
            // lblTotalAppointments
            // 
            this.lblTotalAppointments.AutoSize = true;
            this.lblTotalAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTotalAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTotalAppointments.Location = new System.Drawing.Point(20, 30);
            this.lblTotalAppointments.Name = "lblTotalAppointments";
            this.lblTotalAppointments.Size = new System.Drawing.Size(120, 15);
            this.lblTotalAppointments.TabIndex = 0;
            this.lblTotalAppointments.Text = "Toplam Randevu: 0";
            // 
            // lblCompletedAppointments
            // 
            this.lblCompletedAppointments.AutoSize = true;
            this.lblCompletedAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCompletedAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblCompletedAppointments.Location = new System.Drawing.Point(20, 55);
            this.lblCompletedAppointments.Name = "lblCompletedAppointments";
            this.lblCompletedAppointments.Size = new System.Drawing.Size(82, 15);
            this.lblCompletedAppointments.TabIndex = 1;
            this.lblCompletedAppointments.Text = "Tamamlanan: 0";
            // 
            // lblCancelledAppointments
            // 
            this.lblCancelledAppointments.AutoSize = true;
            this.lblCancelledAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCancelledAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblCancelledAppointments.Location = new System.Drawing.Point(20, 80);
            this.lblCancelledAppointments.Name = "lblCancelledAppointments";
            this.lblCancelledAppointments.Size = new System.Drawing.Size(78, 15);
            this.lblCancelledAppointments.TabIndex = 2;
            this.lblCancelledAppointments.Text = "Ýptal Edilen: 0";
            // 
            // lblScheduledAppointments
            // 
            this.lblScheduledAppointments.AutoSize = true;
            this.lblScheduledAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblScheduledAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblScheduledAppointments.Location = new System.Drawing.Point(20, 105);
            this.lblScheduledAppointments.Name = "lblScheduledAppointments";
            this.lblScheduledAppointments.Size = new System.Drawing.Size(62, 15);
            this.lblScheduledAppointments.TabIndex = 3;
            this.lblScheduledAppointments.Text = "Bekleyen: 0";
            // 
            // lblDepartmentStatsTitle
            // 
            this.lblDepartmentStatsTitle.AutoSize = true;
            this.lblDepartmentStatsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDepartmentStatsTitle.Location = new System.Drawing.Point(300, 30);
            this.lblDepartmentStatsTitle.Name = "lblDepartmentStatsTitle";
            this.lblDepartmentStatsTitle.Size = new System.Drawing.Size(125, 15);
            this.lblDepartmentStatsTitle.TabIndex = 4;
            this.lblDepartmentStatsTitle.Text = "Bölümlere Göre Daðýlým:";
            // 
            // txtDepartmentStats
            // 
            this.txtDepartmentStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDepartmentStats.Location = new System.Drawing.Point(300, 50);
            this.txtDepartmentStats.Multiline = true;
            this.txtDepartmentStats.Name = "txtDepartmentStats";
            this.txtDepartmentStats.ReadOnly = true;
            this.txtDepartmentStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDepartmentStats.Size = new System.Drawing.Size(200, 70);
            this.txtDepartmentStats.TabIndex = 5;
            // 
            // lblDoctorStatsTitle
            // 
            this.lblDoctorStatsTitle.AutoSize = true;
            this.lblDoctorStatsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDoctorStatsTitle.Location = new System.Drawing.Point(520, 30);
            this.lblDoctorStatsTitle.Name = "lblDoctorStatsTitle";
            this.lblDoctorStatsTitle.Size = new System.Drawing.Size(125, 15);
            this.lblDoctorStatsTitle.TabIndex = 6;
            this.lblDoctorStatsTitle.Text = "Doktorlara Göre Daðýlým:";
            // 
            // txtDoctorStats
            // 
            this.txtDoctorStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDoctorStats.Location = new System.Drawing.Point(520, 50);
            this.txtDoctorStats.Multiline = true;
            this.txtDoctorStats.Name = "txtDoctorStats";
            this.txtDoctorStats.ReadOnly = true;
            this.txtDoctorStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDoctorStats.Size = new System.Drawing.Size(200, 70);
            this.txtDoctorStats.TabIndex = 7;
            // 
            // groupBoxAppointments
            // 
            this.groupBoxAppointments.Controls.Add(this.btnExport);
            this.groupBoxAppointments.Controls.Add(this.btnPrint);
            this.groupBoxAppointments.Controls.Add(this.dgvAppointments);
            this.groupBoxAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxAppointments.Location = new System.Drawing.Point(20, 320);
            this.groupBoxAppointments.Name = "groupBoxAppointments";
            this.groupBoxAppointments.Size = new System.Drawing.Size(950, 350);
            this.groupBoxAppointments.TabIndex = 3;
            this.groupBoxAppointments.TabStop = false;
            this.groupBoxAppointments.Text = "Randevu Listesi";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(20, 30);
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(900, 270);
            this.dgvAppointments.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(20, 310);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Yazdýr";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(140, 310);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Dýþa Aktar";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // DailyAppointmentsReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.groupBoxAppointments);
            this.Controls.Add(this.groupBoxStatistics);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.lblTitle);
            this.Name = "DailyAppointmentsReportForm";
            this.Text = "Günlük Randevu Raporu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBoxStatistics.ResumeLayout(false);
            this.groupBoxStatistics.PerformLayout();
            this.groupBoxAppointments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBoxStatistics;
        private System.Windows.Forms.TextBox txtDoctorStats;
        private System.Windows.Forms.Label lblDoctorStatsTitle;
        private System.Windows.Forms.TextBox txtDepartmentStats;
        private System.Windows.Forms.Label lblDepartmentStatsTitle;
        private System.Windows.Forms.Label lblScheduledAppointments;
        private System.Windows.Forms.Label lblCancelledAppointments;
        private System.Windows.Forms.Label lblCompletedAppointments;
        private System.Windows.Forms.Label lblTotalAppointments;
        private System.Windows.Forms.GroupBox groupBoxAppointments;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvAppointments;
    }
}