using Microsoft.EntityFrameworkCore;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        
        // Hemşire işlemleri için yeni DbSet'ler
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationAdministration> MedicationAdministrations { get; set; }
        public DbSet<NursingNote> NursingNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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


            // User-Department relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasOne(p => p.City)
                .WithMany(c => c.Patients)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Patient-District relationship
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.District)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(u => u.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Department)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // MedicalRecord configurations
            modelBuilder.Entity<MedicalRecord>()
                .HasKey(mr => mr.Id);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Doctor)
                .WithMany(u => u.DoctorMedicalRecords)
                .HasForeignKey(mr => mr.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Appointment)
                .WithMany()
                .HasForeignKey(mr => mr.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasOne(vs => vs.Patient)
                .WithMany()
                .HasForeignKey(vs => vs.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VitalSigns>()
                .HasOne(vs => vs.Nurse)
                .WithMany()
                .HasForeignKey(vs => vs.NurseId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasOne(ma => ma.Patient)
                .WithMany()
                .HasForeignKey(ma => ma.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicationAdministration>()
                .HasOne(ma => ma.Medication)
                .WithMany()
                .HasForeignKey(ma => ma.MedicationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicationAdministration>()
                .HasOne(ma => ma.Nurse)
                .WithMany()
                .HasForeignKey(ma => ma.NurseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicationAdministration>()
                .HasOne(ma => ma.Doctor)
                .WithMany()
                .HasForeignKey(ma => ma.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // NursingNote configurations
            modelBuilder.Entity<NursingNote>()
                .HasKey(nn => nn.NursingNoteId);

            modelBuilder.Entity<NursingNote>()
                .HasOne(nn => nn.Patient)
                .WithMany()
                .HasForeignKey(nn => nn.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NursingNote>()
                .HasOne(nn => nn.Nurse)
                .WithMany()
                .HasForeignKey(nn => nn.NurseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NursingNote>()
                .Property(nn => nn.Content)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}

