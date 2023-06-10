using MeldPuntIncidentApp.ViewModel;

namespace MeldPuntIncidentApp.View;

public partial class IncidentEditPage : ContentPage
{
	public IncidentEditPage(IncidentEditViewModel incidentEditViewModel)
	{
		InitializeComponent();
		BindingContext = incidentEditViewModel;
	}
}