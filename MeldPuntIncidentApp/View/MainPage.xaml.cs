using MeldPuntIncidentApp.ViewModel;

namespace MeldPuntIncidentApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
        BindingContext = mainViewModel;
    }

    private void CreateIncident_Clicked(object sender, EventArgs e)
    {
       //add functionality.
    }
}

