using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class Appointment : BaseEntity
    {
        [Required(ErrorMessage = "Hasta seçimi gereklidir")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doktor seçimi gereklidir")]
        public int DoctorId { get; set; }

        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Randevu tarihi gereklidir")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Randevu saati gereklidir")]
        public TimeSpan AppointmentTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public string Notes { get; set; }

        public string Symptoms { get; set; }

        public DateTime? CompletedDate { get; set; }

        // Navigation Properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}