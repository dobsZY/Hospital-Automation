namespace HospitalAutomation.Views.PatientPortal
{
    partial class PatientMainForm
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
            this.yeniRandevuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevuGeçmiþiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.týbbiKayýtlarýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hesabýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bilgileriniGüncelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çýkýþToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMedicalRecords = new System.Windows.Forms.Button();
            this.btnNewAppointment = new System.Windows.Forms.Button();
            this.btnMyAppointments = new System.Windows.Forms.Button();
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
            this.týbbiKayýtlarýmToolStripMenuItem,
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
            this.yeniRandevuToolStripMenuItem,
            this.randevuGeçmiþiToolStripMenuItem});
            this.randevularýmToolStripMenuItem.Name = "randevularýmToolStripMenuItem";
            this.randevularýmToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.randevularýmToolStripMenuItem.Text = "Randevularým";
            // 
            // yeniRandevuToolStripMenuItem
            // 
            this.yeniRandevuToolStripMenuItem.Name = "yeniRandevuToolStripMenuItem";
            this.yeniRandevuToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.yeniRandevuToolStripMenuItem.Text = "Yeni Randevu";
            this.yeniRandevuToolStripMenuItem.Click += new System.EventHandler(this.YeniRandevuToolStripMenuItem_Click);
            // 
            // randevuGeçmiþiToolStripMenuItem
            // 
            this.randevuGeçmiþiToolStripMenuItem.Name = "randevuGeçmiþiToolStripMenuItem";
            this.randevuGeçmiþiToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.randevuGeçmiþiToolStripMenuItem.Text = "Randevu Geçmiþi";
            this.randevuGeçmiþiToolStripMenuItem.Click += new System.EventHandler(this.RandevularýmToolStripMenuItem_Click);
            // 
            // týbbiKayýtlarýmToolStripMenuItem
            // 
            this.týbbiKayýtlarýmToolStripMenuItem.Name = "týbbiKayýtlarýmToolStripMenuItem";
            this.týbbiKayýtlarýmToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.týbbiKayýtlarýmToolStripMenuItem.Text = "Týbbi Kayýtlarým";
            this.týbbiKayýtlarýmToolStripMenuItem.Click += new System.EventHandler(this.TýbbiKayýtlarýmToolStripMenuItem_Click);
            // 
            // hesabýmToolStripMenuItem
            // 
            this.hesabýmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bilgileriniGüncelleToolStripMenuItem,
            this.çýkýþToolStripMenuItem});
            this.hesabýmToolStripMenuItem.Name = "hesabýmToolStripMenuItem";
            this.hesabýmToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.hesabýmToolStripMenuItem.Text = "Hesabým";
            // 
            // bilgileriniGüncelleToolStripMenuItem
            // 
            this.bilgileriniGüncelleToolStripMenuItem.Name = "bilgileriniGüncelleToolStripMenuItem";
            this.bilgileriniGüncelleToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.bilgileriniGüncelleToolStripMenuItem.Text = "Bilgilerimi Güncelle";
            this.bilgileriniGüncelleToolStripMenuItem.Click += new System.EventHandler(this.BilgileriniGüncelle_Click);
            // 
            // çýkýþToolStripMenuItem
            // 
            this.çýkýþToolStripMenuItem.Name = "çýkýþToolStripMenuItem";
            this.çýkýþToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
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
            this.panel1.Controls.Add(this.btnMedicalRecords);
            this.panel1.Controls.Add(this.btnNewAppointment);
            this.panel1.Controls.Add(this.btnMyAppointments);
            this.panel1.Controls.Add(this.lblQuickActions);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(50, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 600);
            this.panel1.TabIndex = 2;
            // 
            // btnMedicalRecords
            // 
            this.btnMedicalRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnMedicalRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnMedicalRecords.ForeColor = System.Drawing.Color.White;
            this.btnMedicalRecords.Location = new System.Drawing.Point(750, 300);
            this.btnMedicalRecords.Name = "btnMedicalRecords";
            this.btnMedicalRecords.Size = new System.Drawing.Size(200, 60);
            this.btnMedicalRecords.TabIndex = 5;
            this.btnMedicalRecords.Text = "Týbbi Kayýtlarým";
            this.btnMedicalRecords.UseVisualStyleBackColor = false;
            this.btnMedicalRecords.Click += new System.EventHandler(this.BtnMedicalRecords_Click);
            // 
            // btnNewAppointment
            // 
            this.btnNewAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnNewAppointment.ForeColor = System.Drawing.Color.Black;
            this.btnNewAppointment.Location = new System.Drawing.Point(450, 300);
            this.btnNewAppointment.Name = "btnNewAppointment";
            this.btnNewAppointment.Size = new System.Drawing.Size(200, 60);
            this.btnNewAppointment.TabIndex = 4;
            this.btnNewAppointment.Text = "Yeni Randevu";
            this.btnNewAppointment.UseVisualStyleBackColor = false;
            this.btnNewAppointment.Click += new System.EventHandler(this.BtnNewAppointment_Click);
            // 
            // btnMyAppointments
            // 
            this.btnMyAppointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnMyAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnMyAppointments.ForeColor = System.Drawing.Color.White;
            this.btnMyAppointments.Location = new System.Drawing.Point(150, 300);
            this.btnMyAppointments.Name = "btnMyAppointments";
            this.btnMyAppointments.Size = new System.Drawing.Size(200, 60);
            this.btnMyAppointments.TabIndex = 3;
            this.btnMyAppointments.Text = "Randevularým";
            this.btnMyAppointments.UseVisualStyleBackColor = false;
            this.btnMyAppointments.Click += new System.EventHandler(this.BtnMyAppointments_Click);
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
            // PatientMainForm
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
            this.Name = "PatientMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hasta Portalý - Hastane Otomasyon Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatientMainForm_FormClosed);
            this.MdiChildActivate += new System.EventHandler(this.PatientMainForm_MdiChildActivate);
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
        private System.Windows.Forms.ToolStripMenuItem yeniRandevuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevuGeçmiþiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem týbbiKayýtlarýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hesabýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bilgileriniGüncelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çýkýþToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblQuickActions;
        private System.Windows.Forms.Button btnMyAppointments;
        private System.Windows.Forms.Button btnNewAppointment;
        private System.Windows.Forms.Button btnMedicalRecords;
    }
}