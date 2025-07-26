using MedicalServicesMicroservice.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MedicalServicesMicroservice.Repository;

public class MedicalServicesRepository(IMongoDatabase db) : IMedicalServicesRepository
{
    private readonly IMongoCollection<MedicalService> _col = db.GetCollection<MedicalService>(MedicalService.DocumentName);

    public IList<MedicalService> GetMedicalServices() =>
        _col.Find(FilterDefinition<MedicalService>.Empty).ToList();

    public IList<MedicalService> GetServicesByDepartment(string department) =>
        _col.Find(s => s.Department == department).ToList();

    public IList<MedicalService> GetServicesByDoctor(string doctorId) =>
        _col.Find(s => s.DoctorId == doctorId).ToList();

    public MedicalService GetMedicalService(string serviceId) =>
        _col.Find(s => s.Id == serviceId).FirstOrDefault();

    public void InsertMedicalService(MedicalService medicalService) =>
        _col.InsertOne(medicalService);

    public void UpdateMedicalService(MedicalService medicalService) =>
        _col.UpdateOne(s => s.Id == medicalService.Id, Builders<MedicalService>.Update
            .Set(s => s.ServiceName, medicalService.ServiceName)
            .Set(s => s.Description, medicalService.Description)
            .Set(s => s.Cost, medicalService.Cost)
            .Set(s => s.Department, medicalService.Department)
            .Set(s => s.DurationMinutes, medicalService.DurationMinutes)
            .Set(s => s.DoctorId, medicalService.DoctorId)
            .Set(s => s.DoctorName, medicalService.DoctorName)
            .Set(s => s.IsEmergencyService, medicalService.IsEmergencyService)
            .Set(s => s.Requirements, medicalService.Requirements));

    public void DeleteMedicalService(string serviceId) =>
        _col.DeleteOne(s => s.Id == serviceId);
}