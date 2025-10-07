using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAutomation.Models
{
    public class MedicalRecord : BaseEntity
    {
        [Required(ErrorMessage = "Hasta seçimi gereklidir")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doktor seçimi gereklidir")]
        public int DoctorId { get; set; }

        public int? AppointmentId { get; set; }

        [Required(ErrorMessage = "Taný gereklidir")]
        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string Prescription { get; set; }

        public string Symptoms { get; set; }

        public string Notes { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}