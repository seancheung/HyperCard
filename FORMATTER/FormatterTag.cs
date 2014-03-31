using System.Text.RegularExpressions;

namespace FORMATTER
{
    internal class FormatterTag
    {
        private FormatterTag() { }

        /// <summary>
        /// Remove all html tags
        /// </summary>
        /// <param name="Input">String to format</param>
        /// <returns></returns>
        public static string RemoveSpaceHtmlTag(string Input)
        {
            string text = Regex.Replace(Input, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "-->", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "<!--.*", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "&#(\\d+);", "", RegexOptions.IgnoreCase);
            text.Replace("<", "");
            text.Replace(">", "");
            text.Replace(System.Environment.NewLine, "");
            return Regex.Replace(text.Trim(), "\\s+", " ");
        }
    }
}
