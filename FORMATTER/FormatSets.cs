using ACCESSOR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORMATTER
{
    public class FormatSets
    {
        /// <summary>
        /// Get all available sets
        /// </summary>
        /// <returns>A list of sets</returns>
        public static List<string> GetSetList()
        {
            string webdata = Request.GetWebData(GetURL());

            List<string> result = new List<string>();

            if (!webdata.Contains("<label for=\"edition\">Edition:</label>"))
            {
                result = null;
            }
            else
            {
                webdata = webdata.Substring(webdata.IndexOf("<label for=\"edition\">Edition:</label>"), webdata.IndexOf("<i>Use SHIFT and CTRL to select more than one edition.</i>") - webdata.IndexOf("<label for=\"edition\">Edition:</label>"));
                webdata = webdata.Substring(webdata.IndexOf("<optgroup"));

                while (webdata.Contains("value="))
                {
                    int num = webdata.IndexOf("/en\">") + 5;
                    int num2 = webdata.IndexOf("</option>", num);
                    int num3 = webdata.IndexOf("<option value=") + 15;
                    int num4 = webdata.IndexOf("/en\">", num3);
                    result.Add(String.Format("{0}({1})", webdata.Substring(num, num2 - num), webdata.Substring(num3, num4 - num3).ToUpper()));
                    webdata = webdata.Substring(num2);
                }
            }

            return result;
        }

        /// <summary>
        /// Get the set list url
        /// </summary>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL()
        {
            return string.Format(@"http://magiccards.info/search.html");
        }
    }
}
