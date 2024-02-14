using System.Collections.Immutable;
using Lora.Api.Clients.Interfaces;
using Lora.Api.DTOs.Github.Requests;
using Lora.Api.DTOs.Github.Responses;
using Lora.Api.Services.Interfaces;

namespace Lora.Api.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IGithubClient _githubClient;

    public RepositoryService(IGithubClient githubClient)
    {
        _githubClient = githubClient;
    }

    public async Task<IImmutableList<GithubRepositoryDataDTO>> ListUserRepositoryAsync(ListUserGithubRepository listGithubRepository)
    {
        var userRepositories = await _githubClient.GetUserRepositoriesAsync(listGithubRepository.UserName);
        var filteredByLanguageRepositories = userRepositories
                                                .Where(r => r.Language == "C#")
                                                .Take(5)
                                                .Select(r => new GithubRepositoryDataDTO(r.Owner.AvatarUrl, r.Name, r.Description));
                                                
        return filteredByLanguageRepositories.ToImmutableList();
    }
}