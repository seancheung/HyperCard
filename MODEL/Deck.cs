using System.ComponentModel;
using System.Reflection;

namespace MODEL
{
    /// <summary>
    /// A deck is a number of the same cards
    /// </summary>
    public class Deck : Card, INotifyPropertyChanged
    {
        private int _num;

        /// <summary>
        /// '1' for mainboard, '0' for sideboard
        /// </summary>
        public int classify;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set or get the number of the cards
        /// </summary>
        public int num
        {
            get
            {
                return this._num;
            }
            set
            {
                this._num = value;
                this.OnPropertyChanged("num");
            }
        }

        /// <summary>
        /// Initialize with a number of cards
        /// </summary>
        /// <param name="card">the card to group a deck</param>
        /// <param name="number">Amount of the cards</param>
        public Deck(Card card, int number)
        {
            this.num = number;
            if (card != null)
            {
                PropertyInfo[] properties = typeof(Card).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo propertyInfo = properties[i];
                    propertyInfo.SetValue(this, typeof(Card).GetProperty(propertyInfo.Name).GetValue(card, null), null);
                }
            }
        }

        private void OnPropertyChanged(string PropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
