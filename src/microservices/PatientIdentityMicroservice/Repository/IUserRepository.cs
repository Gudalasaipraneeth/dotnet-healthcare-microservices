using PatientIdentityMicroservice.Model;
using System.Collections.Generic;

namespace PatientIdentityMicroservice.Repository;

public interface IUserRepository
{
    User? GetUser(string email);
    User? GetUserById(string userId);
    IList<User> GetUsersByRole(UserRole role);
    IList<User> GetDoctorsByDepartment(string department);
    void InsertUser(User user);
    void UpdateUser(User user);
    void DeleteUser(string userId);
}