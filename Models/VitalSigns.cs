using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAutomation.Models
{
    public class VitalSigns : BaseEntity
    {
        [Key]
        public int VitalSignsId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public int NurseId { get; set; }

        [ForeignKey("NurseId")]
        public User Nurse { get; set; }

        [Required]
        public DateTime MeasurementDateTime { get; set; }

        // Vital bulgular
        public double? BloodPressureSystolic { get; set; }
        public double? BloodPressureDiastolic { get; set; }
        public double? HeartRate { get; set; }
        public double? Temperature { get; set; }
        public double? RespiratoryRate { get; set; }
        public double? OxygenSaturation { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public string BloodPressure => 
            BloodPressureSystolic.HasValue && BloodPressureDiastolic.HasValue 
                ? $"{BloodPressureSystolic}/{BloodPressureDiastolic}" 
                : "";
    }
}