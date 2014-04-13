using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace HyperCard
{
    public class Configs
    {
        public static List<KeyValuePair<string, string>> settings
        {
            get;
            set;
        }
        public static bool Save(string lang, int defaultsave, int defaultopen)
        {
            bool result;
            if (!File.Exists("conf.hs"))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlElement xmlElement = xmlDocument.CreateElement("settings");
                    XmlAttribute node = xmlDocument.CreateAttribute("time");
                    xmlElement.Attributes.Append(node);
                    xmlDocument.AppendChild(xmlElement);
                    XmlElement newChild = xmlDocument.CreateElement("lang");
                    xmlElement.AppendChild(newChild);
                    XmlElement xmlElement2 = xmlDocument.CreateElement("fileformat");
                    XmlAttribute node2 = xmlDocument.CreateAttribute("defaultsave");
                    XmlAttribute node3 = xmlDocument.CreateAttribute("defaultopen");
                    xmlElement2.Attributes.Append(node2);
                    xmlElement2.Attributes.Append(node3);
                    xmlElement.AppendChild(xmlElement2);
                    XmlElement xmlElement3 = xmlDocument.CreateElement("mana");
                    List<XmlAttribute> list = new List<XmlAttribute>
					{
						xmlDocument.CreateAttribute("B"),
						xmlDocument.CreateAttribute("G"),
						xmlDocument.CreateAttribute("R"),
						xmlDocument.CreateAttribute("U"),
						xmlDocument.CreateAttribute("W"),
						xmlDocument.CreateAttribute("BG"),
						xmlDocument.CreateAttribute("BR"),
						xmlDocument.CreateAttribute("GU"),
						xmlDocument.CreateAttribute("GW"),
						xmlDocument.CreateAttribute("RG"),
						xmlDocument.CreateAttribute("RW"),
						xmlDocument.CreateAttribute("UB"),
						xmlDocument.CreateAttribute("UR"),
						xmlDocument.CreateAttribute("WB"),
						xmlDocument.CreateAttribute("WU")
					};
                    foreach (XmlAttribute current in list)
                    {
                        xmlElement3.Attributes.Append(current);
                    }
                    xmlElement.AppendChild(xmlElement3);
                    xmlDocument.Save("conf.hs");
                }
                catch (System.Exception ex)
                {
                    result = false;
                    return result;
                }
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("conf.hs");
                xmlDocument["settings"].Attributes["time"].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                xmlDocument["settings"].ChildNodes[0].InnerText = lang;
                xmlDocument["settings"].ChildNodes[1].Attributes["defaultsave"].Value = defaultsave.ToString();
                xmlDocument["settings"].ChildNodes[1].Attributes["defaultopen"].Value = defaultopen.ToString();
                xmlDocument.Save("conf.hs");
            }
            catch (System.Exception ex)
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        public static bool Save(string ManaCode, string id)
        {
            bool result;
            if (!File.Exists("conf.hs"))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlElement xmlElement = xmlDocument.CreateElement("settings");
                    XmlAttribute node = xmlDocument.CreateAttribute("time");
                    xmlElement.Attributes.Append(node);
                    xmlDocument.AppendChild(xmlElement);
                    XmlElement newChild = xmlDocument.CreateElement("lang");
                    xmlElement.AppendChild(newChild);
                    XmlElement xmlElement2 = xmlDocument.CreateElement("fileformat");
                    XmlAttribute node2 = xmlDocument.CreateAttribute("defaultsave");
                    XmlAttribute node3 = xmlDocument.CreateAttribute("defaultopen");
                    xmlElement2.Attributes.Append(node2);
                    xmlElement2.Attributes.Append(node3);
                    xmlElement.AppendChild(xmlElement2);
                    XmlElement xmlElement3 = xmlDocument.CreateElement("mana");
                    List<XmlAttribute> list = new List<XmlAttribute>
					{
						xmlDocument.CreateAttribute("B"),
						xmlDocument.CreateAttribute("G"),
						xmlDocument.CreateAttribute("R"),
						xmlDocument.CreateAttribute("U"),
						xmlDocument.CreateAttribute("W"),
						xmlDocument.CreateAttribute("BG"),
						xmlDocument.CreateAttribute("BR"),
						xmlDocument.CreateAttribute("GU"),
						xmlDocument.CreateAttribute("GW"),
						xmlDocument.CreateAttribute("RG"),
						xmlDocument.CreateAttribute("RW"),
						xmlDocument.CreateAttribute("UB"),
						xmlDocument.CreateAttribute("UR"),
						xmlDocument.CreateAttribute("WB"),
						xmlDocument.CreateAttribute("WU")
					};
                    foreach (XmlAttribute current in list)
                    {
                        xmlElement3.Attributes.Append(current);
                    }
                    xmlElement.AppendChild(xmlElement3);
                    xmlDocument.Save("conf.hs");
                }
                catch (System.Exception ex)
                {
                    result = false;
                    return result;
                }
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("conf.hs");
                xmlDocument["settings"].Attributes["time"].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                xmlDocument["settings"].ChildNodes[2].Attributes[ManaCode].Value = id;
                xmlDocument.Save("conf.hs");
            }
            catch (System.Exception ex)
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        public static void Load()
        {
            if (!File.Exists("conf.hs"))
            {
                Configs.Save("English", 1, 1);
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("conf.hs");
            Configs.settings = new List<KeyValuePair<string, string>>();
            Configs.settings.Add(new KeyValuePair<string, string>("lang", xmlDocument["settings"].ChildNodes[0].InnerText));
            Configs.settings.Add(new KeyValuePair<string, string>("defaultsave", xmlDocument["settings"].ChildNodes[1].Attributes["defaultsave"].Value));
            Configs.settings.Add(new KeyValuePair<string, string>("defaultopen", xmlDocument["settings"].ChildNodes[1].Attributes["defaultopen"].Value));
            Configs.settings.Add(new KeyValuePair<string, string>("B", xmlDocument["settings"].ChildNodes[2].Attributes["B"].Value));
            foreach (XmlAttribute xmlAttribute in xmlDocument["settings"].ChildNodes[2].Attributes)
            {
                Configs.settings.Add(new KeyValuePair<string, string>(xmlAttribute.Name, xmlAttribute.Value));
            }
        }
    }
}
