using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.Services;
using System.Diagnostics;
using Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Controls;
using MeldPuntIncidentApp.View;

namespace MeldPuntIncidentApp.ViewModel;

public partial class IncidentViewModel : BaseViewModel
{

    public MainViewModel mainViewModel;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    private readonly IIncidentService _incidentService;

    public bool IsChecked { get; set; }

    [ObservableProperty]
    public string description;

    [ObservableProperty]
    public string category;

    [ObservableProperty]
    public DateTime date = DateTime.Now;

    public IncidentViewModel(IIncidentService incidentService)
    {
        Title = "New Incident";
        _incidentService = incidentService;
    }

    [RelayCommand]
    async Task Submit()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            Location location = null;

            if (IsChecked)
            {
                location = await GetCurrentLocation();
            }

            var incident = new IncidentItemDto
            {
                Description = Description,
                Category = Category,
                Date = Date,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            Incidents.Add(incident);

            var response = await _incidentService.CreateIncident(incident);

            if (response != null)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Incident added", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"{e}");
            await App.Current.MainPage.DisplayAlert("error", e.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task<Location> GetCurrentLocation()
    {
        Location location = null;
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            _isCheckingLocation = false;
        }

        return location;
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}



