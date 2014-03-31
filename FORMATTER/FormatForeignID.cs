using ACCESSOR;
using MODEL;
using System.Collections.Generic;

namespace FORMATTER
{
    internal class FormatForeignID
    {
        private FormatForeignID() { }

        /// <summary>
        /// Get Foreign ID
        /// </summary>
        /// <param name="card">Card to process</param>
        /// <param name="lang">Language</param>
        /// <returns>A card with foreign ID filled</returns>
        private static Card GetCard(Card card, LANGUAGE lang)
        {
            string webdata = Request.GetWebData(GetURL(card.ID));
            if (!webdata.Contains("This card is available in the following languages:") || !webdata.Contains(lang.ToString().Replace("_", " ")))
            {
                card.zID = string.Empty;

            }
            else
            {
                webdata = webdata.Remove(webdata.IndexOf(lang.ToString().Replace("_", " ")));
                int num = webdata.LastIndexOf("multiverseid=") + 13;
                int num2 = webdata.IndexOf("\"", num);
                card.zID = webdata.Substring(num, num2 - num);
            }

            return card;

        }

        /// <summary>
        /// Get url of the card list data
        /// </summary>
        /// <param name="id">Card ID</param>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL(string id)
        {
            if (id.Contains("|"))
            {
                id = id.Remove(id.IndexOf("|"));
            }

            return string.Format("http://gatherer.wizards.com/Pages/Card/Languages.aspx?multiverseid={0}", id);
        }

        /// <summary>
        /// Get a list of cards with Foreign ID property filled
        /// </summary>
        /// <param name="cards">Cards to process</param>
        /// <param name="lang">Language</param>
        /// <returns>A list of cards</returns>
        public static List<Card> GetCards(List<Card> cards, LANGUAGE lang)
        {
            List<Card> result = new List<Card>();

            foreach (var item in cards)
            {
                double per = 1.0 * (cards.IndexOf(item) + 1) / cards.Count;
                Consoler.Output(string.Format("Total {0:P1} complete\n 2.Getting card foreign ID: {1:P1}", 0.2 + 0.2 * per, per));

                Card card = GetCard(item, lang);

                //use traditional chinese in case of simplified being unavailable
                if (card.zID == string.Empty && lang == LANGUAGE.Chinese_Simplified)
                {
                    card = GetCard(item, LANGUAGE.Chinese_Traditional);
                }

                result.Add(card);
            }

            return result;
        }

    }
}
