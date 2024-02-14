using System.Collections.Immutable;
using System.Net;
using Lora.Api.DTOs;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace Lora.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RepositoryController : ControllerBase
{
    private IRepositoryService _repositoryService;

    public RepositoryController(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    [HttpGet("{user}/list")]
    public async Task<IActionResult> GetUserRepositoriesByLanguageAsync(
        [FromRoute] string user,
        [FromQuery(Name = "language")] string? language,
        [FromQuery(Name = "order")] string? order)
    {
        try
        {
            var userRepositories = await _repositoryService.ListUserRepositoryAsync(new ListUserGithubRepository(user, language, order));
            return Ok(new ApiResponse<GithubRepositoryDataDTO>("Respositories retrived successfully", userRepositories.Count, userRepositories));
        }
        catch (ApiException ex)
        {
            return ex.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(new ApiResponse<GithubRepositoryDataDTO>("Informed user on path was not found", 0, ImmutableList<GithubRepositoryDataDTO>.Empty)),
                _ => StatusCode(StatusCodes.Status502BadGateway, new ApiResponse<GithubRepositoryDataDTO>("The github upstream api returns unespected response", 0, ImmutableList<GithubRepositoryDataDTO>.Empty)),
            };
        }
    }
}
