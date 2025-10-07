using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() : base("name=HospitalConnectionString")
        {
            // Use the custom initializer that recreates database when needed
            Database.SetInitializer(new HospitalDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        
        // Hemþire iþlemleri için yeni DbSet'ler
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationAdministration> MedicationAdministrations { get; set; }
        public DbSet<NursingNote> NursingNotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove pluralizing table name convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // User configurations
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .HasMaxLength(20);

            // User-Department relationship
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(u => u.DepartmentId)
                .WillCascadeOnDelete(false);

            // Patient configurations
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Patient>()
                .Property(p => p.NationalId)
                .IsRequired()
                .HasMaxLength(11);

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.NationalId)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Patient>()
                .Property(p => p.EmergencyContactName)
                .HasMaxLength(100);

            modelBuilder.Entity<Patient>()
                .Property(p => p.EmergencyContactPhone)
                .HasMaxLength(20);

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalHistory)
                .HasMaxLength(2000);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Allergies)
                .HasMaxLength(1000);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Medications)
                .HasMaxLength(1000);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Insurance)
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Occupation)
                .HasMaxLength(100);

            // Patient-City relationship
            modelBuilder.Entity<Patient>()
                .HasOptional(p => p.City)
                .WithMany(c => c.Patients)
                .HasForeignKey(p => p.CityId)
                .WillCascadeOnDelete(false);

            // Patient-District relationship
            modelBuilder.Entity<Patient>()
                .HasOptional(p => p.District)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DistrictId)
                .WillCascadeOnDelete(false);

            // City configurations
            modelBuilder.Entity<City>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<City>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<City>()
                .Property(c => c.Code)
                .HasMaxLength(10);

            modelBuilder.Entity<City>()
                .Property(c => c.Region)
                .HasMaxLength(50);

            // District configurations
            modelBuilder.Entity<District>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<District>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // District-City relationship
            modelBuilder.Entity<District>()
                .HasRequired(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId)
                .WillCascadeOnDelete(false);

            // Department configurations
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Department>()
                .Property(d => d.Code)
                .HasMaxLength(10);

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Code)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .Property(d => d.Description)
                .HasMaxLength(500);

            // Appointment configurations
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Doctor)
                .WithMany(u => u.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Appointment>()
                .HasOptional(a => a.Department)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DepartmentId)
                .WillCascadeOnDelete(false);

            // MedicalRecord configurations
            modelBuilder.Entity<MedicalRecord>()
                .HasKey(mr => mr.Id);

            modelBuilder.Entity<MedicalRecord>()
                .HasRequired(mr => mr.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(mr => mr.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalRecord>()
                .HasRequired(mr => mr.Doctor)
                .WithMany(u => u.DoctorMedicalRecords)
                .HasForeignKey(mr => mr.DoctorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalRecord>()
                .HasOptional(mr => mr.Appointment)
                .WithMany()
                .HasForeignKey(mr => mr.AppointmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalRecord>()
                .Property(mr => mr.Diagnosis)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<MedicalRecord>()
                .Property(mr => mr.Treatment)
                .HasMaxLength(2000);

            modelBuilder.Entity<MedicalRecord>()
                .Property(mr => mr.Prescription)
                .HasMaxLength(2000);

            // VitalSigns configurations
            modelBuilder.Entity<VitalSigns>()
                .HasKey(vs => vs.VitalSignsId);

            modelBuilder.Entity<VitalSigns>()
                .HasRequired(vs => vs.Patient)
                .WithMany()
                .HasForeignKey(vs => vs.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalSigns>()
                .HasRequired(vs => vs.Nurse)
                .WithMany()
                .HasForeignKey(vs => vs.NurseId)
                .WillCascadeOnDelete(false);

            // Medication configurations
            modelBuilder.Entity<Medication>()
                .HasKey(m => m.MedicationId);

            modelBuilder.Entity<Medication>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            // MedicationAdministration configurations
            modelBuilder.Entity<MedicationAdministration>()
                .HasKey(ma => ma.AdministrationId);

            modelBuilder.Entity<MedicationAdministration>()
                .HasRequired(ma => ma.Patient)
                .WithMany()
                .HasForeignKey(ma => ma.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicationAdministration>()
                .HasRequired(ma => ma.Medication)
                .WithMany()
                .HasForeignKey(ma => ma.MedicationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicationAdministration>()
                .HasRequired(ma => ma.Nurse)
                .WithMany()
                .HasForeignKey(ma => ma.NurseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicationAdministration>()
                .HasOptional(ma => ma.Doctor)
                .WithMany()
                .HasForeignKey(ma => ma.DoctorId)
                .WillCascadeOnDelete(false);

            // NursingNote configurations
            modelBuilder.Entity<NursingNote>()
                .HasKey(nn => nn.NursingNoteId);

            modelBuilder.Entity<NursingNote>()
                .HasRequired(nn => nn.Patient)
                .WithMany()
                .HasForeignKey(nn => nn.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NursingNote>()
                .HasRequired(nn => nn.Nurse)
                .WithMany()
                .HasForeignKey(nn => nn.NurseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NursingNote>()
                .Property(nn => nn.Content)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}