using System.ComponentModel.DataAnnotations;

namespace HospitalAutomation.Models.Enums
{
    public enum UserRole
    {
        Admin = 1,
        Doctor = 2,
        Nurse = 3,
        Receptionist = 4,
        Staff = 5,
        Pharmacist = 6,
        Technician = 7
    }

    public enum Gender
    {
        [Display(Name = "Erkek")]
        Male = 1,
        [Display(Name = "Kadın")]
        Female = 2,
        [Display(Name = "Diğer")]
        Other = 3
    }

    public enum AppointmentStatus
    {
        [Display(Name = "Planlandı")]
        Scheduled = 1,
        [Display(Name = "Tamamlandı")]
        Completed = 2,
        [Display(Name = "İptal Edildi")]
        Cancelled = 3,
        [Display(Name = "Gelmedi")]
        NoShow = 4,
        [Display(Name = "Devam Ediyor")]
        InProgress = 5,
        [Display(Name = "Ertelendi")]
        Rescheduled = 6
    }

    public enum BloodType
    {
        [Display(Name = "A Rh+")]
        APositive,
        [Display(Name = "A Rh-")]
        ANegative,
        [Display(Name = "B Rh+")]
        BPositive,
        [Display(Name = "B Rh-")]
        BNegative,
        [Display(Name = "AB Rh+")]
        ABPositive,
        [Display(Name = "AB Rh-")]
        ABNegative,
        [Display(Name = "0 Rh+")]
        OPositive,
        [Display(Name = "0 Rh-")]
        ONegative
    }

    public enum MedicationStatus
    {
        Scheduled = 1,
        Administered = 2,
        Skipped = 3,
        Refused = 4,
        Discontinued = 5
    }

    public enum NursingNoteType
    {
        GeneralCare = 1,
        Assessment = 2,
        Intervention = 3,
        PatientEducation = 4,
        Discharge = 5,
        Emergency = 6,
        MedicationRelated = 7,
        VitalSigns = 8
    }

    public enum Priority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Critical = 4
    }

    public enum ShiftType
    {
        Morning = 1,
        Evening = 2,
        Night = 3
    }
}

