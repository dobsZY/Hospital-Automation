using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAutomation.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}