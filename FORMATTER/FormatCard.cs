using MODEL;
using System.Collections.Generic;

namespace FORMATTER
{
    public class FormatCard
    {
        private FormatCard() { }

        /// <summary>
        /// Get card ids by setname and setcode
        /// </summary>
        /// <param name="setname">Setname</param>
        /// <param name="setcode">Setcode in capital</param>
        /// <param name="lang">Additional Language</param>
        /// <returns>A list of cards with id, NULL is returned if set not found</returns>
        public static List<Card> GetIds(string setname, string setcode, LANGUAGE lang = LANGUAGE.English)
        {
            return FormatID.GetCards(setname, setcode);
        }

        /// <summary>
        /// Get a card with all properties filled
        /// </summary>
        /// <param name="card">A card with ID</param>
        /// <param name="lang">Additional Language</param>
        /// <returns>A fully formatted card, if not found, NULL is returned(Please remove)</returns>
        public static Card GetCard(Card card, LANGUAGE lang = LANGUAGE.English)
        {
            card = FormatDetail.GetCard(card);
            if (card != null)
            {
                card = FormatForeignID.GetCard(card, lang);
                card = FormatForeignDetail.GetCard(card);
                card = FormatLegality.GetCard(card);
                card = FormatEx.GetCard(card);
            }

            return card;
        }
    }
}
