using System.Collections.Immutable;

namespace Lora.Api.DTOs;

public record ApiResponse<T>(string Message, int Total, IImmutableList<T> Results) where T : class;