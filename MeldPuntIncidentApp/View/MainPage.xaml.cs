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

    private void CreateIncident_Clicked(object sender, EventArgs e)
    {
        //Navigate to the incidentPage.
        // use the IncidentViewModel

        var incidentPage = new IncidentPage(incidentViewModel);
        Navigation.PushAsync(incidentPage);
        
        



    }

    private void OnEditTapped(object sender, TappedEventArgs e)
    {

    }

}

