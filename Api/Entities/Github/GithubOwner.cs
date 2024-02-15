using Newtonsoft.Json;

namespace Lora.Api.Entities.Github;

/// <summary>
/// Model to handle directly github model response body from upstream api
/// </summary>
/// <param name="AvatarUrl">Url image profile at github</param>
public record GithubOwner(
    [JsonProperty("avatar_url")]
    string AvatarUrl
);