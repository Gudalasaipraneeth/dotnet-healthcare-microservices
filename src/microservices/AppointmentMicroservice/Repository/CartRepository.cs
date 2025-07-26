using AppointmentMicroservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentMicroservice.Repository;

public class AppointmentRepository(IMongoDatabase db) : IAppointmentRepository
{
    private readonly IMongoCollection<PatientSchedule> _col = db.GetCollection<PatientSchedule>(PatientSchedule.DocumentName);

    public IList<Appointment> GetPatientAppointments(string patientId) =>
        _col
        .Find(s => s.PatientId == patientId)
        .FirstOrDefault()?.Appointments ?? new List<Appointment>();

    public IList<Appointment> GetDoctorAppointments(string doctorId, DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);
        
        var allSchedules = _col.Find(FilterDefinition<PatientSchedule>.Empty).ToList();
        return allSchedules
            .SelectMany(s => s.Appointments)
            .Where(a => a.DoctorId == doctorId && 
                       a.ScheduledDateTime >= startOfDay && 
                       a.ScheduledDateTime < endOfDay)
            .ToList();
    }

    public IList<Appointment> GetDepartmentAppointments(string department, DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);
        
        var allSchedules = _col.Find(FilterDefinition<PatientSchedule>.Empty).ToList();
        return allSchedules
            .SelectMany(s => s.Appointments)
            .Where(a => a.Department == department && 
                       a.ScheduledDateTime >= startOfDay && 
                       a.ScheduledDateTime < endOfDay)
            .ToList();
    }

    public void ScheduleAppointment(string patientId, Appointment appointment)
    {
        var schedule = _col.Find(s => s.PatientId == patientId).FirstOrDefault();
        if (schedule == null)
        {
            schedule = new PatientSchedule
            {
                PatientId = patientId,
                Appointments = new List<Appointment> { appointment }
            };
            _col.InsertOne(schedule);
        }
        else
        {
            var existingAppointment = schedule
                .Appointments
                .FirstOrDefault(a => a.ServiceId == appointment.ServiceId && 
                                   a.ScheduledDateTime == appointment.ScheduledDateTime);

            if (existingAppointment == null)
            {
                schedule.Appointments.Add(appointment);
                var update = Builders<PatientSchedule>.Update
                    .Set(s => s.Appointments, schedule.Appointments);
                _col.UpdateOne(s => s.PatientId == patientId, update);
            }
        }
    }

    public void UpdateAppointment(string patientId, Appointment appointment)
    {
        var schedule = _col.Find(s => s.PatientId == patientId).FirstOrDefault();
        if (schedule != null)
        {
            schedule.Appointments.RemoveAll(a => a.ServiceId == appointment.ServiceId);
            schedule.Appointments.Add(appointment);
            var update = Builders<PatientSchedule>.Update
                .Set(s => s.Appointments, schedule.Appointments);
            _col.UpdateOne(s => s.PatientId == patientId, update);
        }
    }

    public void CancelAppointment(string patientId, string serviceId)
    {
        var schedule = _col.Find(s => s.PatientId == patientId).FirstOrDefault();
        if (schedule != null)
        {
            schedule.Appointments.RemoveAll(a => a.ServiceId == serviceId);
            var update = Builders<PatientSchedule>.Update
                .Set(s => s.Appointments, schedule.Appointments);
            _col.UpdateOne(s => s.PatientId == patientId, update);
        }
    }

    public void UpdateAppointmentStatus(string patientId, string serviceId, AppointmentStatus status)
    {
        var schedule = _col.Find(s => s.PatientId == patientId).FirstOrDefault();
        if (schedule != null)
        {
            var appointment = schedule.Appointments.FirstOrDefault(a => a.ServiceId == serviceId);
            if (appointment != null)
            {
                appointment.Status = status;
                var update = Builders<PatientSchedule>.Update
                    .Set(s => s.Appointments, schedule.Appointments);
                _col.UpdateOne(s => s.PatientId == patientId, update);
            }
        }
    }

    public void UpdateMedicalService(string serviceId, string serviceName, decimal cost)
    {
        // Update medical service in appointments
        var schedules = GetSchedulesWithService(serviceId);
        foreach (var schedule in schedules)
        {
            var appointment = schedule.Appointments.FirstOrDefault(a => a.ServiceId == serviceId);
            if (appointment != null)
            {
                appointment.ServiceName = serviceName;
                appointment.Cost = cost;
                var update = Builders<PatientSchedule>.Update
                    .Set(s => s.Appointments, schedule.Appointments);
                _col.UpdateOne(s => s.Id == schedule.Id, update);
            }
        }
    }

    public void DeleteMedicalServiceAppointments(string serviceId)
    {
        // Cancel all appointments for this medical service
        var schedules = GetSchedulesWithService(serviceId);
        foreach (var schedule in schedules)
        {
            schedule.Appointments.RemoveAll(a => a.ServiceId == serviceId);
            var update = Builders<PatientSchedule>.Update
                .Set(s => s.Appointments, schedule.Appointments);
            _col.UpdateOne(s => s.Id == schedule.Id, update);
        }
    }

    public bool IsTimeSlotAvailable(string doctorId, DateTime dateTime, int durationMinutes)
    {
        var appointmentEnd = dateTime.AddMinutes(durationMinutes);
        var doctorAppointments = GetDoctorAppointments(doctorId, dateTime.Date);
        
        return !doctorAppointments.Any(a => 
            (dateTime >= a.ScheduledDateTime && dateTime < a.ScheduledDateTime.AddMinutes(a.DurationMinutes)) ||
            (appointmentEnd > a.ScheduledDateTime && appointmentEnd <= a.ScheduledDateTime.AddMinutes(a.DurationMinutes)) ||
            (dateTime <= a.ScheduledDateTime && appointmentEnd >= a.ScheduledDateTime.AddMinutes(a.DurationMinutes))
        );
    }

    private IList<PatientSchedule> GetSchedulesWithService(string serviceId) =>
        _col.Find(s => s.Appointments.Any(a => a.ServiceId == serviceId)).ToList();
}