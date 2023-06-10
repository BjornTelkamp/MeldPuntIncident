using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeldPuntIncidentApp.Model;
using MeldPuntIncidentApp.Services;
using Shared.Dtos;

namespace MeldPuntIncidentApp.ViewModel;

public partial class IncidentEditViewModel : BaseViewModel
{
    private readonly IIncidentService _incidentService;

    public IncidentItemDto Incident { get; set; }

    public IncidentEditViewModel(IIncidentService incidentService)
    {
        Title = "Edit Incident";
        _incidentService = incidentService;
    }
}
