using Azure.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoreProject.Common
{
    public static class TextHelper
    {
        public static string ToSlug(this string value)
        {
            return value.Trim().ToLower()
                .Replace("~", "")
                .Replace("!", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace(" ", "-")
                .Replace("/", "")
                .Replace(@"\", "")
                .Replace("<", "")
                .Replace(">", "");
        }

        public static string ConvertHtmlToText(this string text)
        {
            return Regex.Replace(text, "<.*?>", " ")
                .Replace(":&nbsp;", " ");
        }

        public static string FirstFourSentences(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var allSentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
            bool hasMore = allSentences.Length > 4;
            var sentences = allSentences.Take(4);

            string result = string.Join(" ", sentences) + (hasMore ? " ..." : "");
            return result;
        }
    }
}
