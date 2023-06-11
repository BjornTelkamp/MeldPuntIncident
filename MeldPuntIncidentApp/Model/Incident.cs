using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeldPuntIncidentApp.Model;

public class Incident
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required DateTime Date { get; set; }
    private double _latitude;
    public double Latitude
    {
        get => _latitude;
        set
        {
            _latitude = value;
            UpdateLocation();
        }
    }

    private double _longitude;
    public double Longitude
    {
        get => _longitude;
        set
        {
            _longitude = value;
            UpdateLocation();
        }
    }
    public Location Location { get; set; }

    private void UpdateLocation()
    {
        Location = new Location(Latitude, Longitude);
    }
#nullable enable
    public string? ImageUrl { get; set; }

}


