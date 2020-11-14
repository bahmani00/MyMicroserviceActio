using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyMicroserviceActio.Api.Controllers;
using MyMicroserviceActio.Api.Repositories;
using MyMicroserviceActio.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;
using MyMicroserviceActio.Common.Services;

namespace MyMicroserviceActio.Api.Tests.Unit.Controllers
{
    public class ActivitiesControllerTests
    {
        private readonly Mock<IBusClient> busClientMock;
        private readonly Mock<IActivityRepository> activityRepositoryMock;
        private readonly ActivitiesController controller;

        public ActivitiesControllerTests()
        {
            busClientMock = new Mock<IBusClient>();
            activityRepositoryMock = new Mock<IActivityRepository>();
            controller = new ActivitiesController(busClientMock.Object, activityRepositoryMock.Object);
            var userId = Constants.UserId_Admin;
            controller.ControllerContext = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userId.ToString())
                        }, "test"))
                }
            };
        }

        [Fact]
        public async Task activities_controller_post_should_return_accepted()
        {
            var command = new CreateActivity {
                Id = Constants.ActivityId_Admin,
                UserId = Constants.UserId_Admin
            };

            var result = await controller.Post(command);

            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().Be($"activities/{command.Id}");
        }

        [Fact]
        public async Task activities_controller_getById_should_return_NotFound()
        {
            var result = await controller.Get(Guid.Empty);

            var contentResult = result as NotFoundResult;
            contentResult.Should().NotBeNull();
        }

        //[Fact]
        //public async Task activities_controller_getById_should_return_Unauthorized()
        //{
        //    var userId = Guid.Empty;
        //    controller.ControllerContext = new ControllerContext {
        //        HttpContext = new DefaultHttpContext {
        //            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //                            {
        //                    new Claim(ClaimTypes.Name, userId.ToString())
        //                            }, "test"))
        //        }
        //    };

        //    var result = await controller.Get(Constants.ActivityId_Admin);

        //    var contentResult = result as UnauthorizedResult;
        //    contentResult.Should().NotBeNull();
        //}

        //[Fact]
        //public async Task activities_controller_getById_should_return_Activity()
        //{
        //    var userId = Constants.UserId_Admin;
        //    controller.ControllerContext = new ControllerContext {
        //        HttpContext = new DefaultHttpContext {
        //            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //                {
        //                        new Claim(ClaimTypes.Name, userId.ToString())
        //                }, "test"))
        //        }
        //    };

        //    var result = await controller.Get(Constants.ActivityId_Admin);

        //    var contentResult = result as OkResult;
        //    contentResult.Should().NotBeNull();
        //}
    }
}