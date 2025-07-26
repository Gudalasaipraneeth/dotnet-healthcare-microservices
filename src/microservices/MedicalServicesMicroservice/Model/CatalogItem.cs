using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicalServicesMicroservice.Model;

public class MedicalService
{
    public static readonly string DocumentName = nameof(MedicalService);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }
    public required string ServiceName { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public required string Department { get; set; }
    public int DurationMinutes { get; set; }
    public string? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public bool IsEmergencyService { get; set; }
    public string? Requirements { get; set; }
}