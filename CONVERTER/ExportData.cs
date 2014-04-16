using MODEL;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CONVERTER
{
    public class ExportData
    {
        /// <summary>
        /// Export the database grabbed from web
        /// </summary>
        /// <param name="cards">Cards that are fully filled</param>
        /// <param name="filepath">File saving path</param>
        public void Export(List<Card> cards, string filepath)
        {
            if (!File.Exists(filepath))
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                xmlDocument.AppendChild(newChild);
                XmlElement newChild2 = xmlDocument.CreateElement("cards");
                xmlDocument.AppendChild(newChild2);

                try
                {
                    xmlDocument.Save(filepath);
                }
                catch (IOException ex)
                {
                    LoggerError.Log(ex.Message);
                }
            }
            XmlDocument xmlDocument2 = new XmlDocument();

            try
            {
                xmlDocument2.Load(filepath);
            }
            catch (IOException ex)
            {
                LoggerError.Log(ex.Message);
            }

            XmlElement xmlElement = xmlDocument2["cards"];
            foreach (Card current in cards)
            {
                XmlNode xmlNode = xmlDocument2.CreateElement("card");
                XmlNode xmlNode2 = xmlDocument2.SelectSingleNode(string.Format("/cards/card[@id='{0}']", current.ID));
                if (xmlNode2 == null)
                {
                    xmlElement.AppendChild(xmlNode);
                }
                else
                {
                    xmlElement.ReplaceChild(xmlNode, xmlNode2);
                }
                XmlAttribute xmlAttribute = xmlDocument2.CreateAttribute("id");
                xmlAttribute.Value = current.ID;
                xmlNode.Attributes.Append(xmlAttribute);
                XmlAttribute xmlAttribute2 = xmlDocument2.CreateAttribute("zid");
                xmlAttribute2.Value = current.zID;
                xmlNode.Attributes.Append(xmlAttribute2);
                XmlAttribute xmlAttribute3 = xmlDocument2.CreateAttribute("var");
                xmlAttribute3.Value = current.Var;
                xmlNode.Attributes.Append(xmlAttribute3);
                XmlAttribute xmlAttribute4 = xmlDocument2.CreateAttribute("name");
                xmlAttribute4.Value = current.Name;
                xmlNode.Attributes.Append(xmlAttribute4);
                XmlAttribute xmlAttribute5 = xmlDocument2.CreateAttribute("zname");
                xmlAttribute5.Value = current.zName;
                xmlNode.Attributes.Append(xmlAttribute5);
                XmlAttribute xmlAttribute6 = xmlDocument2.CreateAttribute("set");
                xmlAttribute6.Value = current.Set;
                xmlNode.Attributes.Append(xmlAttribute6);
                XmlAttribute xmlAttribute7 = xmlDocument2.CreateAttribute("setcode");
                xmlAttribute7.Value = current.SetCode;
                xmlNode.Attributes.Append(xmlAttribute7);
                XmlAttribute xmlAttribute8 = xmlDocument2.CreateAttribute("color");
                xmlAttribute8.Value = current.Color;
                xmlNode.Attributes.Append(xmlAttribute8);
                XmlAttribute xmlAttribute9 = xmlDocument2.CreateAttribute("colorcode");
                xmlAttribute9.Value = current.ColorCode;
                xmlNode.Attributes.Append(xmlAttribute9);
                XmlAttribute xmlAttribute10 = xmlDocument2.CreateAttribute("cost");
                xmlAttribute10.Value = current.Cost;
                xmlNode.Attributes.Append(xmlAttribute10);
                XmlAttribute xmlAttribute11 = xmlDocument2.CreateAttribute("cmc");
                xmlAttribute11.Value = current.CMC;
                xmlNode.Attributes.Append(xmlAttribute11);
                XmlAttribute xmlAttribute12 = xmlDocument2.CreateAttribute("type");
                xmlAttribute12.Value = current.Type;
                xmlNode.Attributes.Append(xmlAttribute12);
                XmlAttribute xmlAttribute13 = xmlDocument2.CreateAttribute("ztype");
                xmlAttribute13.Value = current.zType;
                xmlNode.Attributes.Append(xmlAttribute13);
                XmlAttribute xmlAttribute14 = xmlDocument2.CreateAttribute("typecode");
                xmlAttribute14.Value = current.TypeCode;
                xmlNode.Attributes.Append(xmlAttribute14);
                XmlAttribute xmlAttribute15 = xmlDocument2.CreateAttribute("mana");
                xmlAttribute15.Value = current.Mana;
                xmlNode.Attributes.Append(xmlAttribute15);
                XmlAttribute xmlAttribute16 = xmlDocument2.CreateAttribute("pow");
                xmlAttribute16.Value = current.Pow;
                xmlNode.Attributes.Append(xmlAttribute16);
                XmlAttribute xmlAttribute17 = xmlDocument2.CreateAttribute("tgh");
                xmlAttribute17.Value = current.Tgh;
                xmlNode.Attributes.Append(xmlAttribute17);
                XmlAttribute xmlAttribute18 = xmlDocument2.CreateAttribute("loyalty");
                xmlAttribute18.Value = current.Loyalty;
                xmlNode.Attributes.Append(xmlAttribute18);
                XmlAttribute xmlAttribute19 = xmlDocument2.CreateAttribute("text");
                xmlAttribute19.Value = current.Text;
                xmlNode.Attributes.Append(xmlAttribute19);
                XmlAttribute xmlAttribute20 = xmlDocument2.CreateAttribute("ztext");
                xmlAttribute20.Value = current.zText;
                xmlNode.Attributes.Append(xmlAttribute20);
                XmlAttribute xmlAttribute21 = xmlDocument2.CreateAttribute("flavor");
                xmlAttribute21.Value = current.Flavor;
                xmlNode.Attributes.Append(xmlAttribute21);
                XmlAttribute xmlAttribute22 = xmlDocument2.CreateAttribute("zflavor");
                xmlAttribute22.Value = current.zFlavor;
                xmlNode.Attributes.Append(xmlAttribute22);
                XmlAttribute xmlAttribute23 = xmlDocument2.CreateAttribute("rarity");
                xmlAttribute23.Value = current.Rarity;
                xmlNode.Attributes.Append(xmlAttribute23);
                XmlAttribute xmlAttribute24 = xmlDocument2.CreateAttribute("raritycode");
                xmlAttribute24.Value = current.RarityCode;
                xmlNode.Attributes.Append(xmlAttribute24);
                XmlAttribute xmlAttribute25 = xmlDocument2.CreateAttribute("artist");
                xmlAttribute25.Value = current.Artist;
                xmlNode.Attributes.Append(xmlAttribute25);
                XmlAttribute xmlAttribute26 = xmlDocument2.CreateAttribute("number");
                xmlAttribute26.Value = current.Number;
                xmlNode.Attributes.Append(xmlAttribute26);
                XmlAttribute xmlAttribute27 = xmlDocument2.CreateAttribute("rulings");
                xmlAttribute27.Value = current.Rulings;
                xmlNode.Attributes.Append(xmlAttribute27);
                XmlAttribute xmlAttribute28 = xmlDocument2.CreateAttribute("legality");
                xmlAttribute28.Value = current.Legality;
                xmlNode.Attributes.Append(xmlAttribute28);
                XmlAttribute xmlAttribute29 = xmlDocument2.CreateAttribute("rating");
                xmlAttribute29.Value = current.Rating;
                xmlNode.Attributes.Append(xmlAttribute29);
            }

            try
            {
                xmlDocument2.Save(filepath);
            }
            catch (IOException ex)
            {
                LoggerError.Log(ex.Message);
            }
        }

        /// <summary>
        /// Export the game format database
        /// </summary>
        /// <param name="format">Game format</param>
        /// <param name="filepath">File saving path</param>
        public void Export(List<Format> format, string filepath)
        {
            if (!File.Exists(filepath))
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                xmlDocument.AppendChild(newChild);
                XmlElement newChild2 = xmlDocument.CreateElement("legality");
                xmlDocument.AppendChild(newChild2);
                xmlDocument.Save(filepath);
            }
            XmlDocument xmlDocument2 = new XmlDocument();
            xmlDocument2.Load(filepath);
            XmlElement xmlElement = xmlDocument2["legality"];
            foreach (Format current in format)
            {
                if (current != null)
                {
                    XmlNode xmlNode = xmlDocument2.CreateElement("format");
                    XmlNode xmlNode2 = xmlDocument2.SelectSingleNode(string.Format("/legality/format[@name='{0}']", current.FormatName.ToString()));
                    if (xmlNode2 == null)
                    {
                        xmlElement.AppendChild(xmlNode);
                    }
                    else
                    {
                        xmlElement.ReplaceChild(xmlNode, xmlNode2);
                    }
                    XmlAttribute xmlAttribute = xmlDocument2.CreateAttribute("name");
                    xmlAttribute.Value = current.FormatName.ToString();
                    xmlNode.Attributes.Append(xmlAttribute);
                    XmlElement xmlElement2 = xmlDocument2.CreateElement("sets");
                    xmlNode.AppendChild(xmlElement2);
                    XmlElement xmlElement3 = xmlDocument2.CreateElement("cards");
                    xmlNode.AppendChild(xmlElement3);
                    foreach (string current2 in current.LegalSets)
                    {
                        XmlElement xmlElement4 = xmlDocument2.CreateElement("set");
                        xmlElement2.AppendChild(xmlElement4);
                        XmlAttribute xmlAttribute2 = xmlDocument2.CreateAttribute("name");
                        xmlAttribute2.Value = current2;
                        xmlElement4.Attributes.Append(xmlAttribute2);
                    }
                    foreach (string current2 in current.BannedCards)
                    {
                        XmlElement xmlElement5 = xmlDocument2.CreateElement("card");
                        xmlElement3.AppendChild(xmlElement5);
                        XmlAttribute xmlAttribute3 = xmlDocument2.CreateAttribute("name");
                        xmlAttribute3.Value = current2;
                        xmlElement5.Attributes.Append(xmlAttribute3);
                    }
                }
            }
            xmlDocument2.Save(filepath);
        }

        public void ExportAs(FileType filetype, List<Deck> decks, FileStream fs)
        {

            switch (filetype)
            {
                case FileType.Virtual_Play_Table: ExportAsVPT(decks, fs);
                    break;
                case FileType.Magic_Workstation: ExportAsMWS(decks, fs);
                    break;
                case FileType.Mage: ExportAsMAGE(decks, fs);
                    break;
                case FileType.Magic_Online: ExportAsMO(decks, fs);
                    break;
                default:
                    break;
            }
        }

        private void ExportAsVPT(List<Deck> decks, FileStream fs)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xmlDocument.AppendChild(newChild);
            XmlElement xmlElement = xmlDocument.CreateElement("deck");
            XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("game");
            xmlAttribute.Value = "mtg";
            xmlElement.Attributes.Append(xmlAttribute);
            XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("mode");
            xmlAttribute2.Value = "Constructed";
            xmlElement.Attributes.Append(xmlAttribute2);
            XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("format");
            xmlAttribute3.Value = string.Empty;
            xmlElement.Attributes.Append(xmlAttribute3);
            XmlAttribute xmlAttribute4 = xmlDocument.CreateAttribute("name");
            xmlAttribute4.Value = "";
            xmlElement.Attributes.Append(xmlAttribute4);
            XmlElement xmlElement2 = xmlDocument.CreateElement("section");
            XmlAttribute xmlAttribute5 = xmlDocument.CreateAttribute("id");
            xmlAttribute5.Value = "main";
            xmlElement2.Attributes.Append(xmlAttribute5);
            xmlElement.AppendChild(xmlElement2);
            XmlElement xmlElement3 = xmlDocument.CreateElement("section");
            XmlAttribute xmlAttribute6 = xmlDocument.CreateAttribute("id");
            xmlAttribute6.Value = "sideboard";
            xmlElement3.Attributes.Append(xmlAttribute6);
            xmlElement.AppendChild(xmlElement3);
            if (decks.Count != 0)
            {
                foreach (Deck current in decks)
                {
                    string text = current.Name;
                    if (current.ID.Contains("|"))
                    {
                        text = current.Name.Remove(current.Name.IndexOf("|"));
                    }
                    if (current.classify == 1)
                    {
                        XmlElement xmlElement4 = xmlDocument.CreateElement("item");
                        XmlAttribute xmlAttribute7 = xmlDocument.CreateAttribute("id");
                        xmlAttribute7.Value = text.Replace("|", "_");
                        xmlElement4.Attributes.Append(xmlAttribute7);
                        XmlElement xmlElement5 = xmlDocument.CreateElement("card");
                        XmlAttribute xmlAttribute8 = xmlDocument.CreateAttribute("lang");
                        xmlAttribute8.Value = "ENG";
                        xmlElement5.Attributes.Append(xmlAttribute8);
                        XmlAttribute xmlAttribute9 = xmlDocument.CreateAttribute("set");
                        xmlAttribute9.Value = current.SetCode;
                        xmlElement5.Attributes.Append(xmlAttribute9);
                        XmlAttribute xmlAttribute10 = xmlDocument.CreateAttribute("count");
                        xmlAttribute10.Value = current.num.ToString();
                        xmlElement5.Attributes.Append(xmlAttribute10);
                        xmlElement4.AppendChild(xmlElement5);
                        xmlElement2.AppendChild(xmlElement4);
                    }
                    else
                    {
                        XmlElement xmlElement4 = xmlDocument.CreateElement("item");
                        XmlAttribute xmlAttribute7 = xmlDocument.CreateAttribute("id");
                        xmlAttribute7.Value = text.Replace("|", "_");
                        xmlElement4.Attributes.Append(xmlAttribute7);
                        XmlElement xmlElement5 = xmlDocument.CreateElement("card");
                        XmlAttribute xmlAttribute8 = xmlDocument.CreateAttribute("lang");
                        xmlAttribute8.Value = "ENG";
                        xmlElement5.Attributes.Append(xmlAttribute8);
                        XmlAttribute xmlAttribute9 = xmlDocument.CreateAttribute("set");
                        xmlAttribute9.Value = current.SetCode;
                        xmlElement5.Attributes.Append(xmlAttribute9);
                        XmlAttribute xmlAttribute10 = xmlDocument.CreateAttribute("count");
                        xmlAttribute10.Value = current.num.ToString();
                        xmlElement5.Attributes.Append(xmlAttribute10);
                        xmlElement4.AppendChild(xmlElement5);
                        xmlElement3.AppendChild(xmlElement4);
                    }
                }
            }
            xmlDocument.AppendChild(xmlElement);
            xmlDocument.Save(fs);
        }

        private void ExportAsMWS(List<Deck> decks, FileStream fs)
        {
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine("// Comments\n");
            streamWriter.WriteLine("\r\n// Lands\n");
            foreach (Deck current in decks)
            {
                string text = current.Name;
                if (current.ID.Contains("|"))
                {
                    text = current.Name.Remove(current.Name.IndexOf("|"));
                }
                if (current.TypeCode.Contains("L") && current.classify == 1)
                {
                    if (current.TypeCode.Contains("B"))
                    {
                        streamWriter.WriteLine(string.Format("{0} [{1}] {2} (1)", current.num.ToString(), current.SetCode, text.Replace("|", "_")));
                    }
                    else
                    {
                        streamWriter.WriteLine(string.Format("{0} [{1}] {2}", current.num.ToString(), current.SetCode, text.Replace("|", "_")));
                    }
                }
            }
            streamWriter.WriteLine("\r\n// Spells\n");
            foreach (Deck current in decks)
            {
                string text = current.Name;
                if (current.ID.Contains("|"))
                {
                    text = current.Name.Remove(current.Name.IndexOf("|"));
                }
                if (!current.TypeCode.Contains("L") && current.classify == 1)
                {
                    streamWriter.WriteLine(string.Format("{0} [{1}] {2}", current.num.ToString(), current.SetCode, text.Replace("|", "_")));
                }
            }
            streamWriter.WriteLine("\r\n// Sideboard\n");
            foreach (Deck current in decks)
            {
                string text = current.Name;
                if (current.ID.Contains("|"))
                {
                    text = current.Name.Remove(current.Name.IndexOf("|"));
                }
                if (current.classify == 0)
                {
                    streamWriter.WriteLine(string.Format("SB: {0} [{1}] {2}", current.num.ToString(), current.SetCode, text.Replace("|", "_")));
                }
            }
            streamWriter.Close();
        }

        private void ExportAsMAGE(List<Deck> decks, FileStream fs)
        {
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine("NAME:");
            foreach (Deck current in decks)
            {
                string text = current.Name;
                string text2 = current.Number;
                if (current.ID.Contains("|"))
                {
                    text = current.Name.Remove(current.Name.IndexOf("|"));
                    text2 = current.Number.Remove(current.Number.IndexOf("|"));
                }
                if (current.classify == 1)
                {
                    streamWriter.WriteLine(string.Format("{0} [{1}:{2}] {3}", new object[]
			{
				current.num.ToString(),
				current.SetCode,
				text2,
				text.Replace("|", "_")
			}));
                }
                else
                {
                    streamWriter.WriteLine(string.Format("SB: {0} [{1}:{2}] {3}", new object[]
			{
				current.num.ToString(),
				current.SetCode,
				text2,
				text.Replace("|", "_")
			}));
                }
            }
            streamWriter.Close();
        }

        private void ExportAsMO(List<Deck> decks, FileStream fs)
        {
            StreamWriter streamWriter = new StreamWriter(fs);
            foreach (Deck current in decks)
            {
                if (current.classify == 1)
                {
                    string text = current.Name;
                    if (current.ID.Contains("|"))
                    {
                        text = current.Name.Remove(current.Name.IndexOf("|"));
                    }
                    streamWriter.WriteLine(string.Format("{0} {1}", current.num.ToString(), text.Replace("|", "_")));
                }
            }
            streamWriter.WriteLine("Sideboard");
            foreach (Deck current in decks)
            {
                if (current.classify == 0)
                {
                    string text = current.Name;
                    if (current.ID.Contains("|"))
                    {
                        text = current.Name.Remove(current.Name.IndexOf("|"));
                    }
                    streamWriter.WriteLine(string.Format("{0} {1}", current.num.ToString(), text.Replace("|", "_")));
                }
            }
            streamWriter.Close();
        }
    }
}
