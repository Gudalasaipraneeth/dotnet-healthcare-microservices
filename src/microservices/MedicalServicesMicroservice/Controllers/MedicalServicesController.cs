using MedicalServicesMicroservice.Model;
using MedicalServicesMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalServicesController(IMedicalServicesRepository medicalServicesRepository) : ControllerBase
{
    // GET: api/<MedicalServicesController>
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var medicalServices = medicalServicesRepository.GetMedicalServices();
        return Ok(medicalServices);
    }

    // GET api/<MedicalServicesController>/department/cardiology
    [HttpGet("department/{department}")]
    [Authorize]
    public IActionResult GetByDepartment(string department)
    {
        var services = medicalServicesRepository.GetServicesByDepartment(department);
        return Ok(services);
    }

    // GET api/<MedicalServicesController>/doctor/653e4410614d711b7fc953a7
    [HttpGet("doctor/{doctorId}")]
    [Authorize]
    public IActionResult GetByDoctor(string doctorId)
    {
        var services = medicalServicesRepository.GetServicesByDoctor(doctorId);
        return Ok(services);
    }

    // GET api/<MedicalServicesController>/653e4410614d711b7fc953a7
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(string id)
    {
        var medicalService = medicalServicesRepository.GetMedicalService(id);
        return Ok(medicalService);
    }

    // POST api/<MedicalServicesController>
    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] MedicalService medicalService)
    {
        medicalServicesRepository.InsertMedicalService(medicalService);
        return CreatedAtAction(nameof(Get), new { id = medicalService.Id }, medicalService);
    }

    // PUT api/<MedicalServicesController>
    [HttpPut]
    [Authorize]
    public IActionResult Put([FromBody] MedicalService? medicalService)
    {
        if (medicalService != null)
        {
            medicalServicesRepository.UpdateMedicalService(medicalService);
            return Ok();
        }
        return new NoContentResult();
    }

    // DELETE api/<MedicalServicesController>/653e4410614d711b7fc953a7
    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(string id)
    {
        medicalServicesRepository.DeleteMedicalService(id);
        return Ok();
    }
}