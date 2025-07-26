using MedicalServicesMicroservice.Model;
using MedicalServicesMicroservice.Repository;
using System;
using System.Collections.Generic;

namespace MedicalServicesMicroservice.Data;

public static class SampleData
{
    public static List<MedicalService> GetSampleMedicalServices()
    {
        return new List<MedicalService>
        {
            new()
            {
                ServiceName = "General Consultation",
                Description = "Comprehensive general health examination and consultation",
                Cost = 150.00m,
                Department = "General Medicine",
                DurationMinutes = 30,
                DoctorId = "64a1b2c3d4e5f6789abcdef1",
                DoctorName = "Dr. Sarah Johnson",
                IsEmergencyService = false,
                Requirements = "Valid ID required"
            },
            new()
            {
                ServiceName = "Cardiology Consultation",
                Description = "Specialized cardiac examination and consultation",
                Cost = 250.00m,
                Department = "Cardiology",
                DurationMinutes = 45,
                DoctorId = "64a1b2c3d4e5f6789abcdef2",
                DoctorName = "Dr. Michael Chen",
                IsEmergencyService = false,
                Requirements = "Referral from primary care physician recommended"
            },
            new()
            {
                ServiceName = "Emergency Room Visit",
                Description = "Immediate medical attention for urgent conditions",
                Cost = 500.00m,
                Department = "Emergency Medicine",
                DurationMinutes = 60,
                DoctorId = "64a1b2c3d4e5f6789abcdef3",
                DoctorName = "Dr. Emily Rodriguez",
                IsEmergencyService = true,
                Requirements = "No appointment necessary"
            },
            new()
            {
                ServiceName = "Dental Cleaning",
                Description = "Professional dental cleaning and oral health check",
                Cost = 120.00m,
                Department = "Dentistry",
                DurationMinutes = 60,
                DoctorId = "64a1b2c3d4e5f6789abcdef4",
                DoctorName = "Dr. Robert Kim",
                IsEmergencyService = false,
                Requirements = "Regular cleaning recommended every 6 months"
            },
            new()
            {
                ServiceName = "Physical Therapy Session",
                Description = "Therapeutic exercises and rehabilitation treatment",
                Cost = 100.00m,
                Department = "Physical Therapy",
                DurationMinutes = 45,
                DoctorId = "64a1b2c3d4e5f6789abcdef5",
                DoctorName = "Dr. Lisa Thompson",
                IsEmergencyService = false,
                Requirements = "Doctor's prescription required"
            },
            new()
            {
                ServiceName = "MRI Scan",
                Description = "Magnetic Resonance Imaging for detailed body scans",
                Cost = 800.00m,
                Department = "Radiology",
                DurationMinutes = 90,
                DoctorId = "64a1b2c3d4e5f6789abcdef6",
                DoctorName = "Dr. David Park",
                IsEmergencyService = false,
                Requirements = "Doctor's order and pre-screening questionnaire required"
            },
            new()
            {
                ServiceName = "Blood Work Panel",
                Description = "Comprehensive blood analysis and lab testing",
                Cost = 75.00m,
                Department = "Laboratory",
                DurationMinutes = 15,
                DoctorId = "64a1b2c3d4e5f6789abcdef7",
                DoctorName = "Dr. Jennifer Wu",
                IsEmergencyService = false,
                Requirements = "Fasting may be required for certain tests"
            },
            new()
            {
                ServiceName = "Pediatric Consultation",
                Description = "Specialized medical care for children and adolescents",
                Cost = 180.00m,
                Department = "Pediatrics",
                DurationMinutes = 30,
                DoctorId = "64a1b2c3d4e5f6789abcdef8",
                DoctorName = "Dr. Amanda Foster",
                IsEmergencyService = false,
                Requirements = "Parent or guardian must accompany minors"
            }
        };
    }
}
