using System.Collections.ObjectModel;
using System.Linq;

namespace MODEL
{
    /// <summary>
    /// A decklist is a group of decks
    /// </summary>
    public class DeckList : ObservableCollection<Deck>
    {
        /// <summary>
        /// Add deck to the decklist
        /// if the decklist already has the card,
        /// then the two decks are combined(add their num together)
        /// </summary>
        /// <param name="deck">the deck to add in</param>
        public void AddEx(Deck deck)
        {
            if (deck.ID != null)
            {
                if (this.Any((Deck p) => p.ID == deck.ID))
                {
                    base[base.IndexOf(this.First((Deck p) => p.ID == deck.ID))].num += deck.num;
                }
                else
                {
                    base.Add(deck);
                }
            }
        }
        /// <summary>
        /// Delete deck from the decklist by reduce num
        /// </summary>
        /// <param name="deck">teh deck to delete</param>
        public void DeleteEx(Deck deck)
        {
            if (this.Any((Deck p) => p.ID == deck.ID))
            {
                if (base[base.IndexOf(this.First((Deck p) => p.ID == deck.ID))].num > 1)
                {
                    base[base.IndexOf(this.First((Deck p) => p.ID == deck.ID))].num -= deck.num;
                }
                else
                {
                    this.RemoveEx(deck);
                }
            }
        }

        /// <summary>
        /// Remove deck from the decklist
        /// </summary>
        /// <param name="deck">the deck to remove</param>
        public void RemoveEx(Deck deck)
        {
            if (this.Any((Deck p) => p.ID == deck.ID))
            {
                base.RemoveAt(base.IndexOf(this.First((Deck p) => p.ID == deck.ID)));
            }
        }

        /// <summary>
        /// Get the exact count of the decklist by adding all deck num together
        /// </summary>
        /// <returns>the total count of the decklist</returns>
        public int CountEx()
        {
            int num = 0;
            foreach (Deck current in this)
            {
                num += current.num;
            }
            return num;
        }
    }
}
