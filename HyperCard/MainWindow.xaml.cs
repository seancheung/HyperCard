using MODEL;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FORMATTER;
using System.Windows.Media;
using System.Windows.Data;

namespace HyperCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static LANGUAGE lang { get; private set; }
        //All cards that are loaded from database
        private ObservableCollection<Card> cards;
        //Cards from filtering result
        private ObservableCollection<Card> tmpcards;
        //Decklist that saves mainboard
        private DeckList deckmain;
        //Decklist that saves sideboard
        private DeckList deckside;

        private Thread tdrefresh;
        private Thread tddownload;
        private Thread tdupdate;
        private Thread tdformat;

        public MainWindow()
        {
            InitializeComponent();

            Configs.Load();
            lang = (LANGUAGE)Enum.Parse(typeof(LANGUAGE), Configs.settings.Find(k => k.Key == "lang").Value);

            if (lang != LANGUAGE.English)
            {
                Binding displayMemberBinding = new Binding("zName");
                GridViewColumn_Name.DisplayMemberBinding = displayMemberBinding;
                GridViewColum_NameMain.DisplayMemberBinding = displayMemberBinding;
                GridViewColum_NameSide.DisplayMemberBinding = displayMemberBinding;
                imgfront.SetBinding(Image.SourceProperty, "zID");
                imgback.SetBinding(Image.SourceProperty, "zID");
                textfield.SetBinding(TextBlock.TextProperty, "zText");
            }


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
                from p in cards
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

        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            detailstab.DataContext = (sender as ListView).ItemsSource;
        }

        private void Button_REFRESH(object sender, RoutedEventArgs e)
        {
            btn_REFRESH.IsEnabled = false;
            btn_IMAGES.IsEnabled = false;
            btn_RESTORE.IsEnabled = false;
            btn_UPDATE.IsEnabled = false;

            tdrefresh = new Thread(delegate()
                {
                    var currentsets = (
                from p in cards
                select p.Set + "(" + p.SetCode + ")").Distinct<string>().Reverse<string>();

                    var allsets = FormatSets.GetSetList();

                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        if (allsets != null)
                        {
                            foreach (string current in allsets)
                            {
                                if (currentsets.Contains(current))
                                {
                                    updatesets.Items.Add(new ListBoxItem
                                    {
                                        Content = current,
                                        IsSelected = false,
                                        Foreground = new SolidColorBrush
                                        {
                                            Color = (Color)ColorConverter.ConvertFromString("#FF336B9B")
                                        }
                                    });
                                }
                                else
                                {
                                    updatesets.Items.Add(new ListBoxItem
                                    {
                                        Content = current,
                                        IsSelected = false
                                    });
                                }
                            }
                        }
                        
                        btn_REFRESH.IsEnabled = true;
                        btn_IMAGES.IsEnabled = true;
                        btn_RESTORE.IsEnabled = true;
                        btn_UPDATE.IsEnabled = true;
                    });
                }
            );

            tdrefresh.Start();
        }

    }
}
