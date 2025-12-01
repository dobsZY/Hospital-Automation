using System;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Data
{
    public class HospitalDbInitializer : DropCreateDatabaseAlways<HospitalDbContext>
    {
        protected override void Seed(HospitalDbContext context)
        {
            try
            {
                // Add some sample cities first
                var cities = new[]
                {
                    new City 
                    { 
                        Name = "İstanbul", 
                        Code = "34", 
                        Region = "Marmara",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new City 
                    { 
                        Name = "Ankara", 
                        Code = "06", 
                        Region = "İç Anadolu",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new City 
                    { 
                        Name = "İzmir", 
                        Code = "35", 
                        Region = "Ege",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var city in cities)
                {
                    context.Cities.Add(city);
                }
                context.SaveChanges();

                // Add sample districts
                var istanbul = context.Cities.FirstOrDefault(c => c.Name == "İstanbul");
                var ankara = context.Cities.FirstOrDefault(c => c.Name == "Ankara");
                var izmir = context.Cities.FirstOrDefault(c => c.Name == "İzmir");

                var districts = new[]
                {
                    new District { Name = "Kadıköy", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Beşiktaş", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Şişli", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Çankaya", CityId = ankara.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Keçiören", CityId = ankara.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Konak", CityId = izmir.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Karşıyaka", CityId = izmir.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true }
                };

                foreach (var district in districts)
                {
                    context.Districts.Add(district);
                }
                context.SaveChanges();

                // Seed Departments - Comprehensive department list
                var departments = new[]
                {
                    new Department 
                    { 
                        Name = "Kardiyoloji", 
                        Code = "CARD", 
                        Description = "Kalp ve damar hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Nöroloji", 
                        Code = "NEUR", 
                        Description = "Sinir sistemi hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Ortopedi", 
                        Code = "ORTH", 
                        Description = "Kemik ve kas hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Dahiliye", 
                        Code = "INT", 
                        Description = "İç hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Göz Hastalıkları", 
                        Code = "EYE", 
                        Description = "Göz ile ilgili hastalıklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Kulak Burun Boğaz", 
                        Code = "ENT", 
                        Description = "KBB ile ilgili hastalıklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Üroloji", 
                        Code = "URO", 
                        Description = "İdrar yolları ve erkek üreme sistemi hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Jinekolog", 
                        Code = "GYN", 
                        Description = "Kadın hastalıkları ve doğum",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Çocuk Hastalıkları", 
                        Code = "PED", 
                        Description = "Çocuklarda görülen hastalıklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Genel Cerrahi", 
                        Code = "SURG", 
                        Description = "Genel cerrahi işlemler",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Dermatoloji", 
                        Code = "DERM", 
                        Description = "Cilt hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Psikiyatri", 
                        Code = "PSY", 
                        Description = "Ruh sağlığı ve hastalıkları",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Radyoloji", 
                        Code = "RAD", 
                        Description = "Görüntüleme teşhis yöntemleri",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Acil Tıp", 
                        Code = "EMER", 
                        Description = "Acil durum tıbbi müdahaleler",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Anesteziyoloji", 
                        Code = "ANES", 
                        Description = "Anestezi ve reanimasyon",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var dept in departments)
                {
                    context.Departments.Add(dept);
                }

                context.SaveChanges(); // Save departments first so we can get their IDs

                // Get department references
                var cardiology = context.Departments.FirstOrDefault(d => d.Code == "CARD");
                var neurology = context.Departments.FirstOrDefault(d => d.Code == "NEUR");
                var orthopedics = context.Departments.FirstOrDefault(d => d.Code == "ORTH");
                var internalMedicine = context.Departments.FirstOrDefault(d => d.Code == "INT");
                var ophthalmology = context.Departments.FirstOrDefault(d => d.Code == "EYE");
                var dermatology = context.Departments.FirstOrDefault(d => d.Code == "DERM");
                var psychiatry = context.Departments.FirstOrDefault(d => d.Code == "PSY");
                var ent = context.Departments.FirstOrDefault(d => d.Code == "ENT");
                var urology = context.Departments.FirstOrDefault(d => d.Code == "URO");
                var gynecology = context.Departments.FirstOrDefault(d => d.Code == "GYN");

                // Seed Admin User
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = PasswordHelper.HashPassword("admin123"),
                    FirstName = "Sistem",
                    LastName = "Yöneticisi",
                    Email = "admin@hospital.com",
                    Role = UserRole.Admin,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true
                };
                context.Users.Add(adminUser);

                // Seed Sample Doctors - Comprehensive list
                var doctors = new[]
                {
                    new User
                    {
                        Username = "dr.mehmet.kardiyoloji",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Mehmet",
                        LastName = "Yılmaz",
                        Email = "mehmet.yilmaz@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = cardiology?.Id,
                        Specialization = "Kardiyoloji",
                        MedicalLicenseNumber = "DOC001",
                        ExperienceYears = 15,
                        Education = "İstanbul Üniversitesi Tıp Fakültesi, Kardiyoloji Uzmanı",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.ayse.noroloji",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Ayşe",
                        LastName = "Kaya",
                        Email = "ayse.kaya@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = neurology?.Id,
                        Specialization = "Nöroloji",
                        MedicalLicenseNumber = "DOC002",
                        ExperienceYears = 12,
                        Education = "Ankara Üniversitesi Tıp Fakültesi, Nöroloji Uzmanı",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.ali.ortopedi",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Ali",
                        LastName = "Özkan",
                        Email = "ali.ozkan@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = orthopedics?.Id,
                        Specialization = "Ortopedi ve Travmatoloji",
                        MedicalLicenseNumber = "DOC003",
                        ExperienceYears = 18,
                        Education = "Hacettepe Üniversitesi Tıp Fakültesi, Ortopedi Uzmanı",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.zeynep.dahiliye",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Zeynep",
                        LastName = "Aydın",
                        Email = "zeynep.aydin@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = internalMedicine?.Id,
                        Specialization = "İç Hastalıkları",
                        MedicalLicenseNumber = "DOC004",
                        ExperienceYears = 10,
                        Education = "Ege Üniversitesi Tıp Fakültesi, İç Hastalıkları Uzmanı",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.can.goz",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Can",
                        LastName = "Bulut",
                        Email = "can.bulut@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = ophthalmology?.Id,
                        Specialization = "Göz Hastalıkları",
                        MedicalLicenseNumber = "DOC005",
                        ExperienceYears = 8,
                        Education = "Marmara Üniversitesi Tıp Fakültesi, Göz Hastalıkları Uzmanı",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var doctor in doctors)
                {
                    context.Users.Add(doctor);
                }

                // Seed Sample Nurses
                var nurses = new[]
                {
                    new User
                    {
                        Username = "hemsire.fatma",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Fatma",
                        LastName = "Demir",
                        Email = "fatma.demir.nurse@hospital.com",
                        Role = UserRole.Nurse,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "hemsire.elif",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Elif",
                        LastName = "Şahin",
                        Email = "elif.sahin@hospital.com",
                        Role = UserRole.Nurse,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var nurse in nurses)
                {
                    context.Users.Add(nurse);
                }

                // Seed Receptionists
                var receptionists = new[]
                {
                    new User
                    {
                        Username = "resepsiyon.ali",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Ali",
                        LastName = "Çelik",
                        Email = "ali.celik@hospital.com",
                        Role = UserRole.Receptionist,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "resepsiyon.sema",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Sema",
                        LastName = "Yıldız",
                        Email = "sema.yildiz@hospital.com",
                        Role = UserRole.Receptionist,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var receptionist in receptionists)
                {
                    context.Users.Add(receptionist);
                }

                // Seed sample patients
                var samplePatients = new[]
                {
                    new Patient
                    {
                        NationalId = "12345678901",
                        FirstName = "Ahmet",
                        LastName = "Yılmaz",
                        BirthDate = new DateTime(1985, 5, 15),
                        Gender = Gender.Male,
                        Phone = "05551111111",
                        Email = "ahmet.yilmaz@email.com",
                        Address = "İstanbul, Beyoğlu",
                        BloodType = BloodType.APositive,
                        CityId = istanbul?.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Patient
                    {
                        NationalId = "98765432109",
                        FirstName = "Ayşe",
                        LastName = "Kaya",
                        BirthDate = new DateTime(1992, 8, 22),
                        Gender = Gender.Female,
                        Phone = "05552222222",
                        Email = "ayse.kaya@email.com",
                        Address = "Ankara, Çankaya",
                        BloodType = BloodType.BPositive,
                        CityId = ankara?.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Patient
                    {
                        NationalId = "11223344556",
                        FirstName = "Mehmet",
                        LastName = "Demir",
                        BirthDate = new DateTime(1978, 12, 3),
                        Gender = Gender.Male,
                        Phone = "05553333333",
                        Email = "mehmet.demir@email.com",
                        Address = "İzmir, Konak",
                        BloodType = BloodType.ONegative,
                        CityId = izmir?.Id,
                        EmergencyContactName = "Fatma Demir",
                        EmergencyContactPhone = "05554444444",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }
                };

                foreach (var patient in samplePatients)
                {
                    context.Patients.Add(patient);
                }

                context.SaveChanges();

                // Seed sample medications
                var medications = new[]
                {
                    new Medication
                    {
                        Name = "Paracetamol",
                        Dosage = "500mg",
                        Unit = "Tablet",
                        Description = "Ağrı kesici ve ateş düşürücü",
                        Manufacturer = "Eczacıbaşı",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    },
                    new Medication
                    {
                        Name = "İbuprofen",
                        Dosage = "400mg",
                        Unit = "Tablet",
                        Description = "Antiinflamatuar ve ağrı kesici",
                        Manufacturer = "Pfizer",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    },
                    new Medication
                    {
                        Name = "Amoksisilin",
                        Dosage = "1000mg",
                        Unit = "Tablet",
                        Description = "Antibiyotik",
                        Manufacturer = "İlko",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    },
                    new Medication
                    {
                        Name = "Omeprazol",
                        Dosage = "20mg",
                        Unit = "Kapsül",
                        Description = "Proton pompa inhibitörü",
                        Manufacturer = "Deva",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    },
                    new Medication
                    {
                        Name = "Metformin",
                        Dosage = "850mg",
                        Unit = "Tablet",
                        Description = "Diyabet ilacı",
                        Manufacturer = "Novartis",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    }
                };

                foreach (var medication in medications)
                {
                    context.Medications.Add(medication);
                }

                context.SaveChanges();

                base.Seed(context);

                System.Diagnostics.Debug.WriteLine("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error seeding database: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
