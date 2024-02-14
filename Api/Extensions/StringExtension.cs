namespace Lora.Api.Extensions;

public static class StringExtensions
{
    private static readonly byte FIRST_CHAR = 0;

    private static readonly byte SECOND_CHAR = 1;

    private static readonly byte FINAL_STRING = 1;

    public static string ToCapitalize(this string s)
    {
        return char.ToUpper(s[FIRST_CHAR]) + s.ToLower().Substring(SECOND_CHAR, s.Length-FINAL_STRING);
    }
}