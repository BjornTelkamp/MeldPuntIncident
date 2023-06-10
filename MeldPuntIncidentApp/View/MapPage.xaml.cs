using MeldPuntIncidentApp.ViewModel;

namespace MeldPuntIncidentApp.View;

public partial class MapPage : ContentPage
{
	public MapPage(MapViewModel mapViewModel)
	{
		InitializeComponent();
		BindingContext = mapViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		var viewModel = (MapViewModel)BindingContext;
		viewModel.GetIncidents();
    }
}