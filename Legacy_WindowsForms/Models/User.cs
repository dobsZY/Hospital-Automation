using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanýcý adý gereklidir")]
        [StringLength(100, ErrorMessage = "Kullanýcý adý en fazla 100 karakter olabilir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Þifre gereklidir")]
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