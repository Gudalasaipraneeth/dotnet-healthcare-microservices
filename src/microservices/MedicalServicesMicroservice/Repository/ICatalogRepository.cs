using MedicalServicesMicroservice.Model;
using System.Collections.Generic;

namespace MedicalServicesMicroservice.Repository;

public interface IMedicalServicesRepository
{
    IList<MedicalService> GetMedicalServices();
    IList<MedicalService> GetServicesByDepartment(string department);
    IList<MedicalService> GetServicesByDoctor(string doctorId);
    MedicalService? GetMedicalService(string serviceId);
    void InsertMedicalService(MedicalService medicalService);
    void UpdateMedicalService(MedicalService medicalService);
    void DeleteMedicalService(string serviceId);
}