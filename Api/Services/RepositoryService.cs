using System.Collections.Immutable;
using Lora.Api.Clients.Interfaces;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Extensions;
using Lora.Api.Services.Interfaces;
using RestEase;

namespace Lora.Api.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IGithubClient _githubClient;

    private readonly byte MAX_RESULTS = 5;

    public RepositoryService(IGithubClient githubClient)
    {
        _githubClient = githubClient;
    }

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