using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

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
                AuthorizationHelper.EnsureStaff();

                if (appointment == null)
                    throw new ArgumentNullException(nameof(appointment));

                if (appointment.PatientId <= 0 || appointment.DoctorId <= 0)
                    throw new ArgumentException("Hasta ve doktor bilgisi gereklidir.");

                appointment.CreatedDate = DateTime.Now;
                appointment.IsActive = true;

                _unitOfWork.Appointments.Add(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment created: patientId={appointment.PatientId}, doctorId={appointment.DoctorId}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CreateAppointment yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu oluþturulurken hata oluþtu.", ex);
                throw new Exception($"Randevu oluþturulurken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();

                if (appointment == null)
                    throw new ArgumentNullException(nameof(appointment));

                var existing = _unitOfWork.Appointments.GetById(appointment.Id);
                if (existing == null)
                    throw new InvalidOperationException("Güncellenecek randevu bulunamadý");

                appointment.UpdatedDate = DateTime.Now;
                _unitOfWork.Appointments.Update(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment updated: id={appointment.Id}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("UpdateAppointment yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu güncellenirken hata oluþtu.", ex);
                throw new Exception($"Randevu güncellenirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool CancelAppointment(int appointmentId, string reason)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();

                var appointment = _unitOfWork.Appointments.GetById(appointmentId);
                if (appointment == null)
                    return false;

                appointment.Status = AppointmentStatus.Cancelled;
                appointment.UpdatedDate = DateTime.Now;

                // Eðer modelde CancelReason alaný varsa kullan; yoksa atla
                try
                {
                    var prop = appointment.GetType().GetProperty("CancelReason");
                    if (prop != null && prop.CanWrite)
                        prop.SetValue(appointment, reason);
                }
                catch { /* ignore */ }

                _unitOfWork.Appointments.Update(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment cancelled: id={appointmentId}, reason='{reason}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CancelAppointment yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu iptal edilirken hata oluþtu.", ex);
                throw new Exception($"Randevu iptal edilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool CompleteAppointment(int appointmentId)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();

                var appointment = _unitOfWork.Appointments.GetById(appointmentId);
                if (appointment == null)
                    return false;

                appointment.Status = AppointmentStatus.Completed;
                appointment.UpdatedDate = DateTime.Now;

                _unitOfWork.Appointments.Update(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment completed: id={appointmentId}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CompleteAppointment yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu tamamlanýrken hata oluþtu.", ex);
                throw new Exception($"Randevu tamamlanýrken hata oluþtu: {ex.Message}", ex);
            }
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetById(appointmentId);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentById yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu getirilirken hata oluþtu.", ex);
                throw new Exception($"Randevu getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetAll();
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAllAppointments yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevular getirilirken hata oluþtu.", ex);
                throw new Exception($"Randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetAppointmentsByPatient(patientId);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentsByPatient yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Hasta randevularý getirilirken hata oluþtu.", ex);
                throw new Exception($"Hasta randevularý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetAppointmentsByDoctor(doctorId);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentsByDoctor yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Doktor randevularý getirilirken hata oluþtu.", ex);
                throw new Exception($"Doktor randevularý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetAppointmentsByDate(date);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentsByDate yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Tarihe göre randevular getirilirken hata oluþtu.", ex);
                throw new Exception($"Tarihe göre randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.GetAppointmentsByDateRange(startDate, endDate);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentsByDateRange yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Tarih aralýðýna göre randevular getirilirken hata oluþtu.", ex);
                throw new Exception($"Tarih aralýðýna göre randevular getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Appointments.IsTimeSlotAvailable(doctorId, date, time);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("IsTimeSlotAvailable yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Zaman aralýðý kontrolü yapýlýrken hata oluþtu.", ex);
                throw new Exception($"Zaman aralýðý kontrolü yapýlýrken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<TimeSpan> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();

                // Basit örnek: 09:00 - 17:00 arasý 30 dk aralýklarla müsait olan slotlarý döndür
                var slots = new List<TimeSpan>();
                var start = new TimeSpan(9, 0, 0);
                var end = new TimeSpan(17, 0, 0);
                var step = TimeSpan.FromMinutes(30);

                for (var t = start; t < end; t = t.Add(step))
                {
                    if (_unitOfWork.Appointments.IsTimeSlotAvailable(doctorId, date, t))
                        slots.Add(t);
                }

                return slots;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAvailableTimeSlots yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Mevcut zaman aralýklarý alýnýrken hata oluþtu.", ex);
                throw new Exception($"Mevcut zaman aralýklarý alýnýrken hata oluþtu: {ex.Message}", ex);
            }
        }
    }
}