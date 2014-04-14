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
    }
}
