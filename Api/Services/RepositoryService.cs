using System.Collections.Immutable;
using Lora.Api.Clients.Interfaces;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Extensions;
using Lora.Api.Services.Interfaces;

namespace Lora.Api.Services;

/// <summary>
/// Repository service implementation of IRepositoryService
/// </summary>
public class RepositoryService : IRepositoryService
{
    private readonly IGithubClient _githubClient;

    private readonly byte MAX_RESULTS = 5;

    /// <summary>
    /// Constructor injected dependecy
    /// </summary>
    /// <param name="githubClient">Client with github requests</param>
    public RepositoryService(IGithubClient githubClient)
    {
        _githubClient = githubClient;
    }

    /// <summary>
    /// Method to get from client list of repository and then categorized it before return to whos calls
    /// </summary>
    /// <param name="listGithubRepository">Model with necessary data to get information from upstream github api</param>
    /// <returns>List of all founded repositories</returns>
    /// <exception cref="ArgumentNullException">If argument is not present</exception>
    public async Task<IImmutableList<GithubRepositoryDataDTO>> ListUserRepositoryAsync(ListUserGithubRepository listGithubRepository)
    {
        if (listGithubRepository is null)
        {
            throw new ArgumentNullException($"Parameter: {typeof(ListUserGithubRepository)} is required, and should not be null");
        }

        listGithubRepository.Validate();

        var githubClientResponseWithRepositories = await _githubClient.GetUserRepositoriesAsync(listGithubRepository.UserName);

        var userRepositories = githubClientResponseWithRepositories.GetContent();

        var lang = listGithubRepository.Language?.ToCapitalize();

        if (lang is not null)
        {
            var filteredByLanguageRepositories = userRepositories
                                                .Where(r => r.Language == lang)
                                                .Take(MAX_RESULTS)
                                                .Select(r => new GithubRepositoryDataDTO(r.Owner.AvatarUrl, r.Name, r.Description));

            return filteredByLanguageRepositories.ToImmutableList();
        }

        var nonFilteredRepositories = userRepositories
                                        .Take(MAX_RESULTS)
                                        .Select(r => new GithubRepositoryDataDTO(r.Owner.AvatarUrl, r.Name, r.Description));

        return nonFilteredRepositories.ToImmutableList();
    }
}