using MODEL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HyperCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //All cards that are loaded from database
        private ObservableCollection<Card> cards;
        //Cards from filtering result
        private ObservableCollection<Card> tmpcards;
        //Decklist that saves mainboard
        private DeckList deckmain;
        //Decklist that saves sideboard
        private DeckList deckside;

        public MainWindow()
        {
            InitializeComponent();

            deckmain = new DeckList();
            deckside = new DeckList();

            if (File.Exists("Database.db"))
            {
                cards = new ObservableCollection<Card>(new CONVERTER.LoadData().LoadDatabase("Database.db"));
            }
            else
            {
                cards = new ObservableCollection<Card>();
            }

            tmpcards = new ObservableCollection<Card>(cards);

            resultboard.ItemsSource = tmpcards;
            mainboard.ItemsSource = deckmain;
            sideboard.ItemsSource = deckside;

            //Get all sets adn add to set list
            var sets = (
                from p in this.cards
                select p.Set + "(" + p.SetCode + ")").Distinct<string>().Reverse<string>();

            foreach (string set in sets)
            {
                allsets.Items.Add(new ListBoxItem
                {
                    Content = set,
                    IsSelected = true
                });
            }

            for (int i = 0; i < 1000; i++)
            {
                deckmain.AddEx(new Deck(cards[i]));
                deckside.AddEx(new Deck(cards[999 - i]));
            }
        }

        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
