using CONVERTER;
using FORMATTER;
using HyperCard.Properties;
using MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
		//Data downloading thread
		private Thread tddownload;
		//Data updating thread
		private Thread tdupdate;

		//Max thread amount for downloading
		private const int maxThread = 10;

		private static object _lock = new object();

		public MainWindow()
		{
			InitializeComponent();

			//Set language
			lang = Settings.Default.lang;

			//Match language setting with data binding
			if (lang != LANGUAGE.English)
			{
				MultiBinding mb = new MultiBinding();
				mb.Bindings.Add(new Binding("zName"));
				mb.Bindings.Add(new Binding("Name"));
				mb.Converter = new Styles.NameConverter();
				GridViewColumn_Name.DisplayMemberBinding = mb;
				GridViewColum_NameMain.DisplayMemberBinding = mb;
				GridViewColum_NameSide.DisplayMemberBinding = mb;
				//textfield.SetBinding(TextBlock.TextProperty, "zText");
			}


			deckmain = new DeckList();
			deckside = new DeckList();

			//Load database
			if (File.Exists(Settings.Default.dbname))
			{
				cards = LoadData.LoadDatabase(Settings.Default.dbname);
			}
			else
			{
				cards = new List<Card>();
			}

			tmpcards = new List<Card>(cards);

			resultboard.ItemsSource = tmpcards;
			mainboard.ItemsSource = deckmain;
			sideboard.ItemsSource = deckside;

			//Get all sets then add to set list
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

		}

		private void Window_DragMove(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}


		private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Binding current selected item to detailstab
			detailstab.DataContext = (sender as ListView).ItemsSource;
			var item = (sender as ListView).SelectedValue;
			//Start a new thread to extract/download images
			new Thread(delegate()
				{
					if (item != null)
					{
						Dispatcher.Invoke(delegate() { imgfront.Source = new BitmapImage(new Uri(@"\Resources\frame_back.jpg", UriKind.RelativeOrAbsolute)); imgback.Source = new BitmapImage(new Uri(@"\Resources\frame_back.jpg", UriKind.RelativeOrAbsolute)); });
						string img = Compressor.GetImagePath(item as Card, lang);
						if (File.Exists(img)) imgfront.Dispatcher.Invoke(delegate() { imgfront.Source = new BitmapImage(new Uri(img, UriKind.RelativeOrAbsolute)); });
						string imgb = Compressor.GetImagePath(item as Card, lang, false);
						if (File.Exists(img)) imgback.Dispatcher.Invoke(delegate() { imgback.Source = new BitmapImage(new Uri(imgb, UriKind.RelativeOrAbsolute)); });
					}
				}).Start();

			//Use Inlines instead of text property is to replace mana symbols with pics.
			textfield.Inlines.Clear();
			if (item != null)
			{
				var text = lang == LANGUAGE.English || string.IsNullOrWhiteSpace((item as Card).zText) ? (item as Card).Text : (item as Card).zText;
				var stext = ManaExtractor.Extract(text);
				foreach (var t in stext)
				{
					if (t.StartsWith("{"))
					{
						textfield.Inlines.Add(new Image() { Source = new BitmapImage(new Uri(ManaIcon.GetIconPath(t), UriKind.RelativeOrAbsolute)), Height = 14, Width = 14 });
					}
					else
					{
						textfield.Inlines.Add(t);
					}
				}
			}

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

					//Write set list as local
					if (allsets != null)
					{
						var data = string.Empty;
						foreach (var set in allsets)
						{
							data += set + "|";
						}
						File.CreateText("sets.hyd").Write(data);
					}

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

			//A new thread that handles the data updating process
			tdupdate = new Thread(delegate()
				{
					foreach (var set in updatesets)
					{
						Dispatcher.Invoke((Action)delegate
						{
							//Set ProgressText
							progresstext.Text = string.Format("Benginning {0}", set);
						});

						//Get Card Id List
						List<Card> cards = FormatCard.GetIds(set.Remove(set.IndexOf("(")), set.Substring(set.IndexOf("(") + 1, set.IndexOf(")") - set.IndexOf("(") - 1));
						if (cards == null)
							continue;

						var n = cards.Count;

						Dispatcher.Invoke((Action)delegate
						{
							//Set ProgressBar
							progressbar.Value = 0;
							progressbar.Maximum = n;
						});

						//Split the full card list into several parts
						var tps = new List<List<Card>>();
						for (int i = 0; i < maxThread - 1; i++)
						{
							tps.Add(cards.GetRange(n / maxThread * i, n / maxThread));
						}
						tps.Add(cards.GetRange(n / maxThread * (maxThread - 1), n / maxThread + n % maxThread));

						WaitCallback waitCallback = new WaitCallback(DownloadCards);
						WaitHandle[] waitHandles = new WaitHandle[maxThread];

						//Start a thread pool for updating
						for (int i = 0; i < maxThread; i++)
						{
							waitHandles[i] = new AutoResetEvent(false);
							ThreadPool.QueueUserWorkItem(waitCallback, new object[] { tps[i], waitHandles[i] });
						}

						//Wait for all downloading threads to finish
						WaitHandle.WaitAll(waitHandles);

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
		/// Method that downloads a list of cards
		/// </summary>
		/// <param name="obj">Parameters boxed in one object</param>
		private void DownloadCards(object obj)
		{
			object[] o = obj as object[];
			List<Card> cards = o[0] as List<Card>;
			AutoResetEvent waitHandle = (AutoResetEvent)o[1];

			for (int i = 0; i < cards.Count; i++)
			{
				Dispatcher.Invoke((Action)delegate { progresstext.Text = string.Format("Formatting Card {0}", cards[i].ID); });

				cards[i] = FormatCard.GetCard(cards[i], lang);
				if (cards[i] == null) cards.RemoveAt(i);

				Dispatcher.Invoke((Action)delegate { progressbar.Value++; });
			}

			lock (_lock)
			{
				//Save Data
				ExportData.Export(cards, Settings.Default.dbname);
			}

			//Set the current thread state as finished
			waitHandle.Set();
		}

		/// <summary>
		/// Disable/Enable all thread buttons
		/// </summary>
		/// <param name="state">Disable/Enable</param>
		private void EnableButtons(bool state)
		{

			btn_REFRESH.IsEnabled = state;
			btn_IMAGES_GATHERER.IsEnabled = state;
			btn_IMAGES_IPLAYMTG.IsEnabled = state;
			btn_IMAGES_MAGICCARDS.IsEnabled = state;
			btn_RESTORE.IsEnabled = state;
			btn_UPDATE.IsEnabled = state;
			progressbar.Visibility = state ? Visibility.Collapsed : Visibility.Visible;
			progresstext.Visibility = state ? Visibility.Collapsed : Visibility.Visible;
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

					Compressor.Zip(pcards[i]);

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

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void Max_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState == System.Windows.WindowState.Maximized)
				WindowState = System.Windows.WindowState.Normal;
			else
				WindowState = System.Windows.WindowState.Maximized;
		}

		private void Min_Click(object sender, RoutedEventArgs e)
		{
			WindowState = System.Windows.WindowState.Minimized;
		}

		private void availablesets_Loaded(object sender, RoutedEventArgs e)
		{
			if (File.Exists("sets.hyd"))
			{
				//Read local set list
				var data = File.ReadAllText("sets.hyd");
				var sets = data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

				EnableButtons(false);

				tdrefresh = new Thread(delegate()
				{
					//Get all sets form local database
					var currentsets = (
				from p in cards
				select p.Set + "(" + p.SetCode + ")").Distinct<string>().Reverse<string>();

					//Begin adding sets to updatesetslistbox
					Dispatcher.BeginInvoke((Action)delegate
					{

						if (allsets != null)
						{
							foreach (string set in sets)
							{
								//If current set is already in local database, mark it with a different color
								if (currentsets.Contains(set))
								{
									availablesets.Items.Add(new ListBoxItem
									{
										Content = set,
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
										Content = set,
										IsSelected = false
									});
								}
							}
						}

						EnableButtons(true);
					});
				});

				tdrefresh.Start();
			}
		}

		/// <summary>
		/// Update a card's database
		/// </summary>
		/// <param name="card">Card to update</param>
		private void UpdateSingleCard(Card card)
		{
			card = FormatCard.GetCard(card, lang);
			ExportData.Export(new List<Card>() { card }, Settings.Default.dbname);
		}

	}
}
