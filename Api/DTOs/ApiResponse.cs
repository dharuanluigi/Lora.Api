using System.Collections.Immutable;

namespace Lora.Api.DTOs;

/// <summary>
/// Record to result RepositoryController resolver
/// </summary>
/// <typeparam name="T">A class to match models to return</typeparam>
/// <param name="Message">A information message about request</param>
/// <param name="Total">The lengh of results field in case sucess</param>
/// <param name="Results">Elements of T type</param>
public record ApiResponse<T>(string Message, int Total, IImmutableList<T> Results) where T : class;