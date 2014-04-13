using ACCESSOR;
using MODEL;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FORMATTER
{
    internal class FormatID
    {
        private FormatID() { }

        /// <summary>
        /// Get a list of cards with ID property filled
        /// </summary>
        /// <param name="setname">Full english set name</param>
        /// <param name="setcode">Setcode in capital</param>
        /// <returns>A list of cards</returns>
        public static List<Card> GetCards(string setname, string setcode)
        {
            List<Card> result = new List<Card>();

            string webdata = Request.GetWebData(GetURL(setname, setcode));

            if (webdata.Contains("Your search returned zero results"))
            {
                result = null;
            }
            else
            {
                for (int i = webdata.IndexOf("href=\"../Card/Details.aspx?multiverseid="); i > 0; i = webdata.IndexOf("href=\"../Card/Details.aspx?multiverseid="))
                {
                    webdata = webdata.Remove(0, i + 40);
                    int length = webdata.IndexOf("\">");
                    int num = webdata.IndexOf("<td>", webdata.IndexOf("Cost:")) + 4;
                    int num2 = webdata.IndexOf("</td>", num);
                    int num3 = webdata.IndexOf("<td>", webdata.IndexOf("Rules Text:")) + 4;
                    int num4 = webdata.IndexOf("</td>", num3);
                    Card item = new Card
                    {
                        ID = webdata.Substring(0, length),
                        Cost = webdata.Substring(num, num2 - num).Trim(),
                        Text = webdata.Substring(num3, num4 - num3).Replace("<br />", "\n").Trim(),
                        Set = setname,
                        SetCode = setcode
                    };
                    result.Add(item);
                }
            }

            return result;

        }

        /// <summary>
        /// Get url of the card list data
        /// </summary>
        /// <param name="setname">Full english set name</param>
        /// <param name="setcode">Setcode in capital</param>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL(string setname, string setcode)
        {
            return string.Format("http://gatherer.wizards.com/Pages/Search/Default.aspx?output=spoiler&method=text&action=advanced&set=+%5b%22{0}%22%5d", setname.Replace(" ", "+"));
        }

    }
}
