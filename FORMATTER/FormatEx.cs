using System;
using System.Linq;
using MODEL;
using System.Collections.Generic;

namespace FORMATTER
{
    internal class FormatEx
    {
        private FormatEx() { }

        /// <summary>
        /// Format foreign text
        /// </summary>
        /// <param name="card">Card to format</param>
        /// <returns>A card with foreign text formatted</returns>
        private static Card FormatzText(Card card)
        {
            string text = "";
            card.Cost = card.Cost.Replace("{", string.Empty).Replace("}", string.Empty);
            for (int i = 0; i < card.Cost.Count<char>(); i++)
            {
                if (card.Cost[i] == '|' || card.Cost[i] == '(' || card.Cost[i] == ')')
                {
                    text += card.Cost[i];
                }
                else
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						"{",
						card.Cost[i],
						"}"
					});
                }
            }
            while (text.Contains("("))
            {
                int num = text.IndexOf("(");
                int length = text.IndexOf(")") - num + 1;
                string text2 = text.Substring(num, length);
                string newValue = text2.Replace("{", string.Empty).Replace("}", string.Empty).Replace("(", "{").Replace(")", "}").Replace("/", string.Empty);
                text = text.Replace(text2, newValue);
            }
            card.Cost = text;
            Card result;
            if (!card.zText.Contains("{") || !card.Text.Contains("{"))
            {
                result = card;
            }
            else
            {
                card.zText = card.zText.Replace("{Tap}", "{T}").Replace("{White}", "{W}").Replace("{Blue}", "{U}").Replace("{Black}", "{B}").Replace("{Red}", "{R}").Replace("{Green}", "{G}");
                card.Text = card.Text.Replace("{Tap}", "{T}").Replace("{White}", "{W}").Replace("{Blue}", "{U}").Replace("{Black}", "{B}").Replace("{Red}", "{R}").Replace("{Green}", "{G}");
                result = card;
            }
            return result;
        }

        /// <summary>
        /// Format Additional properties
        /// </summary>
        /// <param name="card">Card to format</param>
        /// <returns>A card with Additional properties formatted</returns>
        private static Card FormatAdition(Card card)
        {
            if (card.Cost.Contains("W"))
            {
                card.Color += "White ";
                card.ColorCode += "W";
            }
            if (card.Cost.Contains("U"))
            {
                card.Color += "Blue ";
                card.ColorCode += "U";
            }
            if (card.Cost.Contains("B"))
            {
                card.Color += "Black ";
                card.ColorCode += "B";
            }
            if (card.Cost.Contains("R"))
            {
                card.Color += "Red ";
                card.ColorCode += "R";
            }
            if (card.Cost.Contains("G"))
            {
                card.Color += "Green ";
                card.ColorCode += "G";
            }
            if (card.Color == null)
            {
                card.Color += "Colorless ";
                card.ColorCode += "C";
            }
            card.Color = card.Color.Trim();
            if (card.isdoubleface)
            {
                card.Color = String.Format("{0}|{1}", card.Color, card.bcolor);
            }
            card.Color = card.Color.Trim();
            if (card.Type.Contains("Legendary"))
            {
                card.TypeCode += "U";
            }
            if (card.Type.Contains("Land"))
            {
                if (card.Type.Contains("Basic"))
                {
                    if (card.Type.Contains("Plains"))
                    {
                        card.TypeCode += "BL1";
                        card.Mana = "W";
                    }
                    if (card.Type.Contains("Island"))
                    {
                        card.TypeCode += "BL2";
                        card.Mana = "U";
                    }
                    if (card.Type.Contains("Swamp"))
                    {
                        card.TypeCode += "BL3";
                        card.Mana = "B";
                    }
                    if (card.Type.Contains("Mountain"))
                    {
                        card.TypeCode += "BL4";
                        card.Mana = "R";
                    }
                    if (card.Type.Contains("Forest"))
                    {
                        card.TypeCode += "BL5";
                        card.Mana = "G";
                    }
                }
                else
                {
                    card.TypeCode += "L";
                    if (card.Text.Contains("Add {B} or {G}"))
                    {
                        card.Mana = "BG";
                    }
                    if (card.Text.Contains("Add {B} or {R}"))
                    {
                        card.Mana = "BR";
                    }
                    if (card.Text.Contains("Add {G} or {U}"))
                    {
                        card.Mana = "GU";
                    }
                    if (card.Text.Contains("Add {G} or {W}"))
                    {
                        card.Mana = "GW";
                    }
                    if (card.Text.Contains("Add {R} or {G}"))
                    {
                        card.Mana = "RG";
                    }
                    if (card.Text.Contains("Add {R} or {W}"))
                    {
                        card.Mana = "RW";
                    }
                    if (card.Text.Contains("Add {U} or {B}"))
                    {
                        card.Mana = "UB";
                    }
                    if (card.Text.Contains("Add {U} or {R}"))
                    {
                        card.Mana = "UR";
                    }
                    if (card.Text.Contains("Add {W} or {B}"))
                    {
                        card.Mana = "WB";
                    }
                    if (card.Text.Contains("Add {W} or {U}"))
                    {
                        card.Mana = "WU";
                    }
                }
                card.CMC = "0";
            }
            if (card.Type.Contains("Plane"))
            {
                card.TypeCode += "N";
            }
            if (card.Type.Contains("Artifact"))
            {
                card.TypeCode += "A";
            }
            if (card.Type.Contains("Equipment"))
            {
                card.TypeCode += "Q";
            }
            if (card.Type.Contains("Enchantment"))
            {
                card.TypeCode += "E";
            }
            if (card.Type.Contains("Aura"))
            {
                card.TypeCode += "R";
            }
            if (card.Type.Contains("Creature"))
            {
                card.TypeCode += "C";
            }
            if (card.Type.Contains("Planeswalker"))
            {
                card.TypeCode += "P";
            }
            if (card.Type.Contains("Instant"))
            {
                card.TypeCode += "I";
            }
            if (card.Type.Contains("Sorcery"))
            {
                card.TypeCode += "S";
            }
            if (card.Rarity.Contains("Common"))
            {
                card.RarityCode = "C";
            }
            else
            {
                if (card.Rarity.Contains("Uncommon"))
                {
                    card.RarityCode = "U";
                }
                else
                {
                    if (card.Rarity.Contains("Mythic"))
                    {
                        card.RarityCode = "M";
                    }
                    else
                    {
                        if (card.Rarity.Contains("Basic"))
                        {
                            card.RarityCode = "C";
                        }
                        else
                        {
                            card.RarityCode = "R";
                        }
                    }
                }
            }
            return card;
        }

        /// <summary>
        /// Replace foreign letters with legal letters in card name
        /// </summary>
        /// <param name="card">Card to format</param>
        /// <returns>A card with all name in legal letters</returns>
        private static Card FormatSpecialLetter(Card card)
        {
            card.Name = card.Name.Replace("ö", "o").Replace("â", "a").Replace("á", "a").Replace("í", "i").Replace("ú", "u").Replace("û", "u").Replace("Æ", "AE").Replace("é", "e").Replace("à", "a");
            card.Text = card.Text.Replace("ö", "o").Replace("â", "a").Replace("á", "a").Replace("í", "i").Replace("ú", "u").Replace("û", "u").Replace("Æ", "AE").Replace("é", "e").Replace("à", "a");
            return card;
        }

        /// <summary>
        /// Format properties
        /// </summary>
        /// <param name="cards">Cards to format</param>
        /// <returns>A list of cards with properties formatted</returns>
        public static List<Card> Format(List<Card> cards)
        {
            foreach (var item in cards)
            {
                double per = 1.0 * (cards.IndexOf(item) + 1) / cards.Count;
                Consoler.Output(string.Format("Total {0:P1} complete\n 5.Formatting card: {1:P1}", 0.8 + 0.2 * per, per));

                try
                {
                    FormatzText(item);
                    FormatAdition(item);
                    FormatSpecialLetter(item);
                }
                catch (Exception ex)
                {
                    string properties = string.Empty;
                    foreach (System.Reflection.PropertyInfo p in item.GetType().GetProperties())
                    {
                        if (p.GetValue(item)==null)
                        {
                            p.SetValue(item, "NULL");
                        }
                        properties += string.Format("{0}:{1}\n", p.Name, p.GetValue(item));
                    }
                    LoggerError.Log(String.Format("{0}\n{1}", ex.Message, properties));
                }
            }

            return cards;
        }
    }
}
