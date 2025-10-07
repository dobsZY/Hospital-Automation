using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Kullanýcý adý gereklidir")]
        [StringLength(50, ErrorMessage = "Kullanýcý adý en fazla 50 karakter olabilir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Þifre gereklidir")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Ad gereklidir")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarasý giriniz")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Rol seçimi gereklidir")]
        public UserRole Role { get; set; }

        // Doktorlar için bölüm bilgisi
        public int? DepartmentId { get; set; }

        // Doktor özellikleri
        public string Specialization { get; set; } // Uzmanlýk alaný
        public string MedicalLicenseNumber { get; set; } // Týp lisans numarasý
        public int? ExperienceYears { get; set; } // Deneyim yýlý
        public string Education { get; set; } // Eðitim bilgisi

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