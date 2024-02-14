namespace Lora.Api.DTOs.Github.Requests;

public record ListUserGithubRepository(
    string UserName,
    string Order,
    string Language
);