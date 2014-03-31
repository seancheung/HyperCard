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
        private static Card GetCard(Card card)
        {
            string webdata = Request.GetWebData(GetURL(card.ID));

            if (!webdata.Contains("This card has restrictions in the following formats"))
            {
                card.Legality = string.Empty;
                return card;
            }

            webdata = webdata.Substring(webdata.IndexOf("This card has restrictions in the following formats"), webdata.IndexOf("For more information regarding each format and play style modifications") - webdata.IndexOf("This card has restrictions in the following formats"));
            while (webdata.Contains("<td style=\"webdata-align:center;\">"))
            {
                int num = webdata.IndexOf("<td style=\"width:40%;\">") + 23;
                int num2 = webdata.IndexOf("</td>", num);
                string arg = webdata.Substring(num, num2 - num).Trim();
                card.Legality += string.Format("[{0}]", arg);
                webdata = webdata.Substring(webdata.IndexOf("<td style=\"webdata-align:center;\">") + 30);
                if (webdata.Contains("<tr class=\"cardItem oddItem\">"))
                {
                    int num3 = webdata.IndexOf("<tr class=\"cardItem oddItem\">");
                    num3 = webdata.IndexOf("<td>", num3) + 4;
                    int num4 = webdata.IndexOf("</td>", num3);
                    arg = webdata.Substring(num3, num4 - num3).Trim();
                    card.Legality += string.Format("[{0}]", arg);
                    webdata = webdata.Substring(webdata.IndexOf("<td style=\"webdata-align:center;\">") + 30);
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
        /// Get a list of cards with legality filled
        /// </summary>
        /// <param name="cards">Cards to process</param>
        /// <returns>A list of cards with legality filled</returns>
        public static List<Card> GetCards(List<Card> cards)
        {
            List<Card> result = new List<Card>();
            foreach (var item in cards)
            {
                double per = 1.0 * (cards.IndexOf(item) + 1) / cards.Count;
                Consoler.Output(string.Format("Total {0:P1} complete\n 4.Getting card legacy: {1:P1}", 0.6 + 0.2 * per, per));

                Card card = GetCard(item);
                result.Add(card);
            }

            return result;
        }
    }
}
