using MODEL;
using System;
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
        public static void Export(List<Card> cards, string filepath)
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
                catch (Exception ex)
                {
                    LoggerError.Log("Initializing Database Error: " + ex.Message);
                }
            }
            XmlDocument xmlDocument2 = new XmlDocument();

            try
            {
                xmlDocument2.Load(filepath);
            }
            catch (Exception ex)
            {
                LoggerError.Log("Loading Database Error: " + ex.Message);
            }

            XmlElement xmlElement = xmlDocument2["cards"];
            foreach (Card current in cards)
            {
                try
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
                catch (Exception ex)
                {

                    LoggerError.Log("Saving Card Error:\n" + current.ID + "\nError: " + ex.Message);
                }
            }

            try
            {
                xmlDocument2.Save(filepath);
            }
            catch (Exception ex)
            {
                LoggerError.Log("Saving Database Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Export the game format database
        /// </summary>
        /// <param name="format">Game format</param>
        /// <param name="filepath">File saving path</param>
        public static void Export(List<Format> format, string filepath)
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
                try
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
                catch (Exception ex)
                {
                    LoggerError.Log("Saving Format Error:\n" + current.FormatName.ToString() + "\nError: " + ex.Message);
                }
            }
            xmlDocument2.Save(filepath);
        }

        public static void Export(List<Card> cards, string filepath, FileType ftype, LANGUAGE lang)
        {
            switch (ftype)
            {
                case FileType.Virtual_Play_Table:
                    ExportAsVPT(cards, filepath, lang);
                    break;
                case FileType.Magic_Workstation:
                    break;
                case FileType.Mage:
                    break;
                case FileType.Magic_Online:
                    break;
                default:
                    break;
            }
        }

        private static void ExportAsVPT(List<Card> cards, string filepath, LANGUAGE lang)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                xmlDocument.AppendChild(newChild);
                XmlElement xeitems = xmlDocument.CreateElement("items");
                xmlDocument.AppendChild(xeitems);

                XmlAttribute xagame = xmlDocument.CreateAttribute("game");
                xagame.Value = "mtg";
                xeitems.Attributes.Append(xagame);
                XmlAttribute xaset = xmlDocument.CreateAttribute("set");
                xaset.Value = cards[0].SetCode;
                xeitems.Attributes.Append(xaset);
                XmlAttribute xalang = xmlDocument.CreateAttribute("lang");
                if (lang == LANGUAGE.Chinese_Simplified) xalang.Value = "CS";
                else xalang.Value = "EN";
                xeitems.Attributes.Append(xalang);
                XmlAttribute xasetname = xmlDocument.CreateAttribute("setname");
                xasetname.Value = cards[0].Set;
                xeitems.Attributes.Append(xasetname);
                XmlAttribute xaimagepath = xmlDocument.CreateAttribute("imagepath");
                xaimagepath.Value = cards[0].Set;
                xeitems.Attributes.Append(xaimagepath);
                XmlAttribute xadate = xmlDocument.CreateAttribute("date");
                xadate.Value = System.DateTime.Now.ToShortDateString();
                xeitems.Attributes.Append(xadate);
                XmlAttribute xaborder = xmlDocument.CreateAttribute("border");
                xaborder.Value = "Black";
                xeitems.Attributes.Append(xaborder);
                XmlAttribute xacopyright = xmlDocument.CreateAttribute("copyright");
                xacopyright.Value = "™ &amp; © 2014 Wizards of the Coast";
                xeitems.Attributes.Append(xacopyright);

                foreach (var card in cards)
                {
                    XmlNode xmlNode = xmlDocument.CreateElement("item");
                    xeitems.AppendChild(xmlNode);

                    XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("id");
                    xmlAttribute.Value = card.Name;
                    xmlNode.Attributes.Append(xmlAttribute);

                    XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("name");
                    if (lang == LANGUAGE.Chinese_Simplified) xmlAttribute2.Value = card.zName;
                    else xmlAttribute2.Value = card.Name;
                    xmlNode.Attributes.Append(xmlAttribute2);

                    if (card.TypeCode.Contains("L"))
                    {
                        foreach (var ver in card.Vars)
                        {
                            if (!string.IsNullOrWhiteSpace(ver))
                            {
                                XmlAttribute xmlAttribute16 = xmlDocument.CreateAttribute("ver");
                                xmlAttribute16.Value = ver[0].ToString();
                                xmlNode.Attributes.Append(xmlAttribute16);
                            }
                        }
                    }

                    XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("color");
                    xmlAttribute3.Value = card.Color;
                    xmlNode.Attributes.Append(xmlAttribute3);

                    XmlAttribute xmlAttribute4 = xmlDocument.CreateAttribute("cost");
                    xmlAttribute4.Value = card.Cost;
                    xmlNode.Attributes.Append(xmlAttribute4);

                    XmlAttribute xmlAttribute5 = xmlDocument.CreateAttribute("cmc");
                    xmlAttribute5.Value = card.CMC;
                    xmlNode.Attributes.Append(xmlAttribute5);

                    XmlAttribute xmlAttribute6 = xmlDocument.CreateAttribute("type");
                    if (lang == LANGUAGE.Chinese_Simplified) xmlAttribute6.Value = card.zType;
                    else xmlAttribute6.Value = card.Type;
                    xmlNode.Attributes.Append(xmlAttribute6);

                    XmlAttribute xmlAttribute7 = xmlDocument.CreateAttribute("types");
                    xmlAttribute7.Value = card.Type.Replace("—", string.Empty);
                    xmlNode.Attributes.Append(xmlAttribute7);

                    if (card.TypeCode.Contains("C"))
                    {
                        XmlAttribute xmlAttribute8 = xmlDocument.CreateAttribute("power");
                        xmlAttribute8.Value = card.Pow;
                        xmlNode.Attributes.Append(xmlAttribute8);

                        XmlAttribute xmlAttribute9 = xmlDocument.CreateAttribute("toughness");
                        xmlAttribute9.Value = card.Tgh;
                        xmlNode.Attributes.Append(xmlAttribute9);
                    }

                    if (card.TypeCode.Contains("P"))
                    {
                        XmlAttribute xmlAttribute17 = xmlDocument.CreateAttribute("loyalty");
                        xmlAttribute17.Value = card.Loyalty;
                        xmlNode.Attributes.Append(xmlAttribute17);
                    }
                    XmlAttribute xmlAttribute10 = xmlDocument.CreateAttribute("text");
                    if (lang == LANGUAGE.Chinese_Simplified) xmlAttribute10.Value = card.zText;
                    else xmlAttribute10.Value = card.Text;
                    xmlNode.Attributes.Append(xmlAttribute10);

                    XmlAttribute xmlAttribute11 = xmlDocument.CreateAttribute("flavor");
                    if (lang == LANGUAGE.Chinese_Simplified) xmlAttribute11.Value = card.zFlavor;
                    else xmlAttribute11.Value = card.Flavor;
                    xmlNode.Attributes.Append(xmlAttribute11);

                    XmlAttribute xmlAttribute12 = xmlDocument.CreateAttribute("artist");
                    xmlAttribute12.Value = card.Artist;
                    xmlNode.Attributes.Append(xmlAttribute12);

                    XmlAttribute xmlAttribute13 = xmlDocument.CreateAttribute("number");
                    xmlAttribute13.Value = string.Format("{0}/{1}", card.Number, cards.Count.ToString());
                    xmlNode.Attributes.Append(xmlAttribute13);

                    XmlAttribute xmlAttribute14 = xmlDocument.CreateAttribute("rarity");
                    xmlAttribute14.Value = card.Rarity;
                    xmlNode.Attributes.Append(xmlAttribute14);

                    XmlAttribute xmlAttribute15 = xmlDocument.CreateAttribute("foil");
                    xmlAttribute15.Value = "false|true";
                    xmlNode.Attributes.Append(xmlAttribute15);
                }

                xmlDocument.Save(filepath);
            }
            catch (Exception ex)
            {
                LoggerError.Log("Exporting Database As VPT Error: " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// Export as other supported filetype
        /// </summary>
        /// <param name="filetype">output filetype</param>
        /// <param name="decks">decks to save</param>
        /// <param name="fs">Filestream to save to</param>
        public void SaveAs(FileType filetype, List<Deck> decks, FileStream fs)
        {

            switch (filetype)
            {
                case FileType.Virtual_Play_Table: SaveAsVPT(decks, fs);
                    break;
                case FileType.Magic_Workstation: SaveAsMWS(decks, fs);
                    break;
                case FileType.Mage: SaveAsMAGE(decks, fs);
                    break;
                case FileType.Magic_Online: SaveAsMO(decks, fs);
                    break;
                default:
                    break;
            }
        }

        private void SaveAsVPT(List<Deck> decks, FileStream fs)
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
                    try
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
                    catch (Exception ex)
                    {
                        LoggerError.Log("Export As VPT Error:\n" + current.ID + "\nError: " + ex.Message);
                    }
                }
            }
            xmlDocument.AppendChild(xmlElement);
            xmlDocument.Save(fs);
        }

        private void SaveAsMWS(List<Deck> decks, FileStream fs)
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

        private void SaveAsMAGE(List<Deck> decks, FileStream fs)
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

        private void SaveAsMO(List<Deck> decks, FileStream fs)
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
