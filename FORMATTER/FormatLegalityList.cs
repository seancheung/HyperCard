using ACCESSOR;
using MODEL;
using System;
using System.Collections.Generic;

namespace FORMATTER
{
    public class FormatLegalityList
    {
        private FormatLegalityList() { }

        /// <summary>
        /// Get a formatlist from a format
        /// </summary>
        /// <param name="format">the format to process</param>
        /// <returns>A format with properties filled</returns>
        private static Format GetFormatList(FORMAT format)
        {
            string webdata = Request.GetWebData(GetURL(format));

            webdata = webdata.Substring(webdata.IndexOf("<div class=\"article-content\">"), webdata.IndexOf("</div>", webdata.IndexOf("<div class=\"article-content\">")) - webdata.IndexOf("<div class=\"article-content\">"));
            if (!webdata.Contains("<ul>"))
            {
                return null;
            }

            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            int num = webdata.IndexOf("<ul>");
            num = webdata.IndexOf("<ul>", num);
            int num2 = webdata.IndexOf("</ul>", num);
            string text3 = webdata.Substring(num, num2 - num);
            if (!text3.Contains("keyName=\"name\""))
            {
                string[] array = text3.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty).Replace("<i>", string.Empty).Replace("</i>", string.Empty).Replace("<b>", string.Empty).Replace("</b>", string.Empty).Split(new string[]
						{
							"<li>",
							"</li>"
						}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string text4 = array[i];
                    if (!string.IsNullOrWhiteSpace(text4))
                    {
                        list.Add(FormatterTag.RemoveSpaceHtmlTag(text4));
                    }
                }
                webdata = webdata.Substring(webdata.IndexOf("</ul>", num));
                if (webdata.Contains("keyName=\"name\""))
                {
                    int num3 = webdata.IndexOf("<ul>");
                    num3 = webdata.IndexOf("<ul>", num3);
                    int num4 = webdata.IndexOf("</ul>", num3);
                    string text5 = webdata.Substring(num3, num4 - num3);
                    array = text5.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty).Replace("<i>", string.Empty).Replace("</i>", string.Empty).Replace("<b>", string.Empty).Replace("</b>", string.Empty).Split(new string[]
							{
								"<li>",
								"</li>"
							}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text4 = array[i];
                        if (!string.IsNullOrWhiteSpace(text4))
                        {
                            list2.Add(FormatterTag.RemoveSpaceHtmlTag(text4.Replace("</a>", string.Empty).Substring(text4.IndexOf(">") + 1)));
                        }
                    }
                }
            }
            else
            {
                string text5 = text3;
                string[] array = text5.Replace("<ul>", string.Empty).Replace("</ul>", string.Empty).Replace("<i>", string.Empty).Replace("</i>", string.Empty).Replace("<b>", string.Empty).Replace("</b>", string.Empty).Split(new string[]
						{
							"<li>",
							"</li>"
						}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string text4 = array[i];
                    if (!string.IsNullOrWhiteSpace(text4))
                    {
                        list2.Add(FormatterTag.RemoveSpaceHtmlTag(text4.Replace("</a>", string.Empty).Substring(text4.IndexOf(">") + 1)));
                    }
                }
            }

            return new Format(format, list, list2);
        }

        /// <summary>
        /// Get url of the format data
        /// </summary>
        /// <param name="format">Game format</param>
        /// <returns>An url for webrequesting</returns>
        private static string GetURL(FORMAT format)
        {
            string url;

            switch (format)
            {
                case FORMAT.Standard:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrstandard";
                    break;
                case FORMAT.Modern:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrmodern";
                    break;
                case FORMAT.Extended:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrextended";
                    break;
                case FORMAT.Block:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrblock";
                    break;
                case FORMAT.Vintage:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrvintage";
                    break;
                case FORMAT.Legacy:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=judge/resources/sfrlegacy";
                    break;
                case FORMAT.Classic:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=magic/rules/mtgoclassic";
                    break;
                case FORMAT.Commander:
                    url = "http://www.wizards.com/Magic/TCG/Resources.aspx?x=magic/rules/100cardsingleton-commander";
                    break;
                default:
                    url = null;
                    break;
            }

            return url;
        }

        /// <summary>
        /// Get formatlists from all formats
        /// </summary>
        /// <returns>A list of formats with properties filled</returns>
        public static List<Format> GetFormatLists()
        {
            List<Format> result = new List<Format>();
            foreach (FORMAT item in Enum.GetValues(typeof(FORMAT)))
            {
                result.Add(GetFormatList(item));
            }

            return result;
        }
    }
}
