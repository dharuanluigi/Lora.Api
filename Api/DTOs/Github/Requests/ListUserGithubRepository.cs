using System.ComponentModel.DataAnnotations;

namespace Lora.Api.DTOs.Github.Requests;

/// <summary>
/// Model request for centrilize data to call to service RepositoryService handle requests
/// </summary>
/// <param name="UserName">Name of user at Github</param>
/// <param name="Language">OPTIONAL: Should be encoded language name, in case name has special caracter. Like C# should be pass as: C%23 and so on</param>
/// <param name="Order">OPTIONAL: ASC or DESC. Deafult is ASC</param>
public record ListUserGithubRepository(string UserName, string? Language, string? Order)
{
    /// <summary>
    /// Method to validate model, verify if required information was passed
    /// </summary>
    /// <exception cref="ArgumentNullException">If any required information not was passed</exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrEmpty(UserName))
        {
            throw new ArgumentNullException($"Value for member: {nameof(UserName)} is mandatory");
        }
    }
}