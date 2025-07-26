using AppointmentMicroservice.Model;
using System;
using System.Collections.Generic;

namespace AppointmentMicroservice.Repository;

public interface IAppointmentRepository
{
    IList<Appointment> GetPatientAppointments(string patientId);
    IList<Appointment> GetDoctorAppointments(string doctorId, DateTime date);
    IList<Appointment> GetDepartmentAppointments(string department, DateTime date);
    void ScheduleAppointment(string patientId, Appointment appointment);
    void UpdateAppointment(string patientId, Appointment appointment);
    void CancelAppointment(string patientId, string appointmentId);
    void UpdateAppointmentStatus(string patientId, string serviceId, AppointmentStatus status);
    void UpdateMedicalService(string serviceId, string serviceName, decimal cost);
    void DeleteMedicalServiceAppointments(string serviceId);
    bool IsTimeSlotAvailable(string doctorId, DateTime dateTime, int durationMinutes);
}