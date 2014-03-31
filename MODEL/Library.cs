using System.Collections.Generic;

namespace MODEL
{
    /// <summary>
    /// A library is a stack of cards
    /// </summary>
    public class Library : Stack<Card>
    {
        /// <summary>
        /// A sideboard is a list of back-up cards
        /// </summary>
        private List<Card> sideboard = new List<Card>();

        /// <summary>
        /// Initialize the library with a list of decks
        /// </summary>
        /// <param name="decks">Decks consisting of mainboard and sideboard</param>
        public Library(List<Deck> decks)
        {
            foreach (Deck current in decks)
            {
                for (int i = 0; i < current.num; i++)
                {
                    if (current.classify == 1)
                    {
                        base.Push(current as Card);
                    }
                    else
                    {
                        this.sideboard.Add(current);
                    }
                }
            }
            this.Shuffle();
        }

        /// <summary>
        /// Shuffle the cards using Knuth-Durstenfeld Shuffle Method
        /// </summary>
        public void Shuffle()
        {
            int count = base.Count;
            List<Card> cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                cards.Add(base.Pop());
            }

            System.Random random = new System.Random();

            //pick a random card from the unshuffled cards
            //swap it with the bottom card of the unshuffled
            //mark the bottom card of the unshuffled as shuffled
            //Repeat the process
            for (int i = 0; i < cards.Count; i++)
            {
                int index = random.Next(0, cards.Count - i);
                Card temp = cards[index];
                cards[index] = cards[cards.Count - 1 - i];
                cards[cards.Count - 1 - i] = temp;
            }

            base.Clear();

            foreach (Card current in cards)
            {
                base.Push(current);
            }
        }
    }
}
