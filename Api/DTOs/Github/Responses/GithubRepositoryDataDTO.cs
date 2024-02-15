namespace Lora.Api.DTOs.Github.Responses;

/// <summary>
/// DTO to get data from service and pass back to controller whos calls
/// </summary>
/// <param name="Avatar">URL of github image profile</param>
/// <param name="Name">Repository name</param>
/// <param name="Description">Repository description</param>
public record GithubRepositoryDataDTO(
    string Avatar,
    string Name,
    string Description
);