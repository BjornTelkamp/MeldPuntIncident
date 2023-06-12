using MeldPuntIncidentApp.Services;
using MeldPuntIncidentApp.ViewModel;
using Moq;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeldPuntIncidentTests
{
    public class DeleteIncidentTest
    {
        [Fact]
        public async Task DeleteIncident()
        {
            var incidentToDelete = new IncidentItemDto { Description = "test", Category = "severe", Date = DateTime.Now };
            var mockIncidentService = new Mock<IIncidentService>();
            mockIncidentService.Setup(service => service.GetIncidents())
                .ReturnsAsync(new List<IncidentItemDto> { incidentToDelete });
            var viewModel = new MainViewModel(mockIncidentService.Object);
            await viewModel.GetIncidents();

            await viewModel.DeleteIncident(incidentToDelete);

            Assert.Empty(viewModel.Incidents);
        }

    }
}
