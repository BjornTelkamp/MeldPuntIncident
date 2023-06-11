using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.Model;
using MeldPuntIncidentApp.Services;
using Newtonsoft.Json;
using Shared.Dtos;

namespace MeldPuntIncidentApp.ViewModel;

[QueryProperty("Incident", "Incident")]

public partial class IncidentEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IIncidentService _incidentService;

    public IncidentItemDto Incident { get; private set; }

    public IncidentEditViewModel(IIncidentService incidentService)
    {
        Title = "Edit Incident";
        _incidentService = incidentService;
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Incident"))
        {
            string incidentEncoded = (string)query["Incident"];
            string incidentJson = WebUtility.UrlDecode(incidentEncoded);
            Debug.WriteLine(incidentJson);
            Incident = JsonConvert.DeserializeObject<IncidentItemDto>(incidentJson);
            OnPropertyChanged(nameof(Incident));

            Debug.WriteLine("Incident property set: " + incidentJson);


        }
        else
        {
            Debug.WriteLine("Error: Incident key not found");
        }
    }

    [RelayCommand]
    async Task Save()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            var response = await _incidentService.UpdateIncident(Incident);

            if (response != null)
            {
                OnPropertyChanged(nameof(Incidents));
                await App.Current.MainPage.DisplayAlert("Success", "Incident updated", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch(Exception e)
        {
            Debug.WriteLine(e);
        }
        finally
        {
            IsBusy = false;
        }
    }


}
