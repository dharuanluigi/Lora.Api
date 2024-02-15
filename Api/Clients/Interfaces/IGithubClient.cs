using System.Collections.Immutable;
using Lora.Api.Entities.Github;
using RestEase;

namespace Lora.Api.Clients.Interfaces;

/// <summary>
/// Interface to hable github endpoint requests
/// </summary>
[Header("User-Agent", "Lora.API.Client")]
public interface IGithubClient
{
    /// <summary>
    /// This method is injected into container to handle requests for github endpoint to get repositories
    /// by organization username
    /// </summary>
    /// <param name="organizationName">Organization username at github</param>
    /// <returns>Response with content and status code comes from github upstream api</returns>
    [Get("orgs/{organizationName}/repos?direction=asc")]
    Task<Response<ImmutableList<GithubClientResponse>>> GetUserRepositoriesAsync([Path] string organizationName);
}