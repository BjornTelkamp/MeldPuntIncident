using MeldPuntIncidentApp.ViewModel;

namespace MeldPuntIncidentApp.View;

public partial class MainPage : ContentPage
{
    public IncidentViewModel incidentViewModel;
    
    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
        BindingContext = mainViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (MainViewModel)BindingContext;
        viewModel.GetIncidents();

    }

}

