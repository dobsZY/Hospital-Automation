namespace HospitalAutomation.Views.DoctorManagement
{
    partial class DoctorNewMedicalRecordForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panelFeatures = new System.Windows.Forms.Panel();
            this.lblFeatures = new System.Windows.Forms.Label();
            this.lblFeature1 = new System.Windows.Forms.Label();
            this.lblFeature2 = new System.Windows.Forms.Label();
            this.lblFeature3 = new System.Windows.Forms.Label();
            this.lblFeature4 = new System.Windows.Forms.Label();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.panelFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.btnGoBack);
            this.panelMain.Controls.Add(this.panelFeatures);
            this.panelMain.Controls.Add(this.lblDescription);
            this.panelMain.Controls.Add(this.lblMessage);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.pictureBoxIcon);
            this.panelMain.Location = new System.Drawing.Point(200, 100);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 500);
            this.panelMain.TabIndex = 0;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pictureBoxIcon.Location = new System.Drawing.Point(275, 30);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(170, 100);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Yeni Týbbi Kayýt";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblMessage.Location = new System.Drawing.Point(220, 150);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(160, 24);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "?? YAKINDA! ??";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblDescription.Location = new System.Drawing.Point(50, 190);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(500, 60);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Yeni týbbi kayýt oluþturma modülü aktif olarak geliþtirilmektedir.\r\nBu modül ile hastalarýnýz için detaylý týbbi kayýtlar\r\noluþturabilecek ve mevcut kayýtlarý güncelleyebileceksiniz.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFeatures
            // 
            this.panelFeatures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelFeatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFeatures.Controls.Add(this.lblFeature4);
            this.panelFeatures.Controls.Add(this.lblFeature3);
            this.panelFeatures.Controls.Add(this.lblFeature2);
            this.panelFeatures.Controls.Add(this.lblFeature1);
            this.panelFeatures.Controls.Add(this.lblFeatures);
            this.panelFeatures.Location = new System.Drawing.Point(50, 270);
            this.panelFeatures.Name = "panelFeatures";
            this.panelFeatures.Size = new System.Drawing.Size(500, 150);
            this.panelFeatures.TabIndex = 4;
            // 
            // lblFeatures
            // 
            this.lblFeatures.AutoSize = true;
            this.lblFeatures.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblFeatures.Location = new System.Drawing.Point(180, 15);
            this.lblFeatures.Name = "lblFeatures";
            this.lblFeatures.Size = new System.Drawing.Size(140, 20);
            this.lblFeatures.TabIndex = 0;
            this.lblFeatures.Text = "Gelecek Özellikler:";
            // 
            // lblFeature1
            // 
            this.lblFeature1.AutoSize = true;
            this.lblFeature1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFeature1.Location = new System.Drawing.Point(30, 45);
            this.lblFeature1.Name = "lblFeature1";
            this.lblFeature1.Size = new System.Drawing.Size(190, 17);
            this.lblFeature1.TabIndex = 1;
            this.lblFeature1.Text = "? Hasta seçimi ve kayýt oluþturma";
            // 
            // lblFeature2
            // 
            this.lblFeature2.AutoSize = true;
            this.lblFeature2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFeature2.Location = new System.Drawing.Point(30, 67);
            this.lblFeature2.Name = "lblFeature2";
            this.lblFeature2.Size = new System.Drawing.Size(180, 17);
            this.lblFeature2.TabIndex = 2;
            this.lblFeature2.Text = "? Taný ve tedavi bilgi giriþi";
            // 
            // lblFeature3
            // 
            this.lblFeature3.AutoSize = true;
            this.lblFeature3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFeature3.Location = new System.Drawing.Point(30, 89);
            this.lblFeature3.Name = "lblFeature3";
            this.lblFeature3.Size = new System.Drawing.Size(170, 17);
            this.lblFeature3.TabIndex = 3;
            this.lblFeature3.Text = "? Ýlaç reçetesi oluþturma";
            // 
            // lblFeature4
            // 
            this.lblFeature4.AutoSize = true;
            this.lblFeature4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFeature4.Location = new System.Drawing.Point(30, 111);
            this.lblFeature4.Name = "lblFeature4";
            this.lblFeature4.Size = new System.Drawing.Size(170, 17);
            this.lblFeature4.TabIndex = 4;
            this.lblFeature4.Text = "? Dosya ve görsel ekleme";
            // 
            // btnGoBack
            // 
            this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGoBack.ForeColor = System.Drawing.Color.White;
            this.btnGoBack.Location = new System.Drawing.Point(250, 440);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(100, 35);
            this.btnGoBack.TabIndex = 5;
            this.btnGoBack.Text = "Geri Dön";
            this.btnGoBack.UseVisualStyleBackColor = false;
            this.btnGoBack.Click += new System.EventHandler(this.BtnGoBack_Click);
            // 
            // DoctorNewMedicalRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelMain);
            this.Name = "DoctorNewMedicalRecordForm";
            this.Text = "Yeni Týbbi Kayýt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.panelFeatures.ResumeLayout(false);
            this.panelFeatures.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel panelFeatures;
        private System.Windows.Forms.Label lblFeature4;
        private System.Windows.Forms.Label lblFeature3;
        private System.Windows.Forms.Label lblFeature2;
        private System.Windows.Forms.Label lblFeature1;
        private System.Windows.Forms.Label lblFeatures;
        private System.Windows.Forms.Button btnGoBack;
    }
}