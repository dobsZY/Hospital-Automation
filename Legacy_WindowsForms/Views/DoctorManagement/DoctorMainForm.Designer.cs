namespace HospitalAutomation.Views.DoctorManagement
{
    partial class DoctorMainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.randevularýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bugünkürRandevularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tümRandevularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastalarýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.týbbiKayýtlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kayýtlarýGörToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yeniKayýtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hesabýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çýkýþToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewMedicalRecord = new System.Windows.Forms.Button();
            this.btnMyPatients = new System.Windows.Forms.Button();
            this.btnTodayAppointments = new System.Windows.Forms.Button();
            this.lblQuickActions = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randevularýmToolStripMenuItem,
            this.hastalarýmToolStripMenuItem,
            this.týbbiKayýtlarToolStripMenuItem,
            this.hesabýmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // randevularýmToolStripMenuItem
            // 
            this.randevularýmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bugünkürRandevularToolStripMenuItem,
            this.tümRandevularToolStripMenuItem});
            this.randevularýmToolStripMenuItem.Name = "randevularýmToolStripMenuItem";
            this.randevularýmToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.randevularýmToolStripMenuItem.Text = "Randevularým";
            // 
            // bugünkürRandevularToolStripMenuItem
            // 
            this.bugünkürRandevularToolStripMenuItem.Name = "bugünkürRandevularToolStripMenuItem";
            this.bugünkürRandevularToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.bugünkürRandevularToolStripMenuItem.Text = "Bugünkü Randevular";
            this.bugünkürRandevularToolStripMenuItem.Click += new System.EventHandler(this.RandevularýmToolStripMenuItem_Click);
            // 
            // tümRandevularToolStripMenuItem
            // 
            this.tümRandevularToolStripMenuItem.Name = "tümRandevularToolStripMenuItem";
            this.tümRandevularToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.tümRandevularToolStripMenuItem.Text = "Tüm Randevular";
            this.tümRandevularToolStripMenuItem.Click += new System.EventHandler(this.RandevularýmToolStripMenuItem_Click);
            // 
            // hastalarýmToolStripMenuItem
            // 
            this.hastalarýmToolStripMenuItem.Name = "hastalarýmToolStripMenuItem";
            this.hastalarýmToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.hastalarýmToolStripMenuItem.Text = "Hastalarým";
            this.hastalarýmToolStripMenuItem.Click += new System.EventHandler(this.HastalarýmToolStripMenuItem_Click);
            // 
            // týbbiKayýtlarToolStripMenuItem
            // 
            this.týbbiKayýtlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kayýtlarýGörToolStripMenuItem,
            this.yeniKayýtToolStripMenuItem});
            this.týbbiKayýtlarToolStripMenuItem.Name = "týbbiKayýtlarToolStripMenuItem";
            this.týbbiKayýtlarToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.týbbiKayýtlarToolStripMenuItem.Text = "Týbbi Kayýtlar";
            // 
            // kayýtlarýGörToolStripMenuItem
            // 
            this.kayýtlarýGörToolStripMenuItem.Name = "kayýtlarýGörToolStripMenuItem";
            this.kayýtlarýGörToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.kayýtlarýGörToolStripMenuItem.Text = "Kayýtlarý Gör";
            this.kayýtlarýGörToolStripMenuItem.Click += new System.EventHandler(this.TýbbiKayýtToolStripMenuItem_Click);
            // 
            // yeniKayýtToolStripMenuItem
            // 
            this.yeniKayýtToolStripMenuItem.Name = "yeniKayýtToolStripMenuItem";
            this.yeniKayýtToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.yeniKayýtToolStripMenuItem.Text = "Yeni Kayýt";
            this.yeniKayýtToolStripMenuItem.Click += new System.EventHandler(this.YeniKayýtToolStripMenuItem_Click);
            // 
            // hesabýmToolStripMenuItem
            // 
            this.hesabýmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilimToolStripMenuItem,
            this.çýkýþToolStripMenuItem});
            this.hesabýmToolStripMenuItem.Name = "hesabýmToolStripMenuItem";
            this.hesabýmToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.hesabýmToolStripMenuItem.Text = "Hesabým";
            // 
            // profilimToolStripMenuItem
            // 
            this.profilimToolStripMenuItem.Name = "profilimToolStripMenuItem";
            this.profilimToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.profilimToolStripMenuItem.Text = "Profilim";
            this.profilimToolStripMenuItem.Click += new System.EventHandler(this.ProfilimToolStripMenuItem_Click);
            // 
            // çýkýþToolStripMenuItem
            // 
            this.çýkýþToolStripMenuItem.Name = "çýkýþToolStripMenuItem";
            this.çýkýþToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.çýkýþToolStripMenuItem.Text = "Çýkýþ";
            this.çýkýþToolStripMenuItem.Click += new System.EventHandler(this.ÇýkýþToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnNewMedicalRecord);
            this.panel1.Controls.Add(this.btnMyPatients);
            this.panel1.Controls.Add(this.btnTodayAppointments);
            this.panel1.Controls.Add(this.lblQuickActions);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(50, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 600);
            this.panel1.TabIndex = 2;
            // 
            // btnNewMedicalRecord
            // 
            this.btnNewMedicalRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnNewMedicalRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewMedicalRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnNewMedicalRecord.ForeColor = System.Drawing.Color.White;
            this.btnNewMedicalRecord.Location = new System.Drawing.Point(750, 300);
            this.btnNewMedicalRecord.Name = "btnNewMedicalRecord";
            this.btnNewMedicalRecord.Size = new System.Drawing.Size(200, 60);
            this.btnNewMedicalRecord.TabIndex = 5;
            this.btnNewMedicalRecord.Text = "Yeni Týbbi Kayýt";
            this.btnNewMedicalRecord.UseVisualStyleBackColor = false;
            this.btnNewMedicalRecord.Click += new System.EventHandler(this.BtnNewMedicalRecord_Click);
            // 
            // btnMyPatients
            // 
            this.btnMyPatients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnMyPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyPatients.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnMyPatients.ForeColor = System.Drawing.Color.Black;
            this.btnMyPatients.Location = new System.Drawing.Point(450, 300);
            this.btnMyPatients.Name = "btnMyPatients";
            this.btnMyPatients.Size = new System.Drawing.Size(200, 60);
            this.btnMyPatients.TabIndex = 4;
            this.btnMyPatients.Text = "Hastalarým";
            this.btnMyPatients.UseVisualStyleBackColor = false;
            this.btnMyPatients.Click += new System.EventHandler(this.BtnMyPatients_Click);
            // 
            // btnTodayAppointments
            // 
            this.btnTodayAppointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTodayAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTodayAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnTodayAppointments.ForeColor = System.Drawing.Color.White;
            this.btnTodayAppointments.Location = new System.Drawing.Point(150, 300);
            this.btnTodayAppointments.Name = "btnTodayAppointments";
            this.btnTodayAppointments.Size = new System.Drawing.Size(200, 60);
            this.btnTodayAppointments.TabIndex = 3;
            this.btnTodayAppointments.Text = "Bugünkü Randevular";
            this.btnTodayAppointments.UseVisualStyleBackColor = false;
            this.btnTodayAppointments.Click += new System.EventHandler(this.BtnTodayAppointments_Click);
            // 
            // lblQuickActions
            // 
            this.lblQuickActions.AutoSize = true;
            this.lblQuickActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblQuickActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblQuickActions.Location = new System.Drawing.Point(450, 220);
            this.lblQuickActions.Name = "lblQuickActions";
            this.lblQuickActions.Size = new System.Drawing.Size(165, 29);
            this.lblQuickActions.TabIndex = 2;
            this.lblQuickActions.Text = "Hýzlý Ýþlemler";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblDateTime.Location = new System.Drawing.Point(50, 150);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(64, 25);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "Tarih:";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblWelcome.Location = new System.Drawing.Point(50, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(259, 36);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Hoþ Geldiniz, ...";
            // 
            // DoctorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DoctorMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doktor Paneli - Hastane Otomasyon Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DoctorMainForm_FormClosed);
            this.MdiChildActivate += new System.EventHandler(this.DoctorMainForm_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem randevularýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bugünkürRandevularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tümRandevularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastalarýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem týbbiKayýtlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kayýtlarýGörToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yeniKayýtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hesabýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çýkýþToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblQuickActions;
        private System.Windows.Forms.Button btnTodayAppointments;
        private System.Windows.Forms.Button btnMyPatients;
        private System.Windows.Forms.Button btnNewMedicalRecord;
    }
}