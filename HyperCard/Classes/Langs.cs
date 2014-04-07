using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml;

namespace HyperCard
{
    public class Langs
    {
        public static List<KeyValuePair<string, string>> dict
        {
            get;
            set;
        }
        public static string MSG(string key)
        {
            string value = Langs.dict.Find((KeyValuePair<string, string> k) => k.Key == key).Value;
            return value ?? key;
        }
        public static void LoadDict(string path)
        {
            Langs.dict = new List<KeyValuePair<string, string>>();
            if (File.Exists(path))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlElement xmlElement = xmlDocument["lang"];
                foreach (XmlNode xmlNode in xmlElement.ChildNodes)
                {
                    Langs.dict.Add(new KeyValuePair<string, string>(xmlNode.Attributes["name"].Value.ToString(), xmlNode.Attributes["value"].Value.ToString()));
                }
            }
        }
        public static void SetLangs(Control basecontrol)
        {
            foreach (KeyValuePair<string, string> current in Langs.dict)
            {
                object obj = basecontrol.FindName(current.Key);
                if (obj != null)
                {
                    if (obj is Button || obj is RadioButton)
                    {
                        (obj as ContentControl).Content = current.Value;
                    }
                    else
                    {
                        if (obj is Expander)
                        {
                            (obj as Expander).Header = current.Value;
                        }
                        else
                        {
                            if (obj is TextBlock)
                            {
                                (obj as TextBlock).Text = current.Value;
                            }
                            else
                            {
                                if (obj is GridViewColumn)
                                {
                                    (obj as GridViewColumn).Header = current.Value;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
