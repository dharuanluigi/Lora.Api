using System.Collections.Immutable;
using System.Net;
using System.Text.Json;
using Lora.Api.Clients.Interfaces;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.Entities.Github;
using Lora.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestEase;

namespace Lora.Api.Tests.Services;

public class RepositoryServiceTest
{

    [Fact]
    public async void ListUserRepositoryAsync_Should_Throws_ArgumentNullException_When_Parameter_IsNull()
    {
        var mockClientDependency = new Mock<IGithubClient>();

        mockClientDependency.Setup(s => s.GetUserRepositoriesAsync(It.IsAny<string>()));

        var service = new RepositoryService(mockClientDependency.Object);

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await service.ListUserRepositoryAsync(null!));
    }

    [Fact]
    public async void ListUserRepositoryAsync_Should_Throws_ArgumentNullException_When_NullOrEmpty_Username_IsInformed()
    {
        var mockClientDependency = new Mock<IGithubClient>();

        mockClientDependency.Setup(s => s.GetUserRepositoriesAsync(It.IsAny<string>()));

        var service = new RepositoryService(mockClientDependency.Object);

        var invalidModelRequest = new ListUserGithubRepository(null!, "", "");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await service.ListUserRepositoryAsync(invalidModelRequest));
    }

    [Fact]
    public async void ListUserRepositoryAsync_Should_Return_EmptyList_When_NoRepositories_WasFounded_With_SpecifiedLanguageRepository()
    {
        var mockClientDependency = new Mock<IGithubClient>();

        var fakeApiResponseBody = "[{\"Name\":\"repo 1\",\"Description\":\"desc 1\",\"Language\":\"C#\",\"CreatedAt\":\"2024-02-15T20:57:54.647Z\",\"Owner\":{\"AvatarUrl\":\"https:\\/\\/avatar.com\"}},{\"Name\":\"repo 2\",\"Description\":\"desc 2\",\"Language\":\"Java\",\"CreatedAt\":\"2024-CreatedAt02-15T20:57:54.647Z\",\"Owner\":{\"AvatarUrl\":\"https:\\/\\/avatar.com\"}}]";
        var fakeApiResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

        var fakeEmptyListOfRepositories = Task.Run(() => 
            new Response<ImmutableList<GithubClientResponse>>(fakeApiResponseBody, fakeApiResponseMessage, () => JsonSerializer.Deserialize<ImmutableList<GithubClientResponse>>(fakeApiResponseBody)!));

        mockClientDependency.Setup(s => s.GetUserRepositoriesAsync(It.IsAny<string>())).Returns(fakeEmptyListOfRepositories);

        var service = new RepositoryService(mockClientDependency.Object);

        var responseRepositoryList = await service.ListUserRepositoryAsync(new ListUserGithubRepository("userX", "go", ""));

        Assert.Empty(responseRepositoryList);
    }

    [Fact]
    public async void ListUserRepositoryAsync_Should_Return_RepositoryData_When_WasFounded_With_SpecifiedLanguageRepository()
    {
        var mockClientDependency = new Mock<IGithubClient>();

        var fakeApiResponseBody = "[{\"Name\":\"repo 1\",\"Description\":\"desc 1\",\"Language\":\"C#\",\"CreatedAt\":\"2024-02-15T20:57:54.647Z\",\"Owner\":{\"AvatarUrl\":\"https:\\/\\/avatar.com\"}},{\"Name\":\"repo 2\",\"Description\":\"desc 2\",\"Language\":\"Java\",\"CreatedAt\":\"2024-CreatedAt02-15T20:57:54.647Z\",\"Owner\":{\"AvatarUrl\":\"https:\\/\\/avatar.com\"}}]";
        var fakeApiResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

        var fakeEmptyListOfRepositories = Task.Run(() => 
            new Response<ImmutableList<GithubClientResponse>>(fakeApiResponseBody, fakeApiResponseMessage, () => JsonSerializer.Deserialize<ImmutableList<GithubClientResponse>>(fakeApiResponseBody)!));

        mockClientDependency.Setup(s => s.GetUserRepositoriesAsync(It.IsAny<string>())).Returns(fakeEmptyListOfRepositories);

        var service = new RepositoryService(mockClientDependency.Object);

        var responseRepositoryList = await service.ListUserRepositoryAsync(new ListUserGithubRepository("userX", "C#", ""));

        Assert.NotEmpty(responseRepositoryList);
    }

    [Fact]
    public async void ListUserRepositoryAsync_Should_Return_EmptyList_When_NoRepositories_WasFounded()
    {
        var mockClientDependency = new Mock<IGithubClient>();

        var fakeNotFoundedApiResponseBody = "[]";
        var fakeApiResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

        var fakeEmptyListOfRepositories = Task.Run(() => 
            new Response<ImmutableList<GithubClientResponse>>(fakeNotFoundedApiResponseBody, fakeApiResponseMessage, () => JsonSerializer.Deserialize<ImmutableList<GithubClientResponse>>(fakeNotFoundedApiResponseBody)!));

        mockClientDependency.Setup(s => s.GetUserRepositoriesAsync(It.IsAny<string>())).Returns(fakeEmptyListOfRepositories);

        var service = new RepositoryService(mockClientDependency.Object);

        var response = await service.ListUserRepositoryAsync(new ListUserGithubRepository("userX", "", ""));

        Assert.Empty(response);
    }
}