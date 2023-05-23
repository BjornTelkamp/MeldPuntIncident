using MeldPuntIncidentApp.Services;
using MeldPuntIncidentApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using System.Diagnostics;

namespace MeldPuntIncidentApp.ViewModel;

public class MainViewModel : BaseViewModel
{
    private readonly IIncidentService _incidentService;

    private ObservableCollection<IncidentItemDto> _incidents = new();

    public ObservableCollection<IncidentItemDto> Incidents
    {
        get => _incidents;
        set
        {
            _incidents = value;
            OnPropertyChanged(nameof(Incidents));
        }
    }

/*    public List<IncidentItemDto> Incidents { get; set; } = new(); 
*/
    public MainViewModel(IIncidentService incidentService)
    {
        Title = "Incident list";
        _incidentService = incidentService;
        _ = GetIncidents();
    }

    async Task GetIncidents()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            var results = await _incidentService.GetIncidents();

            results.ForEach(incident => Incidents.Add(incident));

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get incidents: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error !", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
