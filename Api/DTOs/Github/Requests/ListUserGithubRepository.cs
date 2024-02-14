using System.ComponentModel.DataAnnotations;

namespace Lora.Api.DTOs.Github.Requests;

public record ListUserGithubRepository(string UserName, string? Language, string? Order)
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrEmpty(UserName))
        {
            throw new ArgumentNullException($"Value for member: {nameof(UserName)} is mandatory");
        }
    }
}