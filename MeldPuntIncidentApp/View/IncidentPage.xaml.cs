using MeldPuntIncidentApp.ViewModel;

namespace MeldPuntIncidentApp.View;

public partial class IncidentPage : ContentPage
{
	public IncidentPage(IncidentViewModel incidentViewModel)
	{
		InitializeComponent();
		BindingContext = incidentViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);// set false in second page, set true in first page

    }
}