using System.Collections.Immutable;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;

namespace Lora.Api.Services.Interfaces;

/// <summary>
/// Interface to handle current repository service that was used for
/// </summary>
public interface IRepositoryService
{
    Task<IImmutableList<GithubRepositoryDataDTO>> ListUserRepositoryAsync(ListUserGithubRepository listGithubRepository);
}