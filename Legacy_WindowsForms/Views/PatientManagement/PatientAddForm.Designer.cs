namespace HospitalAutomation.Views.PatientManagement
{
    partial class PatientAddForm
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
            this.txtNationalId = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.cmbBloodType = new System.Windows.Forms.ComboBox();
            this.txtEmergencyContactName = new System.Windows.Forms.TextBox();
            this.txtEmergencyContactPhone = new System.Windows.Forms.TextBox();
            this.txtMedicalHistory = new System.Windows.Forms.RichTextBox();
            this.txtAllergies = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNationalId = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblBloodType = new System.Windows.Forms.Label();
            this.lblEmergencyContactName = new System.Windows.Forms.Label();
            this.lblEmergencyContactPhone = new System.Windows.Forms.Label();
            this.lblMedicalHistory = new System.Windows.Forms.Label();
            this.lblAllergies = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNationalId
            // 
            this.txtNationalId.Location = new System.Drawing.Point(160, 30);
            this.txtNationalId.MaxLength = 11;
            this.txtNationalId.Name = "txtNationalId";
            this.txtNationalId.Size = new System.Drawing.Size(200, 22);
            this.txtNationalId.TabIndex = 0;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(160, 70);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 22);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(160, 110);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 22);
            this.txtLastName.TabIndex = 2;
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(160, 150);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 22);
            this.dtpBirthDate.TabIndex = 3;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Location = new System.Drawing.Point(160, 190);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(200, 24);
            this.cmbGender.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(530, 30);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 22);
            this.txtPhone.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(530, 70);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 22);
            this.txtEmail.TabIndex = 6;
            // 
            // cmbBloodType
            // 
            this.cmbBloodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBloodType.Location = new System.Drawing.Point(530, 110);
            this.cmbBloodType.Name = "cmbBloodType";
            this.cmbBloodType.Size = new System.Drawing.Size(200, 24);
            this.cmbBloodType.TabIndex = 7;
            // 
            // txtEmergencyContactName
            // 
            this.txtEmergencyContactName.Location = new System.Drawing.Point(530, 150);
            this.txtEmergencyContactName.Name = "txtEmergencyContactName";
            this.txtEmergencyContactName.Size = new System.Drawing.Size(200, 22);
            this.txtEmergencyContactName.TabIndex = 8;
            // 
            // txtEmergencyContactPhone
            // 
            this.txtEmergencyContactPhone.Location = new System.Drawing.Point(530, 190);
            this.txtEmergencyContactPhone.Name = "txtEmergencyContactPhone";
            this.txtEmergencyContactPhone.Size = new System.Drawing.Size(200, 22);
            this.txtEmergencyContactPhone.TabIndex = 9;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(160, 250);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(570, 80);
            this.txtAddress.TabIndex = 10;
            this.txtAddress.Text = "";
            // 
            // txtMedicalHistory
            // 
            this.txtMedicalHistory.Location = new System.Drawing.Point(160, 360);
            this.txtMedicalHistory.Name = "txtMedicalHistory";
            this.txtMedicalHistory.Size = new System.Drawing.Size(570, 80);
            this.txtMedicalHistory.TabIndex = 11;
            this.txtMedicalHistory.Text = "";
            // 
            // txtAllergies
            // 
            this.txtAllergies.Location = new System.Drawing.Point(160, 470);
            this.txtAllergies.Name = "txtAllergies";
            this.txtAllergies.Size = new System.Drawing.Size(570, 80);
            this.txtAllergies.TabIndex = 12;
            this.txtAllergies.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(530, 580);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(640, 580);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Ýptal";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblNationalId
            // 
            this.lblNationalId.AutoSize = true;
            this.lblNationalId.Location = new System.Drawing.Point(30, 33);
            this.lblNationalId.Name = "lblNationalId";
            this.lblNationalId.Size = new System.Drawing.Size(92, 16);
            this.lblNationalId.TabIndex = 15;
            this.lblNationalId.Text = "TC Kimlik No:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(30, 73);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(28, 16);
            this.lblFirstName.TabIndex = 16;
            this.lblFirstName.Text = "Ad:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(30, 113);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(50, 16);
            this.lblLastName.TabIndex = 17;
            this.lblLastName.Text = "Soyad:";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(30, 153);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(91, 16);
            this.lblBirthDate.TabIndex = 18;
            this.lblBirthDate.Text = "Doðum Tarihi:";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(30, 193);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(57, 16);
            this.lblGender.TabIndex = 19;
            this.lblGender.Text = "Cinsiyet:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(400, 33);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(56, 16);
            this.lblPhone.TabIndex = 20;
            this.lblPhone.Text = "Telefon:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(400, 73);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 16);
            this.lblEmail.TabIndex = 21;
            this.lblEmail.Text = "Email:";
            // 
            // lblBloodType
            // 
            this.lblBloodType.AutoSize = true;
            this.lblBloodType.Location = new System.Drawing.Point(400, 113);
            this.lblBloodType.Name = "lblBloodType";
            this.lblBloodType.Size = new System.Drawing.Size(73, 16);
            this.lblBloodType.TabIndex = 22;
            this.lblBloodType.Text = "Kan Grubu:";
            // 
            // lblEmergencyContactName
            // 
            this.lblEmergencyContactName.AutoSize = true;
            this.lblEmergencyContactName.Location = new System.Drawing.Point(400, 153);
            this.lblEmergencyContactName.Name = "lblEmergencyContactName";
            this.lblEmergencyContactName.Size = new System.Drawing.Size(124, 16);
            this.lblEmergencyContactName.TabIndex = 23;
            this.lblEmergencyContactName.Text = "Acil Durum Kiþisi:";
            // 
            // lblEmergencyContactPhone
            // 
            this.lblEmergencyContactPhone.AutoSize = true;
            this.lblEmergencyContactPhone.Location = new System.Drawing.Point(400, 193);
            this.lblEmergencyContactPhone.Name = "lblEmergencyContactPhone";
            this.lblEmergencyContactPhone.Size = new System.Drawing.Size(108, 16);
            this.lblEmergencyContactPhone.TabIndex = 24;
            this.lblEmergencyContactPhone.Text = "Acil Durum Tel:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(30, 253);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(46, 16);
            this.lblAddress.TabIndex = 25;
            this.lblAddress.Text = "Adres:";
            // 
            // lblMedicalHistory
            // 
            this.lblMedicalHistory.AutoSize = true;
            this.lblMedicalHistory.Location = new System.Drawing.Point(30, 363);
            this.lblMedicalHistory.Name = "lblMedicalHistory";
            this.lblMedicalHistory.Size = new System.Drawing.Size(88, 16);
            this.lblMedicalHistory.TabIndex = 26;
            this.lblMedicalHistory.Text = "Týbbi Geçmiþ:";
            // 
            // lblAllergies
            // 
            this.lblAllergies.AutoSize = true;
            this.lblAllergies.Location = new System.Drawing.Point(30, 473);
            this.lblAllergies.Name = "lblAllergies";
            this.lblAllergies.Size = new System.Drawing.Size(57, 16);
            this.lblAllergies.TabIndex = 27;
            this.lblAllergies.Text = "Alerjiler:";
            // 
            // PatientAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.lblAllergies);
            this.Controls.Add(this.lblMedicalHistory);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblEmergencyContactPhone);
            this.Controls.Add(this.lblEmergencyContactName);
            this.Controls.Add(this.lblBloodType);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblNationalId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAllergies);
            this.Controls.Add(this.txtMedicalHistory);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtEmergencyContactPhone);
            this.Controls.Add(this.txtEmergencyContactName);
            this.Controls.Add(this.cmbBloodType);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtNationalId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hasta Ekle";
            this.Load += new System.EventHandler(this.PatientAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNationalId;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.ComboBox cmbBloodType;
        private System.Windows.Forms.TextBox txtEmergencyContactName;
        private System.Windows.Forms.TextBox txtEmergencyContactPhone;
        private System.Windows.Forms.RichTextBox txtMedicalHistory;
        private System.Windows.Forms.RichTextBox txtAllergies;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNationalId;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.Label lblEmergencyContactName;
        private System.Windows.Forms.Label lblEmergencyContactPhone;
        private System.Windows.Forms.Label lblMedicalHistory;
        private System.Windows.Forms.Label lblAllergies;
    }
}