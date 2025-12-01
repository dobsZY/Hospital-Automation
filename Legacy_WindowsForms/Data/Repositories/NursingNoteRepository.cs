using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Data.Repositories
{
    public class NursingNoteRepository : Repository<NursingNote>, INursingNoteRepository
    {
        public NursingNoteRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<NursingNote> GetNotesByPatient(int patientId)
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.PatientId == patientId)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetNotesByNurse(int nurseId)
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.NurseId == nurseId)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetNotesByType(NursingNoteType noteType)
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.NoteType == noteType)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetUrgentNotes()
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.IsUrgent)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetNotesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.NoteDateTime >= startDate && nn.NoteDateTime <= endDate)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetTodayNotes()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .Where(nn => nn.NoteDateTime >= today && nn.NoteDateTime < tomorrow)
                .OrderByDescending(nn => nn.NoteDateTime);
        }

        public IEnumerable<NursingNote> GetRecentNotes(int count = 10)
        {
            return _context.Set<NursingNote>()
                .Include(nn => nn.Patient)
                .Include(nn => nn.Nurse)
                .OrderByDescending(nn => nn.NoteDateTime)
                .Take(count);
        }
    }
}