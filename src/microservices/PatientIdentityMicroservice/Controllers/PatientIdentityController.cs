using PatientIdentityMicroservice.Model;
using PatientIdentityMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace PatientIdentityMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientIdentityController(IUserRepository userRepository, IJwtBuilder jwtBuilder, IEncryptor encryptor)
    : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] User user, [FromQuery(Name = "d")] string destination = "patient")
    {
        var u = userRepository.GetUser(user.Email);

        if (u == null)
        {
            return NotFound("User not found.");
        }

        // Role-based access control for different destinations
        if (destination == "admin" && !u.IsAdmin)
        {
            return BadRequest("Could not authenticate user.");
        }
        
        if (destination == "medical-staff" && !(u.IsDoctor || u.IsNurse || u.IsAdmin))
        {
            return BadRequest("Could not authenticate user.");
        }

        var isValid = u.ValidatePassword(user.Password, encryptor);

        if (!isValid)
        {
            return BadRequest("Could not authenticate user.");
        }

        var token = jwtBuilder.GetToken(u.Id);

        return Ok(token);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var u = userRepository.GetUser(user.Email);

        if (u != null)
        {
            return BadRequest("User already exists.");
        }

        user.SetPassword(user.Password, encryptor);
        userRepository.InsertUser(user);

        return Ok();
    }

    [HttpPost("register-patient")]
    public IActionResult RegisterPatient([FromBody] User patient)
    {
        var u = userRepository.GetUser(patient.Email);

        if (u != null)
        {
            return BadRequest("Patient already exists.");
        }

        // Ensure the user is registered as a patient
        var patientUser = new User
        {
            Email = patient.Email,
            Password = patient.Password,
            Role = UserRole.Patient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address,
            EmergencyContact = patient.EmergencyContact,
            EmergencyContactPhone = patient.EmergencyContactPhone,
            BloodType = patient.BloodType,
            Allergies = patient.Allergies,
            ChronicConditions = patient.ChronicConditions,
            InsuranceProvider = patient.InsuranceProvider,
            InsurancePolicyNumber = patient.InsurancePolicyNumber
        };

        patientUser.SetPassword(patient.Password, encryptor);
        userRepository.InsertUser(patientUser);

        return Ok();
    }

    [HttpPost("register-medical-staff")]
    public IActionResult RegisterMedicalStaff([FromBody] User medicalStaff)
    {
        var u = userRepository.GetUser(medicalStaff.Email);

        if (u != null)
        {
            return BadRequest("Medical staff already exists.");
        }

        medicalStaff.SetPassword(medicalStaff.Password, encryptor);
        userRepository.InsertUser(medicalStaff);

        return Ok();
    }

    [HttpGet("validate")]
    public IActionResult Validate([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
    {
        var u = userRepository.GetUser(email);

        if (u == null)
        {
            return NotFound("User not found.");
        }

        var userId = jwtBuilder.ValidateToken(token);

        if (userId != u.Id)
        {
            return BadRequest("Invalid token.");
        }

        return Ok(new { UserId = userId, Role = u.Role.ToString(), IsAdmin = u.IsAdmin });
    }

    [HttpGet("profile/{userId}")]
    public IActionResult GetProfile(string userId)
    {
        var user = userRepository.GetUserById(userId);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Return profile without sensitive information
        var profile = new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Role,
            user.DateOfBirth,
            user.PhoneNumber,
            user.Address,
            user.BloodType,
            user.Allergies,
            user.ChronicConditions,
            user.Department,
            user.Specialization
        };

        return Ok(profile);
    }
}