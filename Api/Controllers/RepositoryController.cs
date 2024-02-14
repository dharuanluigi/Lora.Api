using Lora.Api.DTOs.Github.Requests;
using Lora.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        var res = await _repositoryService.ListUserRepositoryAsync(new ListUserGithubRepository(user, language, order));
        return Ok(res);
    }
}
