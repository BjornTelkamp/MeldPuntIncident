using MeldPuntIncidentApp.Services;
using System.Collections.ObjectModel;
using Shared.Dtos;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Net;

namespace MeldPuntIncidentApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    readonly IIncidentService _incidentService;

    [ObservableProperty]
    bool isRefreshing;

    public MainViewModel(IIncidentService incidentService)
    {
        Title = "Incident list";
        _incidentService = incidentService;
        _ = GetIncidents();
    }

    [RelayCommand]
    async Task EditIncident(IncidentItemDto incident)
    {
        string incidentJson = JsonConvert.SerializeObject(incident);
        string incidentEncoded = WebUtility.UrlEncode(incidentJson);
        await Shell.Current.GoToAsync($"{nameof(IncidentEditPage)}?Incident={incidentEncoded}");
    }

    [RelayCommand]
    public async Task DeleteIncident(IncidentItemDto obj)
    { 
        if (IsBusy)
        {
            return;
        }

        try
        {
            await _incidentService.DeleteIncident(obj);
            Incidents.Remove(obj);

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        finally
        {
            IsBusy = false;
        }   
    }

    [RelayCommand]
    public async Task GetIncidents()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            IsRefreshing = true;

            var results = await _incidentService.GetIncidents();

            if(Incidents.Count > 0)
            {
                Incidents.Clear();
            }

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
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task Tap()
    {
        await Shell.Current.GoToAsync(nameof(IncidentPage));
    }

    [RelayCommand]
    async Task DetailPage(IncidentItemDto incident)
    {
        string incidentJson = JsonConvert.SerializeObject(incident);
        string incidentEncoded = WebUtility.UrlEncode(incidentJson);
        await Shell.Current.GoToAsync($"{nameof(IncidentDetailPage)}?Incident={incidentEncoded}");
    }
}
