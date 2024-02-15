using System.Collections.Immutable;
using System.Net;
using Lora.Api.DTOs;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace Lora.Api.Controllers;

/// <summary>
/// Repository controller to handle request is needed to interact with repositories
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class RepositoryController : ControllerBase
{
    private IRepositoryService _repositoryService;

    /// <summary>
    /// Controller is to handle all repository api requests
    /// </summary>
    /// <param name="repositoryService">Dependecy injected</param>
    public RepositoryController(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    /// <summary>
    /// This endpoint gets a organization repository by given language. If language is not informed, the
    /// results is comes for the first any repository appears first
    /// </summary>
    /// <param name="organization">Organization name at github</param>
    /// <param name="language">OPTIONAL: Should be encoded language name, in case name has special caracter. Like 'C#' should be pass as: 'C%23' and so on</param>
    /// <param name="order">OPTIONAL: ASC or DESC. Deafult is ASC</param>
    /// <returns></returns>
    [HttpGet("{organization}/list")]
    [ProducesResponseType(typeof(ApiResponse<GithubRepositoryDataDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserRepositoriesByLanguageAsync(
        [FromRoute] string organization,
        [FromQuery(Name = "language")] string? language,
        [FromQuery(Name = "order")] string? order)
    {
        try
        {
            var organizationRepostories = await _repositoryService.ListUserRepositoryAsync(new ListUserGithubRepository(organization, language, order));
            return Ok(new ApiResponse<GithubRepositoryDataDTO>("Respositories retrived successfully", organizationRepostories.Count, organizationRepostories));
        }
        catch (ApiException ex)
        {
            return ex.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(new ApiResponse<GithubRepositoryDataDTO>("Informed organization on path was not found", 0, ImmutableList<GithubRepositoryDataDTO>.Empty)),
                _ => StatusCode(StatusCodes.Status502BadGateway, new ApiResponse<GithubRepositoryDataDTO>("The github upstream api returns unespected response", 0, ImmutableList<GithubRepositoryDataDTO>.Empty)),
            };
        }
    }
}
