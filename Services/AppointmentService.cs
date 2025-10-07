using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services.Interfaces;

namespace HospitalAutomation.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public bool CreateAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                    throw new ArgumentNullException(nameof(appointment));

                // Validate appointment data
                if (appointment.PatientId <= 0)
                    throw new ArgumentException("Geçerli bir hasta seçiniz");

                if (appointment.DoctorId <= 0)
                    throw new ArgumentException("Geçerli bir doktor seçiniz");

                if (appointment.AppointmentDate < DateTime.Today)
                    throw new ArgumentException("Geçmiþ tarihli randevu oluþturulamaz");

                // Check if patient exists
                var patient = _unitOfWork.Patients.GetById(appointment.PatientId);
                if (patient == null)
                    throw new InvalidOperationException("Hasta bulunamadý");

                // Check if doctor exists
                var doctor = _unitOfWork.Users.GetById(appointment.DoctorId);
                if (doctor == null || doctor.Role != UserRole.Doctor)
                    throw new InvalidOperationException("Doktor bulunamadý");

                // Check if time slot is available
                if (!_unitOfWork.Appointments.IsTimeSlotAvailable(appointment.DoctorId, appointment.AppointmentDate, appointment.AppointmentTime))
                    throw new InvalidOperationException("Seçilen saat dilimi müsait deðil");

                _unitOfWork.Appointments.Add(appointment);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu oluþturulurken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                    throw new ArgumentNullException(nameof(appointment));

                var existingAppointment = _unitOfWork.Appointments.GetById(appointment.Id);
                if (existingAppointment == null)
                    throw new InvalidOperationException("Randevu bulunamadý");

                // Check if time slot is available (excluding current appointment)
                if ((existingAppointment.DoctorId != appointment.DoctorId ||
                     existingAppointment.AppointmentDate != appointment.AppointmentDate ||
                     existingAppointment.AppointmentTime != appointment.AppointmentTime) &&
                    !_unitOfWork.Appointments.IsTimeSlotAvailable(appointment.DoctorId, appointment.AppointmentDate, appointment.AppointmentTime))
                {
                    throw new InvalidOperationException("Seçilen saat dilimi müsait deðil");
                }

                _unitOfWork.Appointments.Update(appointment);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu güncellenirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool CancelAppointment(int appointmentId, string reason)
        {
            try
            {
                var appointment = _unitOfWork.Appointments.GetById(appointmentId);
                if (appointment == null)
                    return false;

                appointment.Status = AppointmentStatus.Cancelled;
                appointment.Notes = $"{appointment.Notes}\nÝptal Nedeni: {reason}".Trim();
                _unitOfWork.Appointments.Update(appointment);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu iptal edilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        // Overloaded method for backward compatibility
        public bool CancelAppointment(int appointmentId)
        {
            return CancelAppointment(appointmentId, "");
        }

        public bool CompleteAppointment(int appointmentId)
        {
            try
            {
                var appointment = _unitOfWork.Appointments.GetById(appointmentId);
                if (appointment == null)
                    return false;

                appointment.Status = AppointmentStatus.Completed;
                appointment.CompletedDate = DateTime.Now;
                _unitOfWork.Appointments.Update(appointment);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu tamamlanýrken hata oluþtu: {ex.Message}", ex);
            }
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                return _unitOfWork.Appointments.GetById(appointmentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            try
            {
                return _unitOfWork.Appointments.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId)
        {
            try
            {
                return _unitOfWork.Appointments.GetByPatientId(patientId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Hasta randevularý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            try
            {
                return _unitOfWork.Appointments.GetByDoctorId(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktor randevularý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            try
            {
                return _unitOfWork.Appointments.GetByDate(date);
            }
            catch (Exception ex)
            {
                throw new Exception($"Tarih bazlý randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _unitOfWork.Appointments.GetByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Tarih aralýðý bazlý randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time)
        {
            try
            {
                return _unitOfWork.Appointments.IsTimeSlotAvailable(doctorId, date, time);
            }
            catch (Exception ex)
            {
                throw new Exception($"Saat dilimi kontrolü sýrasýnda hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<TimeSpan> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            try
            {
                var allTimeSlots = GenerateTimeSlots();
                var bookedSlots = _unitOfWork.Appointments.GetByDate(date)
                    .Where(a => a.DoctorId == doctorId && 
                               a.Status != AppointmentStatus.Cancelled)
                    .Select(a => a.AppointmentTime)
                    .ToList();

                return allTimeSlots.Except(bookedSlots).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Müsait saat dilimleri getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        private IEnumerable<TimeSpan> GenerateTimeSlots()
        {
            var timeSlots = new List<TimeSpan>();
            
            // Working hours: 09:00 - 17:00, 30 minute intervals
            for (int hour = 9; hour <= 17; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    // Skip lunch break (12:00 - 13:00)
                    if (hour == 12)
                        continue;
                        
                    timeSlots.Add(new TimeSpan(hour, minute, 0));
                }
            }
            
            return timeSlots;
        }
    }
}