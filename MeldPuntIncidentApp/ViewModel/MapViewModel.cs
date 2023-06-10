using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.Model;
using MeldPuntIncidentApp.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeldPuntIncidentApp.ViewModel;

public partial class MapViewModel : BaseViewModel
{
    private readonly IIncidentService _incidentService;

    private ObservableCollection<Incident> _positions;
    public IEnumerable<Incident> Positions => _positions;

    public MapViewModel(IIncidentService incidentService)
    {
        Title = "Incidents on map";
        _positions = new ObservableCollection<Incident>();
        _incidentService = incidentService;
    }

    [RelayCommand]
    public async void GetIncidents()
    {
        var incidents = await _incidentService.GetIncidents();

        if (incidents != null)
        {
            foreach (var incident in incidents)
            {
                _positions.Add(new Incident
                {
                    Description = incident.Description,
                    Category = incident.Category,
                    Date = incident.Date,
                    Latitude = incident.Latitude,
                    Longitude = incident.Longitude,
                    Location = new Location(incident.Latitude, incident.Longitude)
                });
               
            }
        }
    }
}