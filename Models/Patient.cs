using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class Patient : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No gereklidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalýdýr")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Ad gereklidir")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi gereklidir")]
        public Gender Gender { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarasý giriniz")]
        [StringLength(20)]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
        [StringLength(200)]
        public string Email { get; set; }

        public string Address { get; set; }

        public int? CityId { get; set; }
        public int? DistrictId { get; set; }

        public BloodType? BloodType { get; set; }

        public string EmergencyContactName { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarasý giriniz")]
        public string EmergencyContactPhone { get; set; }

        public string MedicalHistory { get; set; }
        public string Allergies { get; set; }
        public string Medications { get; set; } // Kullandýðý ilaçlar
        public string Insurance { get; set; } // Sigortasý
        public string Occupation { get; set; } // Meslek

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        public int Age => DateTime.Today.Year - BirthDate.Year - (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

        // Navigation Properties
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }

        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            MedicalRecords = new HashSet<MedicalRecord>();
        }
    }
}