using MeldPuntIncidentApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MeldPuntIncidentApi.Data;

public class IncidentDbContext : DbContext
{
    public DbSet<Incident> Incidents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=incidents.db");
    }
}
