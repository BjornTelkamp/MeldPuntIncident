using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;

namespace MeldPuntIncidentApp.Services;

public interface IIncidentService
{
     Task<List<IncidentItemDto>> GetIncidents();
     Task<IncidentItemDto> CreateIncident(IncidentItemDto incident);

     Task DeleteIncident(IncidentItemDto incident);
}
