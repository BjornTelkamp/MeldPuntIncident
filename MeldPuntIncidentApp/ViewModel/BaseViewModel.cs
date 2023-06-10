using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Shared.Dtos;

namespace MeldPuntIncidentApp.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title;

    
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
}
