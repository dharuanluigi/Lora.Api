using System.Collections.Immutable;
using System.Net;
using Lora.Api.Controllers;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestEase;

namespace Lora.Api.Tests.Controllers;

public class RepositoryControllerTest
{
    [Fact]
    public async void GetUserRepositoriesByLanguageAsync_Should_Return_200OK_When_UserRepository_IsFounded()
    {
        var mockServiceDependency = new Mock<IRepositoryService>();
        
        var fakeRepositoryData = new GithubRepositoryDataDTO("https://avatar.com", "repo name", "repo desc");
        var fakeListOfRepositoryData = ImmutableList.Create(fakeRepositoryData);
        var fakeReturnListRepositoryData = Task.Run(() => fakeListOfRepositoryData);
        
        mockServiceDependency.Setup(s => s.ListUserRepositoryAsync(It.IsAny<ListUserGithubRepository>()))
        .Returns(fakeReturnListRepositoryData);

        var controller = new RepositoryController(mockServiceDependency.Object);

        var apiResponse = await controller.GetUserRepositoriesByLanguageAsync("userX", "C%23", null);

        var controllerResponse = apiResponse as ObjectResult;
        Assert.Equal(StatusCodes.Status200OK, controllerResponse?.StatusCode);
    }

    [Fact]
    public async void GetUserRepositoriesByLanguageAsync_Should_Return_404NotFound_When_NoUsername_IsFounded()
    {
        var mockServiceDependency = new Mock<IRepositoryService>();

        var fakeHttpRequest = new HttpRequestMessage(HttpMethod.Get, "Http://localhost");
        var fakeHttpResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
        
        mockServiceDependency.Setup(s => s.ListUserRepositoryAsync(It.IsAny<ListUserGithubRepository>()))
        .Throws(new ApiException(fakeHttpRequest, fakeHttpResponse, "[]"));

        var controller = new RepositoryController(mockServiceDependency.Object);

        var apiResponse = await controller.GetUserRepositoriesByLanguageAsync("userY", "C%23", null);

        var controllerResponse = apiResponse as ObjectResult;
        Assert.Equal(StatusCodes.Status404NotFound, controllerResponse?.StatusCode);
    }
}