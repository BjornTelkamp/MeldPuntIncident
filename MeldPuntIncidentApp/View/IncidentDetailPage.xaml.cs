using MeldPuntIncidentApp.ViewModel;
using Microsoft.Maui.Maps;

namespace MeldPuntIncidentApp.View;

public partial class IncidentDetailPage : ContentPage
{
	public IncidentDetailPage(IncidentDetailViewModel incidentDetailViewModel)
	{
		InitializeComponent();
		BindingContext = incidentDetailViewModel;
	}

    private void OnMoveToRegionRequested(object sender, MapSpan region)
    {
        // Set map location
        detailMap.MoveToRegion(region);
    }
}