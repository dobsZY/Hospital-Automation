using System;
using System.Linq;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HospitalDbContext context)
        {
            context.Database.EnsureCreated();

            // Seeding Cities and Districts using TurkeyDataSeed
            if (!context.Cities.Any())
            {
                var citiesAndDistricts = TurkeyDataSeed.GetCitiesAndDistricts();
                int plateCode = 1;

                foreach (var cityEntry in citiesAndDistricts)
                {
                    var city = new City
                    {
                        Name = cityEntry.Key,
                        Code = plateCode.ToString("D2"), // Auto-generate plate code (approximate)
                        Region = "Türkiye",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    };
                    
                    context.Cities.Add(city);
                    context.SaveChanges(); // Save to generate Id

                    var districts = cityEntry.Value.Select(dName => new District
                    {
                        Name = dName,
                        CityId = city.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    }).ToList();

                    context.Districts.AddRange(districts);
                    plateCode++;
                }
                context.SaveChanges();
            }

            // Seed Departments
            if (!context.Departments.Any())
            {
                var departments = new[]
                {
                    new Department { Name = "Kardiyoloji", Code = "CARD", Description = "Kalp ve damar hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Nöroloji", Code = "NEUR", Description = "Sinir sistemi hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Ortopedi", Code = "ORTH", Description = "Kemik ve kas hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Dahiliye", Code = "INT", Description = "İç hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Göz Hastalıkları", Code = "EYE", Description = "Göz ile ilgili hastalıklar", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Kulak Burun Boğaz", Code = "ENT", Description = "KBB ile ilgili hastalıklar", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Üroloji", Code = "URO", Description = "İdrar yolları ve erkek üreme sistemi hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Kadın Hastalıkları ve Doğum", Code = "GYN", Description = "Kadın hastalıkları ve doğum", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Çocuk Hastalıkları", Code = "PED", Description = "Çocuklarda görülen hastalıklar", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Genel Cerrahi", Code = "SURG", Description = "Genel cerrahi işlemler", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Dermatoloji", Code = "DERM", Description = "Cilt hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Psikiyatri", Code = "PSY", Description = "Ruh sağlığı ve hastalıkları", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Radyoloji", Code = "RAD", Description = "Görüntüleme teşhis yöntemleri", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Acil Tıp", Code = "EMER", Description = "Acil durum tıbbi müdahaleler", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true },
                    new Department { Name = "Anesteziyoloji", Code = "ANES", Description = "Anestezi ve reanimasyon", CreatedDate = DateTime.Now, CreatedBy = "System", IsActive = true }
                };

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }

            if (context.Users.Any())
            {
                return;
            }

            // Get department references for seeding users
            var cardiology = context.Departments.FirstOrDefault(d => d.Code == "CARD");
            var neurology = context.Departments.FirstOrDefault(d => d.Code == "NEUR");
            var orthopedics = context.Departments.FirstOrDefault(d => d.Code == "ORTH");
            var internalMedicine = context.Departments.FirstOrDefault(d => d.Code == "INT");
            var ophthalmology = context.Departments.FirstOrDefault(d => d.Code == "EYE");
            var ent = context.Departments.FirstOrDefault(d => d.Code == "ENT");
            var urology = context.Departments.FirstOrDefault(d => d.Code == "URO");
            var gynecology = context.Departments.FirstOrDefault(d => d.Code == "GYN");
            var pediatrics = context.Departments.FirstOrDefault(d => d.Code == "PED");
            var surgery = context.Departments.FirstOrDefault(d => d.Code == "SURG");
            var dermatology = context.Departments.FirstOrDefault(d => d.Code == "DERM");
            var psychiatry = context.Departments.FirstOrDefault(d => d.Code == "PSY");
            var radiology = context.Departments.FirstOrDefault(d => d.Code == "RAD");
            var emergency = context.Departments.FirstOrDefault(d => d.Code == "EMER");
            var anesthesiology = context.Departments.FirstOrDefault(d => d.Code == "ANES");
            
            // Get city references for seeding patients
            var istanbul = context.Cities.FirstOrDefault(c => c.Name == "İstanbul");
            var ankara = context.Cities.FirstOrDefault(c => c.Name == "Ankara");
            var izmir = context.Cities.FirstOrDefault(c => c.Name == "İzmir");

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

            // Seed Sample Doctors (Generating multiple doctors per department)
            var doctors = new List<User>();
            var random = new Random();
            var depts = new Dictionary<Department, string>();
            if (cardiology != null) depts.Add(cardiology, "Kardiyoloji");
            if (neurology != null) depts.Add(neurology, "Nöroloji");
            if (orthopedics != null) depts.Add(orthopedics, "Ortopedi");
            if (internalMedicine != null) depts.Add(internalMedicine, "Dahiliye");
            if (ophthalmology != null) depts.Add(ophthalmology, "Göz");
            if (ent != null) depts.Add(ent, "KBB");
            if (urology != null) depts.Add(urology, "Üroloji");
            if (gynecology != null) depts.Add(gynecology, "Kadın Doğum");
            if (pediatrics != null) depts.Add(pediatrics, "Çocuk");
            if (surgery != null) depts.Add(surgery, "Genel Cerrahi");
            if (dermatology != null) depts.Add(dermatology, "Cildiye");
            if (psychiatry != null) depts.Add(psychiatry, "Psikiyatri");
            if (radiology != null) depts.Add(radiology, "Radyoloji");
            if (emergency != null) depts.Add(emergency, "Acil");
            if (anesthesiology != null) depts.Add(anesthesiology, "Anestezi");

            int docCount = 1;
            foreach (var dept in depts)
            {
                if (dept.Key == null) continue;

                for (int i = 1; i <= 3; i++) // 3 Doctors per department
                {
                    doctors.Add(new User
                    {
                        Username = $"dr.{dept.Value.ToLower().Replace(" ", "")}{i}",
                        PasswordHash = PasswordHelper.HashPassword("123456"),
                        FirstName = $"Dr. {dept.Value} {i}",
                        LastName = $"Uzmanı",
                        Email = $"dr.{dept.Value.ToLower().Replace(" ", "")}{i}@hospital.com",
                        Role = UserRole.Doctor,
                        DepartmentId = dept.Key.Id,
                        Specialization = dept.Value,
                        MedicalLicenseNumber = $"DOC{docCount.ToString("D3")}",
                        ExperienceYears = random.Next(5, 25),
                        Education = "İstanbul Üniversitesi Tıp Fakültesi",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    });
                    docCount++;
                }
            }

            context.Users.AddRange(doctors);
            context.SaveChanges();

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
            context.Users.AddRange(nurses);

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
            context.Users.AddRange(receptionists);

            // Seed sample patients
            var samplePatients = new List<Patient>();
            for (int i = 1; i <= 10; i++)
            {
                samplePatients.Add(new Patient
                {
                    NationalId = $"{10000000000 + i}",
                    FirstName = $"Hasta{i}",
                    LastName = $"Soyad{i}",
                    BirthDate = DateTime.Now.AddYears(-random.Next(20, 80)),
                    Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
                    Phone = $"0555{i.ToString("D7")}",
                    Email = $"hasta{i}@email.com",
                    Address = "Örnek Adres",
                    BloodType = (BloodType)random.Next(0, 8),
                    CityId = istanbul?.Id,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true
                });
            }
            context.Patients.AddRange(samplePatients);
            context.SaveChanges();

            // Seed Sample Appointments
            var appointments = new List<Appointment>();
            var allDoctors = context.Users.Where(u => u.Role == UserRole.Doctor).ToList();
            var allPatients = context.Patients.ToList();
            
            if (allDoctors.Any() && allPatients.Any())
            {
                // Create past appointments (Completed)
                for (int i = 0; i < 20; i++)
                {
                    var doctor = allDoctors[random.Next(allDoctors.Count)];
                    var patient = allPatients[random.Next(allPatients.Count)];
                    var date = DateTime.Now.AddDays(-random.Next(1, 30));
                    var hour = random.Next(9, 17);
                    
                    appointments.Add(new Appointment
                    {
                        PatientId = patient.Id,
                        DoctorId = doctor.Id,
                        DepartmentId = doctor.DepartmentId,
                        AppointmentDate = date,
                        AppointmentTime = new TimeSpan(hour, 0, 0),
                        Status = AppointmentStatus.Completed,
                        Notes = "Rutin kontrol yapıldı.",
                        Symptoms = "Genel halsizlik",
                        CreatedDate = date.AddDays(-5),
                        CreatedBy = "System",
                        IsActive = true
                    });
                }

                // Create future appointments (Scheduled)
                for (int i = 0; i < 15; i++)
                {
                    var doctor = allDoctors[random.Next(allDoctors.Count)];
                    var patient = allPatients[random.Next(allPatients.Count)];
                    var date = DateTime.Now.AddDays(random.Next(1, 14));
                    var hour = random.Next(9, 17);
                    
                    appointments.Add(new Appointment
                    {
                        PatientId = patient.Id,
                        DoctorId = doctor.Id,
                        DepartmentId = doctor.DepartmentId,
                        AppointmentDate = date,
                        AppointmentTime = new TimeSpan(hour, 0, 0),
                        Status = AppointmentStatus.Scheduled,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        IsActive = true
                    });
                }
                
                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }

            // Seed sample medications
            var medications = new[]
            {
                new Medication { Name = "Paracetamol", Dosage = "500mg", Unit = "Tablet", Description = "Ağrı kesici ve ateş düşürücü", Manufacturer = "Eczacıbaşı", IsActive = true, CreatedDate = DateTime.Now, CreatedBy = "System" },
                new Medication { Name = "İbuprofen", Dosage = "400mg", Unit = "Tablet", Description = "Antiinflamatuar ve ağrı kesici", Manufacturer = "Pfizer", IsActive = true, CreatedDate = DateTime.Now, CreatedBy = "System" },
                new Medication { Name = "Amoksisilin", Dosage = "1000mg", Unit = "Tablet", Description = "Antibiyotik", Manufacturer = "İlko", IsActive = true, CreatedDate = DateTime.Now, CreatedBy = "System" },
                new Medication { Name = "Omeprazol", Dosage = "20mg", Unit = "Kapsül", Description = "Proton pompa inhibitörü", Manufacturer = "Deva", IsActive = true, CreatedDate = DateTime.Now, CreatedBy = "System" },
                new Medication { Name = "Metformin", Dosage = "850mg", Unit = "Tablet", Description = "Diyabet ilacı", Manufacturer = "Novartis", IsActive = true, CreatedDate = DateTime.Now, CreatedBy = "System" }
            };
            context.Medications.AddRange(medications);
            
            context.SaveChanges();
        }
    }
}

