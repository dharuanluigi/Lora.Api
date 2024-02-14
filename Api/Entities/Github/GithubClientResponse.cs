using Newtonsoft.Json;

namespace Lora.Api.Entities.Github;

public record GithubClientResponse(
    string Name,

    string Description,
    
    string Language,
    
    [JsonProperty("created_at")]
    string CreatedAt,
    
    GithubOwner Owner
);