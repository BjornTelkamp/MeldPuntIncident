using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.Model;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace MeldPuntIncidentApp.ViewModel;

[QueryProperty("Incident", "Incident")]
public partial class IncidentDetailViewModel : BaseViewModel, IQueryAttributable
{
    public Incident Incident { get; private set; }

    public IncidentDetailViewModel()
    {
        Title = "Incident Detail";
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Incident"))
        {
            string incidentEncoded = (string)query["Incident"];
            string incidentJson = WebUtility.UrlDecode(incidentEncoded);
            Incident = JsonConvert.DeserializeObject<Incident>(incidentJson);
            OnPropertyChanged(nameof(Incident));
        }
        else
        {
            Debug.WriteLine("Error: Incident key not found");
        }
    }

    [RelayCommand]
    async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }

}
