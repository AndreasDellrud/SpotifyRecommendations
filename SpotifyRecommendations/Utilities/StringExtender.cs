using System;
using System.Globalization;
using System.Threading;

namespace SpotifyRecommendations.Utilities
{
    public static class StringExtender
    {
        public static string ConvertToBase64(this string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        public static string Capitalize(this string str)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(str);
        }
    }
}
