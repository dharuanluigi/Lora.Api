using System.Collections.Immutable;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;

namespace Lora.Api.Services.Interfaces;

/// <summary>
/// Interface to handle current repository service that was used for make requests
/// </summary>
public interface IRepositoryService
{
    /// <summary>
    /// This method call to client to get a list of repositories by given username
    /// </summary>
    /// <param name="listGithubRepository">Necessary record with required username and other filters if needed</param>
    /// <returns>An immutable list with founded repository with it data</returns>
    Task<IImmutableList<GithubRepositoryDataDTO>> ListUserRepositoryAsync(ListUserGithubRepository listGithubRepository);
}