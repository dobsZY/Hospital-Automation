using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Models
{
    public class NursingNote : BaseEntity
    {
        [Key]
        public int NursingNoteId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public int NurseId { get; set; }

        [ForeignKey("NurseId")]
        public User Nurse { get; set; }

        [Required]
        public DateTime NoteDateTime { get; set; }

        [Required]
        public NursingNoteType NoteType { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [StringLength(500)]
        public string Assessment { get; set; }

        [StringLength(500)]
        public string InterventionPlanned { get; set; }

        [StringLength(500)]
        public string PatientResponse { get; set; }

        public bool IsUrgent { get; set; }
    }
}