namespace MeldPuntIncidentApi.Model;

public class Incident
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required DateTime Date { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? ImageUrl { get; set; }
    
}
