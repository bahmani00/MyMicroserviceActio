using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MyMicroserviceActio.Api.Controllers;
using System.Linq;
using Xunit;

namespace MyMicroserviceActio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void WeatherForecast_controller_get_should_return_string_content()
        {
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger.Object);

            var result = controller.Get();
            
            result.Should().NotBeNull();
            result.Count().Should().Be(5);
        }
    }
}