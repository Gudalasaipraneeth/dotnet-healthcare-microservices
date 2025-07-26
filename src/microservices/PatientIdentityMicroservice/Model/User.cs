using Middleware;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PatientIdentityMicroservice.Model;

public class User
{
    public static readonly string DocumentName = nameof(User);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }
    public required string Email { get; init; }
    public required string Password { get; set; }
    public string? Salt { get; set; }
    public UserRole Role { get; init; } = UserRole.Patient;
    
    // Healthcare-specific properties
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? EmergencyContact { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public string? BloodType { get; set; }
    public string[]? Allergies { get; set; }
    public string[]? ChronicConditions { get; set; }
    public string? InsuranceProvider { get; set; }
    public string? InsurancePolicyNumber { get; set; }
    
    // For medical staff
    public string? Department { get; set; }
    public string? Specialization { get; set; }
    public string? LicenseNumber { get; set; }

    public bool IsAdmin => Role == UserRole.Admin;
    public bool IsDoctor => Role == UserRole.Doctor;
    public bool IsNurse => Role == UserRole.Nurse;
    public bool IsPatient => Role == UserRole.Patient;

    public void SetPassword(string password, IEncryptor encryptor)
    {
        Salt = encryptor.GetSalt();
        Password = encryptor.GetHash(password, Salt);
    }

    public bool ValidatePassword(string password, IEncryptor encryptor) =>
        Password == encryptor.GetHash(password, Salt);
}

public enum UserRole
{
    Patient,
    Doctor,
    Nurse,
    Admin,
    Receptionist
}