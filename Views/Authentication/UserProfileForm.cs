using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.Authentication
{
    public partial class UserProfileForm : Form
    {
        private UserService _userService;

        public UserProfileForm()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblUserType = new System.Windows.Forms.Label();
            this.lblUserTypeValue = new System.Windows.Forms.Label();
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
            this.lblTitle.Text = "Profil Bilgilerim";
            
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.lblFirstName);
            this.groupBoxInfo.Controls.Add(this.txtFirstName);
            this.groupBoxInfo.Controls.Add(this.lblLastName);
            this.groupBoxInfo.Controls.Add(this.txtLastName);
            this.groupBoxInfo.Controls.Add(this.lblEmail);
            this.groupBoxInfo.Controls.Add(this.txtEmail);
            this.groupBoxInfo.Controls.Add(this.lblPhone);
            this.groupBoxInfo.Controls.Add(this.txtPhone);
            this.groupBoxInfo.Controls.Add(this.lblUserType);
            this.groupBoxInfo.Controls.Add(this.lblUserTypeValue);
            this.groupBoxInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBoxInfo.Location = new System.Drawing.Point(50, 80);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(500, 280);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Kiþisel Bilgiler";
            
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
            this.txtFirstName.Location = new System.Drawing.Point(120, 37);
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
            this.txtLastName.Location = new System.Drawing.Point(120, 77);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 26);
            this.txtLastName.TabIndex = 3;
            
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 120);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(58, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "E-mail:";
            
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 117);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 26);
            this.txtEmail.TabIndex = 5;
            
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
            this.txtPhone.Location = new System.Drawing.Point(120, 157);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 26);
            this.txtPhone.TabIndex = 7;
            
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Location = new System.Drawing.Point(30, 200);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(64, 20);
            this.lblUserType.TabIndex = 8;
            this.lblUserType.Text = "Görev:";
            
            // 
            // lblUserTypeValue
            // 
            this.lblUserTypeValue.AutoSize = true;
            this.lblUserTypeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserTypeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblUserTypeValue.Location = new System.Drawing.Point(120, 200);
            this.lblUserTypeValue.Name = "lblUserTypeValue";
            this.lblUserTypeValue.Size = new System.Drawing.Size(58, 20);
            this.lblUserTypeValue.TabIndex = 9;
            this.lblUserTypeValue.Text = "Doktor";
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 380);
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
            this.btnCancel.Location = new System.Drawing.Point(320, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Ýptal";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            
            // 
            // UserProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profil Bilgileri";
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadUserData()
        {
            if (SessionManager.IsStaffLogin)
            {
                var user = SessionManager.CurrentUser;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
                
                // Kullanýcý türünü Türkçe olarak göster
                switch (user.Role)
                {
                    case HospitalAutomation.Models.Enums.UserRole.Doctor:
                        lblUserTypeValue.Text = "Doktor";
                        break;
                    case HospitalAutomation.Models.Enums.UserRole.Nurse:
                        lblUserTypeValue.Text = "Hemþire";
                        break;
                    case HospitalAutomation.Models.Enums.UserRole.Admin:
                        lblUserTypeValue.Text = "Yönetici";
                        break;
                    case HospitalAutomation.Models.Enums.UserRole.Receptionist:
                        lblUserTypeValue.Text = "Resepsiyonist";
                        break;
                    case HospitalAutomation.Models.Enums.UserRole.Staff:
                        lblUserTypeValue.Text = "Personel";
                        break;
                    default:
                        lblUserTypeValue.Text = "Bilinmiyor";
                        break;
                }
            }
            
            // Servisi baþlat
            try
            {
                var context = new HospitalDbContext();
                var unitOfWork = new UnitOfWork(context);
                _userService = new UserService(unitOfWork);
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

                if (SessionManager.IsStaffLogin)
                {
                    var user = SessionManager.CurrentUser;
                    user.FirstName = txtFirstName.Text.Trim();
                    user.LastName = txtLastName.Text.Trim();
                    user.Email = txtEmail.Text.Trim();
                    user.Phone = txtPhone.Text.Trim();

                    // Veritabanýný güncelle
                    _userService.UpdateUser(user);

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
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Label lblUserTypeValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxInfo;
    }
}