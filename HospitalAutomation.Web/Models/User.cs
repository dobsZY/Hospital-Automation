using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class User : BaseEntity
    {
        // Id BaseEntity'den geliyor

        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        [StringLength(100, ErrorMessage = "Kullanıcı adı en fazla 100 karakter olabilir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "E-mail gereklidir")]
        [StringLength(200, ErrorMessage = "E-mail en fazla 200 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Rol seçimi gereklidir")]
        public UserRole Role { get; set; }

        // Doktorlar için bölüm bilgisi
        public int? DepartmentId { get; set; }

        // Doktor özellikleri
        public string? Specialization { get; set; } // Uzmanlık alanı
        public string? MedicalLicenseNumber { get; set; } // Tıp lisans numarası
        public int? ExperienceYears { get; set; } // Deneyim yılı
        public string? Education { get; set; } // Eğitim bilgisi
        public string? ProfilePicturePath { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        // Navigation Properties
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Appointment> DoctorAppointments { get; set; }
        public virtual ICollection<MedicalRecord> DoctorMedicalRecords { get; set; }
        public virtual ICollection<Patient> CreatedPatients { get; set; }

        public User()
        {
            DoctorAppointments = new HashSet<Appointment>();
            DoctorMedicalRecords = new HashSet<MedicalRecord>();
            CreatedPatients = new HashSet<Patient>();
        }
    }
}

