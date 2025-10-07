using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.PatientPortal
{
    public partial class PatientProfileForm : Form
    {
        private PatientService _patientService;

        public PatientProfileForm()
        {
            InitializeComponent();
            LoadPatientData();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblNationalId = new System.Windows.Forms.Label();
            this.txtNationalId = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblBirthDateValue = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(200, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Hasta Profilim";
            
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.lblFirstName);
            this.groupBoxInfo.Controls.Add(this.txtFirstName);
            this.groupBoxInfo.Controls.Add(this.lblLastName);
            this.groupBoxInfo.Controls.Add(this.txtLastName);
            this.groupBoxInfo.Controls.Add(this.lblNationalId);
            this.groupBoxInfo.Controls.Add(this.txtNationalId);
            this.groupBoxInfo.Controls.Add(this.lblPhone);
            this.groupBoxInfo.Controls.Add(this.txtPhone);
            this.groupBoxInfo.Controls.Add(this.lblEmail);
            this.groupBoxInfo.Controls.Add(this.txtEmail);
            this.groupBoxInfo.Controls.Add(this.lblAddress);
            this.groupBoxInfo.Controls.Add(this.txtAddress);
            this.groupBoxInfo.Controls.Add(this.lblBirthDate);
            this.groupBoxInfo.Controls.Add(this.lblBirthDateValue);
            this.groupBoxInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBoxInfo.Location = new System.Drawing.Point(50, 80);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(500, 360);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Hasta Bilgileri";
            
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(30, 40);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(35, 20);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "Ad:";
            
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(140, 37);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 26);
            this.txtFirstName.TabIndex = 1;
            
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(30, 80);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(58, 20);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Soyad:";
            
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(140, 77);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 26);
            this.txtLastName.TabIndex = 3;
            
            // 
            // lblNationalId
            // 
            this.lblNationalId.AutoSize = true;
            this.lblNationalId.Location = new System.Drawing.Point(30, 120);
            this.lblNationalId.Name = "lblNationalId";
            this.lblNationalId.Size = new System.Drawing.Size(104, 20);
            this.lblNationalId.TabIndex = 4;
            this.lblNationalId.Text = "TC Kimlik No:";
            
            // 
            // txtNationalId
            // 
            this.txtNationalId.Location = new System.Drawing.Point(140, 117);
            this.txtNationalId.Name = "txtNationalId";
            this.txtNationalId.ReadOnly = true;
            this.txtNationalId.Size = new System.Drawing.Size(200, 26);
            this.txtNationalId.TabIndex = 5;
            this.txtNationalId.BackColor = System.Drawing.Color.LightGray;
            
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(30, 160);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(69, 20);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "Telefon:";
            
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(140, 157);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 26);
            this.txtPhone.TabIndex = 7;
            
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 200);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(58, 20);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "E-mail:";
            
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(140, 197);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 26);
            this.txtEmail.TabIndex = 9;
            
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(30, 240);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(54, 20);
            this.lblAddress.TabIndex = 10;
            this.lblAddress.Text = "Adres:";
            
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(140, 237);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 60);
            this.txtAddress.TabIndex = 11;
            
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(30, 310);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(103, 20);
            this.lblBirthDate.TabIndex = 12;
            this.lblBirthDate.Text = "Doðum Tarihi:";
            
            // 
            // lblBirthDateValue
            // 
            this.lblBirthDateValue.AutoSize = true;
            this.lblBirthDateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBirthDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblBirthDateValue.Location = new System.Drawing.Point(140, 310);
            this.lblBirthDateValue.Name = "lblBirthDateValue";
            this.lblBirthDateValue.Size = new System.Drawing.Size(100, 20);
            this.lblBirthDateValue.TabIndex = 13;
            this.lblBirthDateValue.Text = "01.01.1990";
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(320, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Ýptal";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            
            // 
            // PatientProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hasta Profili";
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadPatientData()
        {
            if (SessionManager.IsPatientLogin)
            {
                var patient = SessionManager.CurrentPatient;
                txtFirstName.Text = patient.FirstName;
                txtLastName.Text = patient.LastName;
                txtNationalId.Text = patient.NationalId;
                txtPhone.Text = patient.Phone;
                txtEmail.Text = patient.Email;
                txtAddress.Text = patient.Address;
                lblBirthDateValue.Text = patient.BirthDate.ToString("dd.MM.yyyy");
            }
            
            // Servisi baþlat
            try
            {
                var context = new HospitalDbContext();
                var unitOfWork = new UnitOfWork(context);
                _patientService = new PatientService(unitOfWork);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servis baþlatýlýrken hata oluþtu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Ad ve Soyad alanlarý boþ býrakýlamaz!", "Uyarý", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SessionManager.IsPatientLogin)
                {
                    var patient = SessionManager.CurrentPatient;
                    patient.FirstName = txtFirstName.Text.Trim();
                    patient.LastName = txtLastName.Text.Trim();
                    patient.Phone = txtPhone.Text.Trim();
                    patient.Email = txtEmail.Text.Trim();
                    patient.Address = txtAddress.Text.Trim();

                    // Veritabanýný güncelle
                    _patientService.UpdatePatient(patient);

                    MessageBox.Show("Profil bilgileriniz baþarýyla güncellendi!", "Baþarýlý", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Profil güncellenirken hata oluþtu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblNationalId;
        private System.Windows.Forms.TextBox txtNationalId;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblBirthDateValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxInfo;
    }
}