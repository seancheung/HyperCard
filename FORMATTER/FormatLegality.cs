using ACCESSOR;
using MODEL;
using System.Collections.Generic;

namespace FORMATTER
{
    internal class FormatLegality
    {
        private FormatLegality() { }

        /// <summary>
        /// Get card legacy
        /// </summary>
        /// <param name="card">Card to process</param>
        /// <returns>A card with legality filled</returns>
        private static Card GetLegality(Card card)
        {
            string webdata = Request.GetWebData(GetURL(card.ID));

            if (!webdata.Contains("This card has restrictions in the following formats"))
            {
                card.Legality = string.Empty;
                return card;
            }

            webdata = webdata.Substring(webdata.IndexOf("This card has restrictions in the following formats"), webdata.IndexOf("For more information regarding each format and play style modifications") - webdata.IndexOf("This card has restrictions in the following formats"));
            while (webdata.Contains("<td style=\"text-align:center;\">"))
            {
                int num = webdata.IndexOf("<td style=\"width:40%;\">") + 23;
                int num2 = webdata.IndexOf("</td>", num);
                string arg = webdata.Substring(num, num2 - num).Trim();
                card.Legality += string.Format("[{0}]", arg);
                webdata = webdata.Substring(webdata.IndexOf("<td style=\"text-align:center;\">") + 30);
                if (webdata.Contains("<tr class=\"cardItem oddItem\">"))
                {
                    int num3 = webdata.IndexOf("<tr class=\"cardItem oddItem\">");
                    num3 = webdata.IndexOf("<td>", num3) + 4;
                    int num4 = webdata.IndexOf("</td>", num3);
                    arg = webdata.Substring(num3, num4 - num3).Trim();
                    card.Legality += string.Format("[{0}]", arg);
                    webdata = webdata.Substring(webdata.IndexOf("<td style=\"text-align:center;\">") + 30);
                }
            }

            return card;
        }
        /// <summary>
        /// Get url of the legality data
        /// </summary>
        /// <param name="id">Card ID</param>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL(string id)
        {
            if (id.Contains("|"))
            {
                id = id.Remove(id.IndexOf("|"));
            }

            return string.Format("http://gatherer.wizards.com/Pages/Card/Printings.aspx?multiverseid={0}", id);
        }

        /// <summary>
        /// Get a card with legality filled
        /// </summary>
        /// <param name="card">Card to process</param>
        /// <returns>A ard with legality filled</returns>
        public static Card GetCard(Card card)
        {
            return GetLegality(card);
        }
    }
}
