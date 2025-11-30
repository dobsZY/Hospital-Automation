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
                // Allow staff or patients to create appointments
                // If patient, ensure they can only create appointments for themselves
                if (SessionManager.IsPatientLogin)
                {
                    var currentPatient = SessionManager.CurrentPatient;
                    if (currentPatient == null || currentPatient.Id != appointment.PatientId)
                        throw new UnauthorizedAccessException("Sadece kendi randevunuzu oluşturabilirsiniz.");
                }
                else if (!SessionManager.IsStaffLogin)
                {
                    throw new UnauthorizedAccessException("Randevu oluşturmak için giriş yapmalısınız.");
                }

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
                LogHelper.Information("CreateAppointment yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu oluşturulurken hata oluştu.", ex);
                throw new Exception($"Randevu oluşturulurken hata oluştu: {ex.Message}", ex);
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
                    throw new InvalidOperationException("Güncellenecek randevu bulunamadı");

                appointment.UpdatedDate = DateTime.Now;
                _unitOfWork.Appointments.Update(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment updated: id={appointment.Id}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("UpdateAppointment yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu güncellenirken hata oluştu.", ex);
                throw new Exception($"Randevu güncellenirken hata oluştu: {ex.Message}", ex);
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

                _unitOfWork.Appointments.Update(appointment);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Appointment cancelled: id={appointmentId}, reason='{reason}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CancelAppointment yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu iptal edilirken hata oluştu.", ex);
                throw new Exception($"Randevu iptal edilirken hata oluştu: {ex.Message}", ex);
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
                LogHelper.Information("CompleteAppointment yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu tamamlanırken hata oluştu.", ex);
                throw new Exception($"Randevu tamamlanırken hata oluştu: {ex.Message}", ex);
            }
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = _unitOfWork.Appointments.GetById(appointmentId);
                if (appointment == null)
                    return null;

                // Allow staff to view any appointment
                // Allow patients to view only their own appointments
                if (SessionManager.IsPatientLogin)
                {
                    var currentPatient = SessionManager.CurrentPatient;
                    if (currentPatient == null || currentPatient.Id != appointment.PatientId)
                        throw new UnauthorizedAccessException("Sadece kendi randevunuzu görebilirsiniz.");
                }
                else if (!SessionManager.IsStaffLogin)
                {
                    throw new UnauthorizedAccessException("Randevu görüntülemek için giriş yapmalısınız.");
                }

                return appointment;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentById yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevu getirilirken hata oluştu.", ex);
                throw new Exception($"Randevu getirilirken hata oluştu: {ex.Message}", ex);
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
                LogHelper.Information("GetAllAppointments yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Randevular getirilirken hata oluştu.", ex);
                throw new Exception($"Randevular getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId)
        {
            try
            {
                // Allow staff to view any patient's appointments
                // Allow patients to view only their own appointments
                if (SessionManager.IsPatientLogin)
                {
                    var currentPatient = SessionManager.CurrentPatient;
                    if (currentPatient == null || currentPatient.Id != patientId)
                        throw new UnauthorizedAccessException("Sadece kendi randevularınızı görebilirsiniz.");
                }
                else if (!SessionManager.IsStaffLogin)
                {
                    throw new UnauthorizedAccessException("Randevuları görüntülemek için giriş yapmalısınız.");
                }

                return _unitOfWork.Appointments.GetAppointmentsByPatient(patientId);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAppointmentsByPatient yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Hasta randevuları getirilirken hata oluştu.", ex);
                throw new Exception($"Hasta randevuları getirilirken hata oluştu: {ex.Message}", ex);
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
                LogHelper.Information("GetAppointmentsByDoctor yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Doktor randevuları getirilirken hata oluştu.", ex);
                throw new Exception($"Doktor randevuları getirilirken hata oluştu: {ex.Message}", ex);
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
                LogHelper.Information("GetAppointmentsByDate yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Tarihe göre randevular getirilirken hata oluştu.", ex);
                throw new Exception($"Tarihe göre randevular getirilirken hata oluştu: {ex.Message}", ex);
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
                LogHelper.Information("GetAppointmentsByDateRange yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Tarih aralığına göre randevular getirilirken hata oluştu.", ex);
                throw new Exception($"Tarih aralığına göre randevular getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime date, TimeSpan time)
        {
            try
            {
                // Allow both staff and patients to check time slot availability
                if (!SessionManager.IsLoggedIn)
                    throw new UnauthorizedAccessException("Zaman aralığı kontrolü için giriş yapmalısınız.");

                return _unitOfWork.Appointments.IsTimeSlotAvailable(doctorId, date, time);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("IsTimeSlotAvailable yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Zaman aralığı kontrolü yapılırken hata oluştu.", ex);
                throw new Exception($"Zaman aralığı kontrolü yapılırken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<TimeSpan> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            try
            {
                // Allow both staff and patients to get available time slots
                if (!SessionManager.IsLoggedIn)
                    throw new UnauthorizedAccessException("Müsait zaman aralıklarını görmek için giriş yapmalısınız.");

                // Basit örnek: 09:00 - 17:00 arası 30 dk aralıklarla müsait olan slotları döndür
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
                LogHelper.Information("GetAvailableTimeSlots yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Mevcut zaman aralıkları alınırken hata oluştu.", ex);
                throw new Exception($"Mevcut zaman aralıkları alınırken hata oluştu: {ex.Message}", ex);
            }
        }
    }
}

