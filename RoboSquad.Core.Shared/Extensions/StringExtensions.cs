namespace RoboSquad.Core.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAllWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
