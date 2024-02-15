namespace Lora.Api.Extensions;

/// <summary>
/// Handle string plus util functions
/// </summary>
public static class StringExtensions
{
    private static readonly byte FIRST_CHAR = 0;

    private static readonly byte SECOND_CHAR = 1;

    private static readonly byte FINAL_STRING = 1;

    /// <summary>
    /// Method to transform given string to capitalized one
    /// Ex: from 'word' to 'Word' 
    /// </summary>
    /// <param name="s">Any string to be capitalized</param>
    /// <returns>The same string transformed to capitalized one</returns>
    public static string ToCapitalize(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }
        
        return char.ToUpper(s[FIRST_CHAR]) + s.ToLower().Substring(SECOND_CHAR, s.Length-FINAL_STRING);
    }
}