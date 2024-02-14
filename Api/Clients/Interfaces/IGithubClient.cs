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
    [Get("orgs/{organizationName}/repos?direction=asc")]
    Task<IImmutableList<GithubClientResponse>> GetUserRepositoriesAsync([Path] string organizationName);
}