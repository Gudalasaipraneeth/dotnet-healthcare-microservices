using PatientIdentityMicroservice.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PatientIdentityMicroservice.Repository;

public class UserRepository(IMongoDatabase db) : IUserRepository
{
    private readonly IMongoCollection<User> _col = db.GetCollection<User>(User.DocumentName);

    public User? GetUser(string email) =>
        _col.Find(u => u.Email == email).FirstOrDefault();

    public User? GetUserById(string userId) =>
        _col.Find(u => u.Id == userId).FirstOrDefault();

    public IList<User> GetUsersByRole(UserRole role) =>
        _col.Find(u => u.Role == role).ToList();

    public IList<User> GetDoctorsByDepartment(string department) =>
        _col.Find(u => u.Role == UserRole.Doctor && u.Department == department).ToList();

    public void InsertUser(User user) =>
        _col.InsertOne(user);

    public void UpdateUser(User user) =>
        _col.UpdateOne(u => u.Id == user.Id, Builders<User>.Update
            .Set(u => u.FirstName, user.FirstName)
            .Set(u => u.LastName, user.LastName)
            .Set(u => u.PhoneNumber, user.PhoneNumber)
            .Set(u => u.Address, user.Address)
            .Set(u => u.EmergencyContact, user.EmergencyContact)
            .Set(u => u.EmergencyContactPhone, user.EmergencyContactPhone)
            .Set(u => u.BloodType, user.BloodType)
            .Set(u => u.Allergies, user.Allergies)
            .Set(u => u.ChronicConditions, user.ChronicConditions)
            .Set(u => u.InsuranceProvider, user.InsuranceProvider)
            .Set(u => u.InsurancePolicyNumber, user.InsurancePolicyNumber)
            .Set(u => u.Department, user.Department)
            .Set(u => u.Specialization, user.Specialization)
            .Set(u => u.LicenseNumber, user.LicenseNumber));

    public void DeleteUser(string userId) =>
        _col.DeleteOne(u => u.Id == userId);
}