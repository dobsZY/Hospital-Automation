using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class Medication : BaseEntity
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Dosage { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Manufacturer { get; set; }

        public new bool IsActive { get; set; } = true;
    }

    public class MedicationAdministration : BaseEntity
    {
        [Key]
        public int AdministrationId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }

        [Required]
        public int NurseId { get; set; }

        [ForeignKey("NurseId")]
        public User Nurse { get; set; }

        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public User Doctor { get; set; }

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        public DateTime? AdministeredDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosage { get; set; }

        [Required]
        public MedicationStatus Status { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [StringLength(500)]
        public string SideEffects { get; set; }
    }
}