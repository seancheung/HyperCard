using MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CONVERTER
{
	public class LoadData
	{
		/// <summary>
		/// Load database from file
		/// </summary>
		/// <param name="xmlpath"></param>
		/// <returns></returns>
		public static List<Card> LoadDatabase(string xmlpath)
		{
			List<Card> list = new List<Card>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlpath);
			XmlElement xmlElement = xmlDocument["cards"];
			foreach (XmlNode xmlNode in xmlElement.ChildNodes)
			{
				try
				{
					Card card = new Card();
					card.ID = xmlNode.Attributes["id"].Value.ToString();
					card.zID = xmlNode.Attributes["zid"].Value.ToString();
					card.Var = xmlNode.Attributes["var"].Value.ToString();
					card.Name = xmlNode.Attributes["name"].Value.ToString();
					card.zName = xmlNode.Attributes["zname"].Value.ToString();
					card.Set = xmlNode.Attributes["set"].Value.ToString();
					card.SetCode = xmlNode.Attributes["setcode"].Value.ToString();
					card.Color = xmlNode.Attributes["color"].Value.ToString();
					card.ColorCode = xmlNode.Attributes["colorcode"].Value.ToString();
					card.Cost = xmlNode.Attributes["cost"].Value.ToString();
					card.CMC = xmlNode.Attributes["cmc"].Value.ToString();
					card.Type = xmlNode.Attributes["type"].Value.ToString();
					card.zType = xmlNode.Attributes["ztype"].Value.ToString();
					card.TypeCode = xmlNode.Attributes["typecode"].Value.ToString();
					card.Mana = xmlNode.Attributes["mana"].Value.ToString();
					card.Pow = xmlNode.Attributes["pow"].Value.ToString();
					card.Tgh = xmlNode.Attributes["tgh"].Value.ToString();
					card.Loyalty = xmlNode.Attributes["loyalty"].Value.ToString();
					card.Text = xmlNode.Attributes["text"].Value.ToString();
					card.zText = xmlNode.Attributes["ztext"].Value.ToString();
					card.Flavor = xmlNode.Attributes["flavor"].Value.ToString();
					card.zFlavor = xmlNode.Attributes["zflavor"].Value.ToString();
					card.Rarity = xmlNode.Attributes["rarity"].Value.ToString();
					card.RarityCode = xmlNode.Attributes["raritycode"].Value.ToString();
					card.Artist = xmlNode.Attributes["artist"].Value.ToString();
					card.Number = xmlNode.Attributes["number"].Value.ToString();
					card.Rulings = xmlNode.Attributes["rulings"].Value.ToString();
					card.Rating = xmlNode.Attributes["rating"].Value.ToString();
					card.Legality = xmlNode.Attributes["legality"].Value.ToString();
					list.Add(card);
				}
				catch (Exception ex)
				{
					LoggerError.Log(string.Format("Loading Data Error:\nID:{0}\nError:{1}", xmlNode.Attributes["id"].Value.ToString(), ex.Message));
					throw;
				}
			}
			return list;
		}

		private List<string> GetManaIcon(Card card)
		{
			char[] separator = new char[] { '{', '}' };
			string[] array = new string[] { "" };
			if (card.Cost != null)
			{
				array = card.Cost.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
			List<string> list = new List<string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string mana = array2[i];
				try
				{
					if (mana != "|")
					{
						list.Add(String.Format("/Resources/mana_{0}.png", mana.ToLower()));
					}
					else
					{
						list.Add("/Resources/mana_sep.png");
					}
				}
				catch (Exception ex)
				{
					LoggerError.Log(ex.Message);
					throw;
				}
			}
			return list;
		}

		//public List<Deck> Open(FileType filetype, FileStream fs, List<Card> database)
		//{

		//}

		private List<Deck> Open_VPT(FileStream fs, List<Card> database)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fs);
			List<Deck> list = new List<Deck>();
			string text = string.Empty;
			if (xmlDocument["deck"].ChildNodes[0].HasChildNodes)
			{
				XmlNodeList childNodes = xmlDocument["deck"].ChildNodes[0].ChildNodes;
				foreach (XmlNode xmlNode in childNodes)
				{
					if (!(xmlNode.Name == "group"))
					{
						try
						{
							Card card = new Card() { Name = xmlNode.Attributes["id"].Value.Replace("_", "|"), SetCode = xmlNode.FirstChild.Attributes["set"].Value };
							if (card != null)
							{
								list.Add(new Deck(card, int.Parse(xmlNode.FirstChild.Attributes["count"].Value)) { classify = 1 });
							}
							else
							{
								text += string.Format("{0} [{1}]\n", xmlNode.Attributes["id"].Value.Replace("_", "|"), xmlNode.FirstChild.Attributes["set"].Value);
							}
						}
						catch (Exception ex)
						{
							LoggerError.Log(ex.Message);
							throw;
						}
					}
				}
			}
			if (xmlDocument["deck"].ChildNodes[1].HasChildNodes)
			{
				XmlNodeList childNodes = xmlDocument["deck"].ChildNodes[1].ChildNodes;
				foreach (XmlNode xmlNode in childNodes)
				{
					try
					{
						Card card = new Card() { Name = xmlNode.Attributes["id"].Value.Replace("_", "|"), SetCode = xmlNode.FirstChild.Attributes["set"].Value };
						if (card != null)
						{
							list.Add(new Deck(card, int.Parse(xmlNode.FirstChild.Attributes["count"].Value)) { classify = 0 });
						}
						else
						{
							text += string.Format("{0} [{1}]\n", xmlNode.Attributes["id"].Value.Replace("_", "|"), xmlNode.FirstChild.Attributes["set"].Value);
						}
					}
					catch (Exception ex)
					{
						LoggerError.Log(ex.Message);
						throw;
					}
				}
			}
			if (text != string.Empty)
			{
				throw new Exception(text);
			}
			return list;
		}

		private List<Deck> Open_MWS(FileStream fs, List<Card> database)
		{

		}

		//private List<Deck> Open_MAGE(FileStream fs, List<Card> database)
		//{
		//}

		//private List<Deck> Open_MO(FileStream fs, List<Card> database)
		//{
		//}

		private static Card FindCard(List<Card> cards, Card card)
		{
			if (card.ID != null) card = cards.Find(c => c.ID == card.ID);
			else if (card.Name != null && card.SetCode != null) card = cards.Find(c => c.SetCode == card.SetCode && (c.Name == card.Name || c.zName == card.Name));
			else if (card.Name != null && card.Set != null) card = cards.Find(c => c.Set == card.Set && (c.Name == card.Name || c.zName == card.Name));
			else if (card.Number != null && card.Number != null) card = cards.Find(c => c.SetCode == card.SetCode && (c.Number == card.Number));
			else if (card.Number != null && card.Set != null) card = cards.Find(c => c.Set == card.Set && (c.Number == card.Number));
			else if (card.Name != null) card = cards.Find(c => c.Name == card.Name || c.zName == card.Name);
			else card = null;

			return card;
		}
	}
}
