using System.Text.RegularExpressions;
using System;
using System.Text;

namespace MotorMart.Core.Common
{
    public class StringHelpers
    {
        const string HTML_TAG_PATTERN = "<.*?>";

        public static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public static string RemovePathFromFile(string inputString)
        {
            // Loop backwards until we hit a / or \
            string result = String.Empty;
            for (int iLoop = inputString.Length-1; iLoop >=0 ; iLoop--)
            {
                if ((inputString.Substring(iLoop,1) == @"/" ) || (inputString.Substring(iLoop,1) == @"\" ))
                {
                    break;
                }
                result = inputString.Substring(iLoop,1) + result;
            }
            return result;
        }

        public static string TwoDigitNumber(int input)
        {
            return String.Format("{0:00}", input);
        }

        public static string GenerateRandomString(int Length)
        {
            Random rng = new Random();
            char[] valid = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < Length; i++)
            {
                sb.Append(valid[rng.Next(valid.Length)]);
            }
            return sb.ToString();
        }

        public static string GenerateUrlReference(string SourceString)
        {
            SourceString = SourceString.Replace("  ", " ");
            SourceString = SourceString.Replace(" ", "-");
            SourceString = SourceString.Replace("&", "-");
            SourceString = SourceString.Replace("?", "-");
            SourceString = SourceString.Replace("!", "-");
            SourceString = SourceString.Replace("£", "-");
            SourceString = SourceString.Replace("$", "-");
            SourceString = SourceString.Replace("%", "-");
            SourceString = SourceString.Replace("*", "-");
            SourceString = SourceString.Replace("(", "-");
            SourceString = SourceString.Replace(")", "-");
            SourceString = SourceString.Replace("+", "-");
            SourceString = SourceString.Replace("=", "-");
            SourceString = SourceString.Replace("[", "-");
            SourceString = SourceString.Replace("]", "-");
            SourceString = SourceString.Replace("@", "-");
            SourceString = SourceString.Replace(":", "-");
            SourceString = SourceString.Replace(";", "-");
            SourceString = SourceString.Replace(",", "-");
            SourceString = SourceString.Replace(".", "-");
            SourceString = SourceString.Replace("#", "-");
            SourceString = SourceString.Replace("~", "-");
            SourceString = SourceString.Replace(">", "-");
            SourceString = SourceString.Replace("<", "-");
            SourceString = SourceString.Replace("|", "-");
            SourceString = SourceString.Replace("\"", "-");
            SourceString = SourceString.Replace("'", "-");
            SourceString = SourceString.Replace("’", "");
            SourceString = SourceString.Replace(@"\", "-");
            SourceString = SourceString.Replace("/", "-");

            SourceString = SourceString.Replace("--------", "-");
            SourceString = SourceString.Replace("-------", "-");
            SourceString = SourceString.Replace("------", "-");
            SourceString = SourceString.Replace("-----", "-");
            SourceString = SourceString.Replace("----", "-");
            SourceString = SourceString.Replace("---", "-");
            SourceString = SourceString.Replace("--", "-");

            if (SourceString.EndsWith("-")) SourceString = SourceString.Substring(0, SourceString.Length - 1);
            if (SourceString.EndsWith("-")) SourceString = SourceString.Substring(0, SourceString.Length - 1);
            if (SourceString.EndsWith("-")) SourceString = SourceString.Substring(0, SourceString.Length - 1);
            if (SourceString.EndsWith("-")) SourceString = SourceString.Substring(0, SourceString.Length - 1);
            if (SourceString.EndsWith("-")) SourceString = SourceString.Substring(0, SourceString.Length - 1);

            if (SourceString.StartsWith("-")) SourceString = SourceString.Substring(1);
            if (SourceString.StartsWith("-")) SourceString = SourceString.Substring(1);
            if (SourceString.StartsWith("-")) SourceString = SourceString.Substring(1);
            if (SourceString.StartsWith("-")) SourceString = SourceString.Substring(1);
            if (SourceString.StartsWith("-")) SourceString = SourceString.Substring(1);

            SourceString = SourceString.ToLower();
            return SourceString;
        }

        public static string JsFriendly(string Source)
        {
            Source = Source.Replace("'", "&apos;");
            return Source;
        }
    }
}
