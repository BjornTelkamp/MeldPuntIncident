using MeldPuntIncidentApp.Model;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MeldPuntIncidentApp.Services;

public class IncidentService : IIncidentService
{
    List<IncidentItemDto> incidentList = new();
    readonly HttpClient httpClient;

    public IncidentService()
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        this.httpClient = new HttpClient(handler);
    }

    public async Task<List<IncidentItemDto>> GetIncidents()
    {
        var response = await httpClient.GetAsync("https://10.0.2.2:7108/api/incidents");

        if (response.IsSuccessStatusCode)
        {
            incidentList = await response.Content.ReadFromJsonAsync<List<IncidentItemDto>>();
        }

        return incidentList;
    }

    public async Task<IncidentItemDto> CreateIncident(IncidentItemDto incident)
    {
        var respone = await httpClient.PostAsJsonAsync("https://10.0.2.2:7108/api/incidents", incident);
        respone.EnsureSuccessStatusCode();
        var createIncident = await respone.Content.ReadFromJsonAsync<IncidentItemDto>();
        
        return createIncident;
    }

    public async Task DeleteIncident(IncidentItemDto incident)
    {
        var response = await httpClient.DeleteAsync($"https://10.0.2.2:7108/api/incidents/{incident.Id}");
        response.EnsureSuccessStatusCode();
    }
}