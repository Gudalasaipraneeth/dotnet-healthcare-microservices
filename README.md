# 🏥 Healthcare Management Platform
> A modern microservices-based healthcare management system

## 🚧 Project Status: In Active Development

This project is currently being developed to transform traditional e-commerce patterns into a comprehensive healthcare management platform. 

### ✅ Completed Features
- [x] Medical Services Microservice (formerly Catalog)
- [x] Patient Identity Management with healthcare roles
- [x] Basic appointment scheduling structure
- [x] Healthcare-specific data models
- [x] MongoDB integration for patient data

### 🔄 Currently Working On
- [ ] Advanced appointment conflict detection
- [ ] Doctor availability scheduling system
- [ ] Patient medical history tracking
- [ ] Insurance verification integration
- [ ] Role-based access control refinement

### 📋 Planned Features
- [ ] Real-time notifications for appointments
- [ ] Medical record document management
- [ ] Prescription management system
- [ ] Laboratory results integration
- [ ] Emergency contact alerts
- [ ] HIPAA compliance enhancements

## 🏗️ Architecture Overview

```
┌─────────────────────┐    ┌─────────────────────┐    ┌─────────────────────┐
│ Medical Services    │    │ Appointment         │    │ Patient Identity    │
│ Microservice        │    │ Microservice        │    │ Microservice        │
│                     │    │                     │    │                     │
│ • Service Catalog   │    │ • Scheduling        │    │ • User Management   │
│ • Department Mgmt   │    │ • Availability      │    │ • Role-based Auth   │
│ • Doctor Assignment │    │ • Conflict Detection│    │ • Medical Profiles  │
└─────────────────────┘    └─────────────────────┘    └─────────────────────┘
```

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK
- MongoDB 4.4+
- Docker (optional)

### Development Setup
```bash
# Clone the repository
git clone https://github.com/yourusername/dotnet-healthcare-microservices.git
cd dotnet-healthcare-microservices

# Start MongoDB
docker run -d -p 27017:27017 mongo

# Run Medical Services
cd src/microservices/MedicalServicesMicroservice
dotnet run

# Run Appointment Service (separate terminal)
cd src/microservices/AppointmentMicroservice
dotnet run

# Run Patient Identity Service (separate terminal)
cd src/microservices/PatientIdentityMicroservice
dotnet run
```

## 🏥 Healthcare Domain Models

### Medical Service
```csharp
public class MedicalService
{
    public string ServiceName { get; set; }
    public string Department { get; set; }
    public decimal Cost { get; set; }
    public int DurationMinutes { get; set; }
    public string DoctorId { get; set; }
    public bool IsEmergencyService { get; set; }
    // ... more properties
}
```

### Patient Appointment
```csharp
public class Appointment
{
    public string ServiceId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string DoctorId { get; set; }
    public AppointmentStatus Status { get; set; }
    public string PatientNotes { get; set; }
    // ... more properties
}
```

### Patient Profile
```csharp
public class User
{
    public string Email { get; set; }
    public UserRole Role { get; set; } // Patient, Doctor, Nurse, Admin
    public string BloodType { get; set; }
    public string[] Allergies { get; set; }
    public string[] ChronicConditions { get; set; }
    public string InsuranceProvider { get; set; }
    // ... more properties
}
```

## 🔧 Development Notes

### Recent Changes
- Transformed e-commerce catalog to medical services catalog
- Enhanced user model with healthcare-specific fields
- Implemented appointment scheduling with conflict detection
- Added role-based authentication for healthcare workers

### Technical Debt
- [ ] Update unit tests for healthcare scenarios
- [ ] Refactor gateway configurations
- [ ] Add comprehensive error handling
- [ ] Implement proper logging for healthcare compliance

## 🧪 Testing

```bash
# Run unit tests
dotnet test

# Run specific microservice tests
cd tests/MedicalServicesMicroservice.UnitTests
dotnet test
```

## 📝 API Documentation

### Medical Services API
- `GET /api/medicalservices` - List all services
- `GET /api/medicalservices/department/{dept}` - Services by department
- `POST /api/medicalservices` - Create new service
- `PUT /api/medicalservices` - Update service

### Appointment API
- `GET /api/appointment?patientId={id}` - Patient appointments
- `GET /api/appointment/doctor?doctorId={id}&date={date}` - Doctor schedule
- `POST /api/appointment` - Schedule appointment
- `PUT /api/appointment/status` - Update appointment status

### Patient Identity API
- `POST /api/patientidentity/register-patient` - Register patient
- `POST /api/patientidentity/register-medical-staff` - Register staff
- `GET /api/patientidentity/profile/{userId}` - Get user profile

## 🚨 Known Issues
- [ ] Appointment overlap validation needs refinement
- [ ] MongoDB connection string configuration
- [ ] JWT token expiration handling
- [ ] Docker compose service dependencies

## 📈 Progress Tracking

**Week 1:** ✅ Basic microservices transformation  
**Week 2:** 🔄 Healthcare domain modeling (in progress)  
**Week 3:** 📅 Advanced scheduling features (planned)  
**Week 4:** 📊 Dashboard and reporting (planned)  

## 🤝 Contributing

This is an active development project. Current focus areas:
1. Appointment scheduling optimization
2. Healthcare compliance features
3. Integration testing
4. Documentation improvements

## 📞 Contact

For questions about the healthcare domain implementation or architecture decisions, please open an issue.

---
*This project demonstrates modern microservices architecture applied to healthcare management systems.*
