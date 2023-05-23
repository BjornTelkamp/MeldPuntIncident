using MeldPuntIncidentApi.Data;
using MeldPuntIncidentApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MeldPuntIncidentApi.Services;

public class IncidentApi
{
    public async Task<List<Incident>> GetIncidentsAsync()
    {
        using (var db = new IncidentDbContext())
        {
            return await db.Incidents.ToListAsync();
        }
    }

    public async Task<Incident> GetIncidentByIdAsync(int id)
    {
        using (var db = new IncidentDbContext())
        {
            return await db.Incidents.FindAsync(id);
        }
    }

    public async Task AddIncidentAsync(Incident incident)
    {
        using (var db = new IncidentDbContext())
        {
            db.Incidents.Add(incident);
            await db.SaveChangesAsync();
        }
    }

    public async Task UpdateIncidentAsync(Incident incident)
    {
        using (var db = new IncidentDbContext())
        {
            db.Incidents.Update(incident);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteIncidentAsync(int id)
    {
        using (var db = new IncidentDbContext())
        {
            var incident = await db.Incidents.FindAsync(id);
            if (incident != null)
            {
                db.Incidents.Remove(incident);
                await db.SaveChangesAsync();
            }
        }
    }
}