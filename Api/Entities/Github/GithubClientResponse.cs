using Newtonsoft.Json;

namespace Lora.Api.Entities.Github;

/// <summary>
/// Entity to handle directly Github response model and get just necessary fields from upstream github api
/// </summary>
/// <param name="Name">Repository name</param>
/// <param name="Description">Repository description</param>
/// <param name="Language">Repository language</param>
/// <param name="CreatedAt">Repository created date</param>
/// <param name="Owner">Repository who is the owner</param>
public record GithubClientResponse(
    string Name,

    string Description,
    
    string Language,
    
    [JsonProperty("created_at")]
    string CreatedAt,
    
    GithubOwner Owner
);