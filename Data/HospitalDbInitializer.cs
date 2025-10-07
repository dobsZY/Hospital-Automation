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
                        Name = "Ýstanbul", 
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
                        Region = "Ýç Anadolu",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new City 
                    { 
                        Name = "Ýzmir", 
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
                var istanbul = context.Cities.FirstOrDefault(c => c.Name == "Ýstanbul");
                var ankara = context.Cities.FirstOrDefault(c => c.Name == "Ankara");
                var izmir = context.Cities.FirstOrDefault(c => c.Name == "Ýzmir");

                var districts = new[]
                {
                    new District { Name = "Kadýköy", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Beþiktaþ", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Þiþli", CityId = istanbul.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Çankaya", CityId = ankara.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Keçiören", CityId = ankara.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Konak", CityId = izmir.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new District { Name = "Karþýyaka", CityId = izmir.Id, CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true }
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
                        Description = "Kalp ve damar hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Nöroloji", 
                        Code = "NEUR", 
                        Description = "Sinir sistemi hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Ortopedi", 
                        Code = "ORTH", 
                        Description = "Kemik ve kas hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Dahiliye", 
                        Code = "INT", 
                        Description = "Ýç hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Göz Hastalýklarý", 
                        Code = "EYE", 
                        Description = "Göz ile ilgili hastalýklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Kulak Burun Boðaz", 
                        Code = "ENT", 
                        Description = "KBB ile ilgili hastalýklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Üroloji", 
                        Code = "URO", 
                        Description = "Ýdrar yollarý ve erkek üreme sistemi hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Jinekolog", 
                        Code = "GYN", 
                        Description = "Kadýn hastalýklarý ve doðum",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Çocuk Hastalýklarý", 
                        Code = "PED", 
                        Description = "Çocuklarda görülen hastalýklar",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Genel Cerrahi", 
                        Code = "SURG", 
                        Description = "Genel cerrahi iþlemler",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Dermatoloji", 
                        Code = "DERM", 
                        Description = "Cilt hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Psikiyatri", 
                        Code = "PSY", 
                        Description = "Ruh saðlýðý ve hastalýklarý",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Radyoloji", 
                        Code = "RAD", 
                        Description = "Görüntüleme teþhis yöntemleri",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Department 
                    { 
                        Name = "Acil Týp", 
                        Code = "EMER", 
                        Description = "Acil durum týbbi müdahaleler",
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
                    Phone = "05551234567",
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
                        LastName = "Yýlmaz",
                        Email = "mehmet.yilmaz@hospital.com",
                        Phone = "05551234568",
                        Role = UserRole.Doctor,
                        DepartmentId = cardiology?.Id,
                        Specialization = "Kardiyoloji",
                        MedicalLicenseNumber = "DOC001",
                        ExperienceYears = 15,
                        Education = "Ýstanbul Üniversitesi Týp Fakültesi, Kardiyoloji Uzmaný",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.ayse.noroloji",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Ayþe",
                        LastName = "Kaya",
                        Email = "ayse.kaya@hospital.com",
                        Phone = "05551234569",
                        Role = UserRole.Doctor,
                        DepartmentId = neurology?.Id,
                        Specialization = "Nöroloji",
                        MedicalLicenseNumber = "DOC002",
                        ExperienceYears = 12,
                        Education = "Ankara Üniversitesi Týp Fakültesi, Nöroloji Uzmaný",
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
                        Phone = "05551234570",
                        Role = UserRole.Doctor,
                        DepartmentId = orthopedics?.Id,
                        Specialization = "Ortopedi ve Travmatoloji",
                        MedicalLicenseNumber = "DOC003",
                        ExperienceYears = 18,
                        Education = "Hacettepe Üniversitesi Týp Fakültesi, Ortopedi Uzmaný",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "dr.zeynep.dahiliye",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Dr. Zeynep",
                        LastName = "Aydýn",
                        Email = "zeynep.aydin@hospital.com",
                        Phone = "05551234571",
                        Role = UserRole.Doctor,
                        DepartmentId = internalMedicine?.Id,
                        Specialization = "Ýç Hastalýklarý",
                        MedicalLicenseNumber = "DOC004",
                        ExperienceYears = 10,
                        Education = "Ege Üniversitesi Týp Fakültesi, Ýç Hastalýklarý Uzmaný",
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
                        Phone = "05551234572",
                        Role = UserRole.Doctor,
                        DepartmentId = ophthalmology?.Id,
                        Specialization = "Göz Hastalýklarý",
                        MedicalLicenseNumber = "DOC005",
                        ExperienceYears = 8,
                        Education = "Marmara Üniversitesi Týp Fakültesi, Göz Hastalýklarý Uzmaný",
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
                        Username = "hemþire.fatma",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Fatma",
                        LastName = "Demir",
                        Email = "fatma.demir.nurse@hospital.com",
                        Phone = "05551234580",
                        Role = UserRole.Nurse,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new User
                    {
                        Username = "hemþire.elif",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = "Elif",
                        LastName = "Þahin",
                        Email = "elif.sahin@hospital.com",
                        Phone = "05551234581",
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
                        Phone = "05551234590",
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
                        LastName = "Yýldýz",
                        Email = "sema.yildiz@hospital.com",
                        Phone = "05551234591",
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
                        LastName = "Yýlmaz",
                        BirthDate = new DateTime(1985, 5, 15),
                        Gender = Gender.Male,
                        Phone = "05551111111",
                        Email = "ahmet.yilmaz@email.com",
                        Address = "Ýstanbul, Beyoðlu",
                        BloodType = BloodType.APositive,
                        CityId = istanbul?.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    },
                    new Patient
                    {
                        NationalId = "98765432109",
                        FirstName = "Ayþe",
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
                        Address = "Ýzmir, Konak",
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
                        Description = "Aðrý kesici ve ateþ düþürücü",
                        Manufacturer = "Eczacýbaþý",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System"
                    },
                    new Medication
                    {
                        Name = "Ýbuprofen",
                        Dosage = "400mg",
                        Unit = "Tablet",
                        Description = "Antiinflamatuar ve aðrý kesici",
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
                        Manufacturer = "Ýlko",
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
                        Description = "Diyabet ilacý",
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