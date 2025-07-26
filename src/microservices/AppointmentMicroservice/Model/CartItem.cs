using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AppointmentMicroservice.Model;

public class Appointment
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ServiceId { get; init; }
    public required string ServiceName { get; set; }
    public decimal Cost { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public int DurationMinutes { get; set; }
    public string? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? Department { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? PatientNotes { get; set; }
    public string? DoctorNotes { get; set; }
}

public enum AppointmentStatus
{
    Scheduled,
    Confirmed,
    InProgress,
    Completed,
    Cancelled,
    NoShow
}