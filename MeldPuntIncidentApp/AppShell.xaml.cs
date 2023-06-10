using MeldPuntIncidentApp.View;

namespace MeldPuntIncidentApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(IncidentPage), typeof(IncidentPage));
		Routing.RegisterRoute(nameof(IncidentEditPage), typeof(IncidentEditPage));
		Routing.RegisterRoute(nameof(IncidentDetailPage), typeof(IncidentDetailPage));
	}
}
