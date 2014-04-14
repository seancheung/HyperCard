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
using System.Collections.Generic;

namespace HyperCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Current language setting
        public static LANGUAGE lang { get; private set; }
        //All cards that are loaded from database
        private List<Card> cards;
        //Cards from filtering result
        private List<Card> tmpcards;
        //Decklist that saves mainboard
        private DeckList deckmain;
        //Decklist that saves sideboard
        private DeckList deckside;

        //Data refreshing thread
        private Thread tdrefresh;
        private Thread tddownload;
        private Thread tdupdate;

        public MainWindow()
        {
            InitializeComponent();

            //Load application settings
            Configs.Load();
            //Set language
            //lang = (LANGUAGE)Enum.Parse(typeof(LANGUAGE), Configs.settings.Find(k => k.Key == "lang").Value);
            lang = LANGUAGE.Chinese_Simplified;

            //Match language setting with data binding
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

            //Load database
            if (File.Exists("Database.db"))
            {
                cards = new CONVERTER.LoadData().LoadDatabase("Database.db");
            }
            else
            {
                cards = new List<Card>();
            }

            tmpcards = new List<Card>(cards);

            resultboard.ItemsSource = tmpcards;
            mainboard.ItemsSource = deckmain;
            sideboard.ItemsSource = deckside;

            //Get all sets adn add to set list
            var sets = (
                from p in cards
                select p.Set + "(" + p.SetCode + ")").Distinct<string>().Reverse<string>();

            //Add all local sets to setslistbox
            foreach (string set in sets)
            {
                allsets.Items.Add(new ListBoxItem
                {
                    Content = set,
                    IsSelected = true
                });
            }

            //for (int i = 0; i < 1000; i++)
            //{
            //    deckmain.AddEx(new Deck(cards[i]));
            //    deckside.AddEx(new Deck(cards[999 - i]));
            //}
        }

        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Binding current selected item to detailstab
            detailstab.DataContext = (sender as ListView).ItemsSource;
        }

        private void btn_REFRESH_Click(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            tdrefresh = new Thread(delegate()
                {
                    //Get all sets form local database
                    var currentsets = (
                from p in cards
                select p.Set + "(" + p.SetCode + ")").Distinct<string>().Reverse<string>();

                    //Get available sets from web
                    var allsets = FormatSets.GetSetList();

                    //Begin adding sets to updatesetslistbox
                    Dispatcher.BeginInvoke((Action)delegate
                    {

                        if (allsets != null)
                        {
                            foreach (string current in allsets)
                            {
                                //If current set is already in local database, mark it with a different color
                                if (currentsets.Contains(current))
                                {
                                    availablesets.Items.Add(new ListBoxItem
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
                                    availablesets.Items.Add(new ListBoxItem
                                    {
                                        Content = current,
                                        IsSelected = false
                                    });
                                }
                            }
                        }

                        EnableButtons(true);
                    });
                }
            );

            tdrefresh.Start();
        }

        private void btn_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            List<string> updatesets = new List<string>();
            //Get all checked sets
            foreach (ListBoxItem item in availablesets.Items)
            {
                if (item.IsSelected)
                {
                    updatesets.Add(item.Content.ToString());
                }
            }
            if (updatesets.Count == 0) return;

            EnableButtons(false);

            tdupdate = new Thread(delegate()
                {
                    //Handle sets one by one
                    foreach (var set in updatesets)
                    {
                        Dispatcher.BeginInvoke((Action)delegate
                        {
                            //Set ProgressText
                            progresstext.Text = string.Format("Benginning {0}", set);
                        });

                        //Get Card Id List
                        List<Card> cards = FormatCard.GetIds(set.Remove(set.IndexOf("(")), set.Substring(set.IndexOf("(") + 1, set.IndexOf(")") - set.IndexOf("(") - 1));
                        if (cards == null)
                            continue;

                        Dispatcher.BeginInvoke((Action)delegate
                        {
                            //Set ProgressBar
                            progressbar.Value = 0;
                            progressbar.Maximum = cards.Count;
                        });

                        for (int i = 0; i < cards.Count; i++)
                        {
                            Dispatcher.BeginInvoke((Action)delegate
                            {
                                //Set ProgressText
                                progresstext.Text = string.Format("Formatting Card {0}", cards[i].ID);
                            });

                            //Get Card Properties
                            cards[i] = FormatCard.GetCard(cards[i], lang);
                            //Remove Null card
                            if (cards[i] == null)
                                cards.RemoveAt(i);

                            Dispatcher.BeginInvoke((Action)delegate
                            {
                                //Set ProgressBar
                                progressbar.Value = i;
                            });

                        }

                        //Save data to local database
                        new CONVERTER.ExportData().Export(cards, "Database.db");

                        Dispatcher.BeginInvoke((Action)delegate
                        {
                            //Mark the finished set as local
                            foreach (ListBoxItem item in availablesets.Items)
                            {
                                if (item.Content.ToString() == set)
                                {
                                    item.IsSelected = false;
                                    item.Foreground = new SolidColorBrush
                                    {
                                        Color = (Color)ColorConverter.ConvertFromString("#FF336B9B")
                                    };
                                    break;
                                }
                            }
                        });
                    }

                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        EnableButtons(true);
                    });
                });

            tdupdate.Start();
        }

        /// <summary>
        /// Disable/Enable all thread buttons
        /// </summary>
        /// <param name="p">Disable/Enable</param>
        private void EnableButtons(bool p)
        {

            btn_REFRESH.IsEnabled = p;
            btn_IMAGES_GATHERER.IsEnabled = p;
            btn_IMAGES_IPLAYMTG.IsEnabled = p;
            btn_IMAGES_MAGICCARDS.IsEnabled = p;
            btn_RESTORE.IsEnabled = p;
            btn_UPDATE.IsEnabled = p;
            progressbar.Visibility = p ? Visibility.Collapsed : Visibility.Visible;
            progresstext.Visibility = p ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btn_IMAGES_Click(object sender, RoutedEventArgs e)
        {
            Website site = Website.gatherer;

            switch ((sender as Button).Content.ToString())
            {
                case "GATHERER":
                    break;
                case "MAGICCARDS": site = Website.magiccards;
                    break;
                case "IPLAYMTG": site = Website.iplaymtg;
                    break;
                default:
                    break;
            }

            List<string> updatesets = new List<string>();
            //Get all checked sets
            foreach (ListBoxItem item in availablesets.Items)
            {
                if (item.IsSelected)
                {
                    updatesets.Add(item.Content.ToString().Remove(item.Content.ToString().IndexOf("(")));
                }
            }
            if (updatesets.Count == 0) return;

            EnableButtons(false);

            tddownload = new Thread(delegate()
            {
                var pcards = cards.Where(c => updatesets.Contains(c.Set)).ToList();

                Dispatcher.BeginInvoke((Action)delegate
                {
                    //Set ProgressBar
                    progressbar.Value = 0;
                    progressbar.Maximum = pcards.Count;
                });

                for (int i = 0; i < pcards.Count; i++)
                {
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        //Set ProgressText
                        progresstext.Text = string.Format("Downloading Card {0}", pcards[i].ID);
                    });

                    CONVERTER.Compressor.Zip(pcards[i]);

                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        //Set ProgressBar
                        progressbar.Value = i;
                    });
                }

                Dispatcher.BeginInvoke((Action)delegate
                {
                    EnableButtons(true);
                });

            });

            tddownload.Start();
        }

    }
}
