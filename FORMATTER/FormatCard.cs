using MODEL;
using System.Collections.Generic;

namespace FORMATTER
{
    public class FormatCard
    {
        private FormatCard() { }

        /// <summary>
        /// Get a list of cards by set
        /// </summary>
        /// <param name="setname">Full english set name</param>
        /// <param name="setcode">Setcode in capital</param>
        /// <param name="lang">Language</param>
        /// <returns>A list of fully formatted cards</returns>
        public static List<Card> GetCards(string setname, string setcode, LANGUAGE lang = LANGUAGE.Chinese_Simplified)
        {
            List<Card> cards = new List<Card>();
            Consoler.Output("Start getting ID list");
            cards = FormatID.GetCards(setname, setcode);
            Consoler.Output(string.Format("Finished getting ID list, {0} cards found",cards.Count));
            Consoler.Output("Start getting card info");
            cards = FormatDetail.GetCards(cards);
            Consoler.Output("Finished getting card info");
            Consoler.Output("Start getting card foreign ID");
            cards = FormatForeignID.GetCards(cards, lang);
            Consoler.Output("Finished getting card foreign ID");
            Consoler.Output("Start getting card foreign info");
            cards = FormatForeignDetail.GetCards(cards);
            Consoler.Output("Finished getting card foreign info");
            Consoler.Output("Start getting card legacy info");
            cards = FormatLegality.GetCards(cards);
            Consoler.Output("Finished getting card legacy info");
            Consoler.Output("Start formatting card");
            cards = FormatEx.Format(cards);
            Consoler.Output("Complete");

            return cards;
        }
    }
}
