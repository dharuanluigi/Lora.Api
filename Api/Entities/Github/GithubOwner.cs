using Newtonsoft.Json;

namespace Lora.Api.Entities.Github;

public record GithubOwner(
    [JsonProperty("avatar_url")]
    string AvatarUrl
);