using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class Appointment : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hasta seçimi gereklidir")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [Required(ErrorMessage = "Doktor seçimi gereklidir")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }

        [Required(ErrorMessage = "Randevu tarihi gereklidir")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Randevu saati gereklidir")]
        public TimeSpan AppointmentTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [StringLength(500)]
        public string Notes { get; set; }

        public string Symptoms { get; set; }

        public DateTime? CompletedDate { get; set; }

        // Navigation Properties
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}