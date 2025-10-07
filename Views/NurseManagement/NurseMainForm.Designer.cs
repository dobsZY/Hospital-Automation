namespace HospitalAutomation.Views.NurseManagement
{
    partial class NurseMainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hastaBakýmýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vitalBulgularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilacTakibiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hemþireNotlarýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastalarýmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.günlükRandevularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.þifreDeðiþtirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çýkýþToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblQuickActions = new System.Windows.Forms.Label();
            this.btnVitalSigns = new System.Windows.Forms.Button();
            this.btnMedicationTracking = new System.Windows.Forms.Button();
            this.btnNursingNotes = new System.Windows.Forms.Button();
            this.btnPatientList = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hastaBakýmýToolStripMenuItem,
            this.hastalarýmToolStripMenuItem,
            this.randevularToolStripMenuItem,
            this.ayarlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hastaBakýmýToolStripMenuItem
            // 
            this.hastaBakýmýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vitalBulgularToolStripMenuItem,
            this.ilacTakibiToolStripMenuItem,
            this.hemþireNotlarýToolStripMenuItem});
            this.hastaBakýmýToolStripMenuItem.Name = "hastaBakýmýToolStripMenuItem";
            this.hastaBakýmýToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.hastaBakýmýToolStripMenuItem.Text = "Hasta Bakýmý";
            // 
            // vitalBulgularToolStripMenuItem
            // 
            this.vitalBulgularToolStripMenuItem.Name = "vitalBulgularToolStripMenuItem";
            this.vitalBulgularToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.vitalBulgularToolStripMenuItem.Text = "Vital Bulgular";
            this.vitalBulgularToolStripMenuItem.Click += new System.EventHandler(this.VitalBulgularToolStripMenuItem_Click);
            // 
            // ilacTakibiToolStripMenuItem
            // 
            this.ilacTakibiToolStripMenuItem.Name = "ilacTakibiToolStripMenuItem";
            this.ilacTakibiToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.ilacTakibiToolStripMenuItem.Text = "Ýlaç Takibi";
            this.ilacTakibiToolStripMenuItem.Click += new System.EventHandler(this.IlacTakibiToolStripMenuItem_Click);
            // 
            // hemþireNotlarýToolStripMenuItem
            // 
            this.hemþireNotlarýToolStripMenuItem.Name = "hemþireNotlarýToolStripMenuItem";
            this.hemþireNotlarýToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.hemþireNotlarýToolStripMenuItem.Text = "Hemþire Notlarý";
            this.hemþireNotlarýToolStripMenuItem.Click += new System.EventHandler(this.HemþireNotlarýToolStripMenuItem_Click);
            // 
            // hastalarýmToolStripMenuItem
            // 
            this.hastalarýmToolStripMenuItem.Name = "hastalarýmToolStripMenuItem";
            this.hastalarýmToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.hastalarýmToolStripMenuItem.Text = "Hastalarým";
            this.hastalarýmToolStripMenuItem.Click += new System.EventHandler(this.HastalarýmToolStripMenuItem_Click);
            // 
            // randevularToolStripMenuItem
            // 
            this.randevularToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.günlükRandevularToolStripMenuItem});
            this.randevularToolStripMenuItem.Name = "randevularToolStripMenuItem";
            this.randevularToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.randevularToolStripMenuItem.Text = "Randevular";
            // 
            // günlükRandevularToolStripMenuItem
            // 
            this.günlükRandevularToolStripMenuItem.Name = "günlükRandevularToolStripMenuItem";
            this.günlükRandevularToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.günlükRandevularToolStripMenuItem.Text = "Günlük Randevular";
            this.günlükRandevularToolStripMenuItem.Click += new System.EventHandler(this.GünlükRandevularToolStripMenuItem_Click);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilimToolStripMenuItem,
            this.þifreDeðiþtirToolStripMenuItem,
            this.çýkýþToolStripMenuItem});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            // 
            // profilimToolStripMenuItem
            // 
            this.profilimToolStripMenuItem.Name = "profilimToolStripMenuItem";
            this.profilimToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.profilimToolStripMenuItem.Text = "Profilim";
            this.profilimToolStripMenuItem.Click += new System.EventHandler(this.ProfilimToolStripMenuItem_Click);
            // 
            // þifreDeðiþtirToolStripMenuItem
            // 
            this.þifreDeðiþtirToolStripMenuItem.Name = "þifreDeðiþtirToolStripMenuItem";
            this.þifreDeðiþtirToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.þifreDeðiþtirToolStripMenuItem.Text = "Þifre Deðiþtir";
            this.þifreDeðiþtirToolStripMenuItem.Click += new System.EventHandler(this.ÞifreDeðiþtirToolStripMenuItem_Click);
            // 
            // çýkýþToolStripMenuItem
            // 
            this.çýkýþToolStripMenuItem.Name = "çýkýþToolStripMenuItem";
            this.çýkýþToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.çýkýþToolStripMenuItem.Text = "Çýkýþ";
            this.çýkýþToolStripMenuItem.Click += new System.EventHandler(this.ÇýkýþToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 674);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(130, 20);
            this.toolStripStatusLabel1.Text = "Hemþire: Test Hemþire";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabel2.Text = "Tarih:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.lblQuickActions);
            this.panel1.Controls.Add(this.btnVitalSigns);
            this.panel1.Controls.Add(this.btnMedicationTracking);
            this.panel1.Controls.Add(this.btnNursingNotes);
            this.panel1.Controls.Add(this.btnPatientList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 646);
            this.panel1.TabIndex = 2;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblWelcome.Location = new System.Drawing.Point(350, 100);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(500, 37);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Hemþire Paneline Hoþgeldiniz";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDateTime.ForeColor = System.Drawing.Color.Gray;
            this.lblDateTime.Location = new System.Drawing.Point(500, 150);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(125, 24);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "Tarih ve Saat";
            // 
            // lblQuickActions
            // 
            this.lblQuickActions.AutoSize = true;
            this.lblQuickActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblQuickActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblQuickActions.Location = new System.Drawing.Point(500, 230);
            this.lblQuickActions.Name = "lblQuickActions";
            this.lblQuickActions.Size = new System.Drawing.Size(160, 26);
            this.lblQuickActions.TabIndex = 2;
            this.lblQuickActions.Text = "Hýzlý Ýþlemler";
            // 
            // btnVitalSigns
            // 
            this.btnVitalSigns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnVitalSigns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVitalSigns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnVitalSigns.ForeColor = System.Drawing.Color.White;
            this.btnVitalSigns.Location = new System.Drawing.Point(300, 300);
            this.btnVitalSigns.Name = "btnVitalSigns";
            this.btnVitalSigns.Size = new System.Drawing.Size(150, 50);
            this.btnVitalSigns.TabIndex = 3;
            this.btnVitalSigns.Text = "Vital Bulgular";
            this.btnVitalSigns.UseVisualStyleBackColor = false;
            this.btnVitalSigns.Click += new System.EventHandler(this.BtnVitalSigns_Click);
            // 
            // btnMedicationTracking
            // 
            this.btnMedicationTracking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnMedicationTracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicationTracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMedicationTracking.ForeColor = System.Drawing.Color.White;
            this.btnMedicationTracking.Location = new System.Drawing.Point(475, 300);
            this.btnMedicationTracking.Name = "btnMedicationTracking";
            this.btnMedicationTracking.Size = new System.Drawing.Size(150, 50);
            this.btnMedicationTracking.TabIndex = 4;
            this.btnMedicationTracking.Text = "Ýlaç Takibi";
            this.btnMedicationTracking.UseVisualStyleBackColor = false;
            this.btnMedicationTracking.Click += new System.EventHandler(this.BtnMedicationTracking_Click);
            // 
            // btnNursingNotes
            // 
            this.btnNursingNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNursingNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNursingNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnNursingNotes.ForeColor = System.Drawing.Color.White;
            this.btnNursingNotes.Location = new System.Drawing.Point(650, 300);
            this.btnNursingNotes.Name = "btnNursingNotes";
            this.btnNursingNotes.Size = new System.Drawing.Size(150, 50);
            this.btnNursingNotes.TabIndex = 5;
            this.btnNursingNotes.Text = "Hemþire Notlarý";
            this.btnNursingNotes.UseVisualStyleBackColor = false;
            this.btnNursingNotes.Click += new System.EventHandler(this.BtnNursingNotes_Click);
            // 
            // btnPatientList
            // 
            this.btnPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPatientList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPatientList.ForeColor = System.Drawing.Color.White;
            this.btnPatientList.Location = new System.Drawing.Point(500, 380);
            this.btnPatientList.Name = "btnPatientList";
            this.btnPatientList.Size = new System.Drawing.Size(150, 50);
            this.btnPatientList.TabIndex = 6;
            this.btnPatientList.Text = "Hasta Listesi";
            this.btnPatientList.UseVisualStyleBackColor = false;
            this.btnPatientList.Click += new System.EventHandler(this.BtnPatientList_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // NurseMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NurseMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hemþire Paneli - Hastane Otomasyon Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NurseMainForm_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem hastaBakýmýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vitalBulgularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilacTakibiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hemþireNotlarýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastalarýmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem günlükRandevularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem þifreDeðiþtirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çýkýþToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblQuickActions;
        private System.Windows.Forms.Button btnVitalSigns;
        private System.Windows.Forms.Button btnMedicationTracking;
        private System.Windows.Forms.Button btnNursingNotes;
        private System.Windows.Forms.Button btnPatientList;
        private System.Windows.Forms.Timer timer1;
    }
}