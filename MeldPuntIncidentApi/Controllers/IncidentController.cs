using MeldPuntIncidentApi.Data;
using MeldPuntIncidentApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeldPuntIncidentApi.Controllers;


[ApiController]
[Route("api/incidents")]
public class IncidentController : ControllerBase
{

    private readonly IncidentDbContext _dbContext;

    public IncidentController(IncidentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Incident>>> GetIncidents()
    {
        return await _dbContext.Incidents.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Incident>> GetIncidentById(int id)
    {
        return await _dbContext.Incidents.FindAsync(id);
    }

    [HttpPost]
    public IActionResult CreateIncident([FromBody] Incident incident)
    {
        _dbContext.Incidents.Add(incident);
        _dbContext.SaveChanges();

        return Ok(incident);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateIncident(int id, [FromBody] Incident updatedIncident)
    {
        var incident = _dbContext.Incidents.Find(id);
        if (incident == null)
            return NotFound();

        incident.Description = updatedIncident.Description;
        incident.Date = updatedIncident.Date;
        incident.Category = updatedIncident.Category;
        incident.Latitude = updatedIncident.Latitude;
        incident.Longitude = updatedIncident.Longitude;

        _dbContext.SaveChanges();

        return Ok(incident);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteIncident(int id)
    {
        var incident = _dbContext.Incidents.Find(id);
        if (incident == null)
            return NotFound();

        _dbContext.Incidents.Remove(incident);
        _dbContext.SaveChanges();

        return Ok();
    }
}
    
