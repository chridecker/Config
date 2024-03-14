using System.Text.RegularExpressions;

namespace UI.Constants
{
    public static partial class RegexConstants
    {
        [GeneratedRegex("(\"(\\\\u[a-zA-Z0-9]{4}|\\\\[^u]|[^\\\\\"])*\"(\\s*:)?|\\b(true|false|null)\\b|-?\\d+(?:\\.\\d*)?(?:[eE][+\\-]?\\d+)?)")]
        public static partial Regex JsonTypes();
        [GeneratedRegex("^\"")]
        public static partial Regex JsonTypeText();
        [GeneratedRegex("(?<=\").*(?=\")")]
        public static partial Regex JsonTextValue();
        [GeneratedRegex(":$")]
        public static partial Regex JsonTypeKey();
        [GeneratedRegex("true|false")]
        public static partial Regex JsonTypeString();
        [GeneratedRegex("null")]
        public static partial Regex JsonTypeNull();
        [GeneratedRegex("\\[|\\]")]
        public static partial Regex JsonTypeSquareBraket();
        [GeneratedRegex("{|}")]
        public static partial Regex JsonTypeCurvedBraket();
    }
}
