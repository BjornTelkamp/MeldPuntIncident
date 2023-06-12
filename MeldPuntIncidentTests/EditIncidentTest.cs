using MeldPuntIncidentApp.Services;
using MeldPuntIncidentApp.ViewModel;
using Microsoft.Maui.Devices.Sensors;
using Moq;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MeldPuntIncidentTests
{
    public class EditIncidentTest
    {
        [Fact]
        public async Task SaveTest()
        {
            var incidentToUpdate = new IncidentItemDto { Description = "test", Category = "severe", Date = DateTime.Now };
            var mockIncidentService = new Mock<IIncidentService>();
            mockIncidentService.Setup(service => service.UpdateIncident(It.IsAny<IncidentItemDto>()))
                .ReturnsAsync(incidentToUpdate);
            var viewModel = new IncidentEditViewModel(mockIncidentService.Object);
            viewModel.Incident = incidentToUpdate;

            await viewModel.Save();

            mockIncidentService.Verify(service => service.UpdateIncident(incidentToUpdate), Times.Once());
        }


    }
}
