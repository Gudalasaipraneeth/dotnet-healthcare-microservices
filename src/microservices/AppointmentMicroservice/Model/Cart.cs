using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AppointmentMicroservice.Model;

public class PatientSchedule
{
    public static readonly string DocumentName = nameof(PatientSchedule);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PatientId { get; init; }
    public List<Appointment> Appointments { get; init; } = new();
}