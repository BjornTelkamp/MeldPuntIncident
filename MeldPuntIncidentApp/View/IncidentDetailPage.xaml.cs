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
}