using AppointmentMicroservice.Model;
using AppointmentMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppointmentMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IAppointmentRepository appointmentRepository) : ControllerBase
{
    // GET: api/<AppointmentController>?patientId=653e43b8c76b6b56a720803e
    [HttpGet]
    [Authorize]
    public IActionResult Get([FromQuery(Name = "patientId")] string patientId)
    {
        var appointments = appointmentRepository.GetPatientAppointments(patientId);
        return Ok(appointments);
    }

    // GET: api/<AppointmentController>/doctor?doctorId=653e43b8c76b6b56a720803e&date=2025-07-24
    [HttpGet("doctor")]
    [Authorize]
    public IActionResult GetDoctorAppointments([FromQuery(Name = "doctorId")] string doctorId, 
                                               [FromQuery(Name = "date")] DateTime date)
    {
        var appointments = appointmentRepository.GetDoctorAppointments(doctorId, date);
        return Ok(appointments);
    }

    // GET: api/<AppointmentController>/department?department=Cardiology&date=2025-07-24
    [HttpGet("department")]
    [Authorize]
    public IActionResult GetDepartmentAppointments([FromQuery(Name = "department")] string department, 
                                                   [FromQuery(Name = "date")] DateTime date)
    {
        var appointments = appointmentRepository.GetDepartmentAppointments(department, date);
        return Ok(appointments);
    }

    // GET: api/<AppointmentController>/availability?doctorId=653e43b8c76b6b56a720803e&dateTime=2025-07-24T10:00:00&duration=30
    [HttpGet("availability")]
    [Authorize]
    public IActionResult CheckAvailability([FromQuery(Name = "doctorId")] string doctorId,
                                          [FromQuery(Name = "dateTime")] DateTime dateTime,
                                          [FromQuery(Name = "duration")] int durationMinutes)
    {
        var isAvailable = appointmentRepository.IsTimeSlotAvailable(doctorId, dateTime, durationMinutes);
        return Ok(new { IsAvailable = isAvailable });
    }

    // POST api/<AppointmentController>
    [HttpPost]
    [Authorize]
    public IActionResult Post([FromQuery(Name = "patientId")] string patientId, [FromBody] Appointment appointment)
    {
        // Validate appointment time availability
        if (!appointmentRepository.IsTimeSlotAvailable(appointment.DoctorId, appointment.ScheduledDateTime, appointment.DurationMinutes))
        {
            return BadRequest("The selected time slot is not available.");
        }

        appointmentRepository.ScheduleAppointment(patientId, appointment);
        return Ok();
    }

    // PUT api/<AppointmentController>
    [HttpPut]
    [Authorize]
    public IActionResult Put([FromQuery(Name = "patientId")] string patientId, [FromBody] Appointment appointment)
    {
        // Validate appointment time availability for rescheduling
        if (!appointmentRepository.IsTimeSlotAvailable(appointment.DoctorId, appointment.ScheduledDateTime, appointment.DurationMinutes))
        {
            return BadRequest("The selected time slot is not available.");
        }

        appointmentRepository.UpdateAppointment(patientId, appointment);
        return Ok();
    }

    // PUT api/<AppointmentController>/status
    [HttpPut("status")]
    [Authorize]
    public IActionResult UpdateStatus([FromQuery(Name = "patientId")] string patientId,
                                     [FromQuery(Name = "serviceId")] string serviceId,
                                     [FromQuery(Name = "status")] AppointmentStatus status)
    {
        appointmentRepository.UpdateAppointmentStatus(patientId, serviceId, status);
        return Ok();
    }

    // DELETE api/<AppointmentController>
    [HttpDelete]
    [Authorize]
    public IActionResult Delete([FromQuery(Name = "patientId")] string patientId, 
                                [FromQuery(Name = "serviceId")] string serviceId)
    {
        appointmentRepository.CancelAppointment(patientId, serviceId);
        return Ok();
    }

    // PUT api/<AppointmentController>/update-medical-service
    [HttpPut("update-medical-service")]
    [Authorize]
    public IActionResult Put([FromQuery(Name = "serviceId")] string serviceId, 
                             [FromQuery(Name = "serviceName")] string serviceName, 
                             [FromQuery(Name = "cost")] decimal cost)
    {
        appointmentRepository.UpdateMedicalService(serviceId, serviceName, cost);
        return Ok();
    }

    // DELETE api/<AppointmentController>/delete-medical-service
    [HttpDelete("delete-medical-service")]
    [Authorize]
    public IActionResult Delete([FromQuery(Name = "serviceId")] string serviceId)
    {
        appointmentRepository.DeleteMedicalServiceAppointments(serviceId);
        return Ok();
    }
}