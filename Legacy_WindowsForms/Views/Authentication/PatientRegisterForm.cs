using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.Authentication
{
    public partial class PatientRegisterForm : Form
    {
        private IPatientService _patientService;

        public PatientRegisterForm()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
        }

        private void InitializeServices()
        {
            try
            {
                var context = new HospitalDbContext();
                var unitOfWork = new UnitOfWork(context);
                _patientService = new PatientService(unitOfWork);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Servis baþlatma hatasý: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupUI()
        {
            try
            {
                // Fill Gender ComboBox
                cmbGender.Items.Clear();
                cmbGender.Items.Add(new ComboBoxItem("Erkek", Gender.Male));
                cmbGender.Items.Add(new ComboBoxItem("Kadýn", Gender.Female));
                cmbGender.DisplayMember = "Text";
                cmbGender.ValueMember = "Value";

                // Fill Blood Type ComboBox
                cmbBloodType.Items.Clear();
                cmbBloodType.Items.Add(new ComboBoxItem("Seçiniz", null));
                foreach (BloodType bloodType in Enum.GetValues(typeof(BloodType)))
                {
                    string displayText = "";
                    switch (bloodType)
                    {
                        case BloodType.APositive:
                            displayText = "A RH+";
                            break;
                        case BloodType.ANegative:
                            displayText = "A RH-";
                            break;
                        case BloodType.BPositive:
                            displayText = "B RH+";
                            break;
                        case BloodType.BNegative:
                            displayText = "B RH-";
                            break;
                        case BloodType.ABPositive:
                            displayText = "AB RH+";
                            break;
                        case BloodType.ABNegative:
                            displayText = "AB RH-";
                            break;
                        case BloodType.OPositive:
                            displayText = "0 RH+";
                            break;
                        case BloodType.ONegative:
                            displayText = "0 RH-";
                            break;
                        default:
                            displayText = bloodType.ToString();
                            break;
                    }
                    cmbBloodType.Items.Add(new ComboBoxItem(displayText, bloodType));
                }
                cmbBloodType.DisplayMember = "Text";
                cmbBloodType.ValueMember = "Value";
                cmbBloodType.SelectedIndex = 0; // Select "Seçiniz" by default

                // Set birth date limits
                dtpBirthDate.MaxDate = DateTime.Today;
                dtpBirthDate.MinDate = DateTime.Today.AddYears(-120);
                dtpBirthDate.Value = DateTime.Today.AddYears(-25); // Default to 25 years old
            }
            catch (Exception ex)
            {
                MessageBox.Show($"UI hazýrlama hatasý: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Disable the register button to prevent multiple clicks
                btnRegister.Enabled = false;
                btnRegister.Text = "Kaydediliyor...";
                Application.DoEvents();

                var patient = new Patient
                {
                    NationalId = txtNationalId.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    BirthDate = dtpBirthDate.Value.Date,
                    Gender = (Gender)((ComboBoxItem)cmbGender.SelectedItem).Value,
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    BloodType = cmbBloodType.SelectedIndex <= 0 ? null : (BloodType?)((ComboBoxItem)cmbBloodType.SelectedItem).Value,
                    EmergencyContactName = string.IsNullOrWhiteSpace(txtEmergencyContactName.Text) ? null : txtEmergencyContactName.Text.Trim(),
                    EmergencyContactPhone = string.IsNullOrWhiteSpace(txtEmergencyContactPhone.Text) ? null : txtEmergencyContactPhone.Text.Trim(),
                    CreatedBy = "PatientSelfRegistration",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                if (_patientService.CreatePatient(patient))
                {
                    var loginPassword = patient.BirthDate.ToString("ddMM");
                    var message = $"Hasta kaydýnýz baþarýyla oluþturuldu!\n\n" +
                                 $"Giriþ Bilgileriniz:\n" +
                                 $"TC Kimlik No: {patient.NationalId}\n" +
                                 $"Þifreniz: {loginPassword}\n\n" +
                                 $"(Þifreniz doðum tarihinizin gün+ay formatýndadýr)\n\n" +
                                 $"Bu bilgileri kaydetmeyi unutmayýn!";
                    
                    MessageBox.Show(message, "Kayýt Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hasta kaydý oluþturulamadý! Lütfen bilgilerinizi kontrol edip tekrar deneyin.", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Kayýt sýrasýnda hata oluþtu:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDetay: {ex.InnerException.Message}";
                }
                
                MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the register button
                btnRegister.Enabled = true;
                btnRegister.Text = "Kayýt Ol";
            }
        }

        private bool ValidateInput()
        {
            // TC Kimlik No validation
            if (string.IsNullOrWhiteSpace(txtNationalId.Text))
            {
                MessageBox.Show("TC Kimlik No boþ býrakýlamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            if (txtNationalId.Text.Trim().Length != 11)
            {
                MessageBox.Show("TC Kimlik No 11 haneli olmalýdýr!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            if (!txtNationalId.Text.All(char.IsDigit))
            {
                MessageBox.Show("TC Kimlik No sadece rakamlardan oluþmalýdýr!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidTCKimlikNo(txtNationalId.Text.Trim()))
            {
                MessageBox.Show("Geçersiz TC Kimlik No! Lütfen doðru TC Kimlik No giriniz.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            // Check if national ID already exists
            try
            {
                if (_patientService.IsNationalIdExists(txtNationalId.Text.Trim()))
                {
                    MessageBox.Show("Bu TC Kimlik No ile kayýtlý hasta bulunmaktadýr!\n\nGiriþ yapmak için ana ekrandaki giriþ formunu kullanabilirsiniz.", 
                        "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNationalId.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TC Kimlik No kontrolü sýrasýnda hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Name validation
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Ad alaný boþ býrakýlamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (txtFirstName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Ad en az 2 karakter olmalýdýr!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            // Last name validation
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Soyad alaný boþ býrakýlamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (txtLastName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Soyad en az 2 karakter olmalýdýr!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            // Gender validation
            if (cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Cinsiyet seçimi yapýnýz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            // Birth date validation
            if (dtpBirthDate.Value.Date >= DateTime.Today)
            {
                MessageBox.Show("Doðum tarihi bugünden önce olmalýdýr!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }

            var age = DateTime.Today.Year - dtpBirthDate.Value.Year;
            if (DateTime.Today.DayOfYear < dtpBirthDate.Value.DayOfYear)
                age--;

            if (age < 0 || age > 120)
            {
                MessageBox.Show("Geçerli bir doðum tarihi giriniz! (0 ile 120 yaþ arasý)", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }

            // Phone validation (optional but if provided must be valid)
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                var phone = txtPhone.Text.Trim();
                if (phone.Length < 10 || !phone.All(c => char.IsDigit(c) || c == ' ' || c == '-' || c == '(' || c == ')'))
                {
                    MessageBox.Show("Geçerli bir telefon numarasý giriniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            // Email validation (optional but if provided must be valid)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                var email = txtEmail.Text.Trim();
                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Geçerli bir e-posta adresi giriniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtNationalId_TextChanged(object sender, EventArgs e)
        {
            // Only allow digits and limit to 11 characters
            var textBox = (TextBox)sender;
            var text = textBox.Text;
            var cursorPosition = textBox.SelectionStart;
            
            // Remove non-digit characters
            var cleanText = new string(text.Where(char.IsDigit).ToArray());
            
            // Limit to 11 characters
            if (cleanText.Length > 11)
                cleanText = cleanText.Substring(0, 11);
            
            if (text != cleanText)
            {
                textBox.Text = cleanText;
                textBox.SelectionStart = Math.Min(cursorPosition, cleanText.Length);
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // Format phone number as user types
            var textBox = (TextBox)sender;
            var text = textBox.Text;
            var cursorPosition = textBox.SelectionStart;
            
            // Remove all non-digit characters
            var cleanText = new string(text.Where(char.IsDigit).ToArray());
            
            // Limit to 11 characters (for Turkish mobile numbers)
            if (cleanText.Length > 11)
                cleanText = cleanText.Substring(0, 11);
            
            if (text != cleanText)
            {
                textBox.Text = cleanText;
                textBox.SelectionStart = Math.Min(cursorPosition, cleanText.Length);
            }
        }
    }
}