using Shared.Dtos;
using MeldPuntIncidentApp.ViewModel;
using Moq;
using MeldPuntIncidentApp.Services;
using Microsoft.Maui.Controls;

namespace MeldPuntIncidentTests
{
    public class GetIncidentsTest
    {
        [Fact]
        public async Task GetIncidents()
        {
            var mockIncidentService = new Mock<IIncidentService>();
            mockIncidentService.Setup(service => service.GetIncidents())
                .ReturnsAsync(new List<IncidentItemDto> { new IncidentItemDto { Description = "test", Category = "severe", Date = DateTime.Now } });
            var viewModel = new MainViewModel(mockIncidentService.Object);

            await viewModel.GetIncidents();

            Assert.Single(viewModel.Incidents);
        }

    }
}