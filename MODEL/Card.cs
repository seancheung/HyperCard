
using System;
using System.Collections.Generic;
namespace MODEL
{
    /// <summary>
    /// Card class that contains all basic info
    /// </summary>
    public class Card
    {

        /// <summary>
        /// the color of its B-side card(if double-faced)
        /// (use use ' ' as separator for multi-color, e.g. 'Red Blue')
        /// </summary>
        public string bColor { get; set; }

        /// <summary>
        /// English WotcID of the card
        /// (use '|' as separator for dual, e.g. '12345|67890')
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Foreign WotcID of the card
        /// (use '|' as separator for dual, e.g. '12345|67890')
        /// </summary>
        public string zID { get; set; }

        /// <summary>
        /// Variation of the card(for basic land card)
        /// (in the format of '(1:373546)(2:373609)(3:373683)(4:373746)')
        /// </summary>
        public string Var { get; set; }

        /// <summary>
        /// English name of the card
        /// (use '|' as separator for dual, e.g. 'ABC|DEF')
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Foreign name of the card
        /// (use '|' as separator for dual, e.g. 'ABC|DEF')
        /// </summary>
        public string zName { get; set; }

        /// <summary>
        /// Full english set name of the card
        /// (use '|' as separator for dual, e.g. 'ABC|DEF')
        /// </summary>
        public string Set { get; set; }

        /// <summary>
        /// Setcode in capital
        /// </summary>
        public string SetCode { get; set; }

        /// <summary>
        /// Full english color name of the card
        /// (use use ' ' as separator for multi-color, e.g. 'Blue Red')
        /// (use use '|' as separator for dual, e.g. 'Blue|Black')
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Colorcode in capital
        /// (no separator needed for multi-color, e.g. 'UR')
        /// (use use '|' as separator for dual, e.g. 'U|B')
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// Cost of the card
        /// (use '{}' for each mana symbol, e.g. '{3}{B}{R}')
        /// (bracket hybrid mana symbol as one, e.g. '{WU}')
        /// (use use '|' as separator for dual, e.g. '{1}{W}|{2}{G}{G}')
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// Converted mana cost of the card
        /// (use use '|' as separator for dual, e.g. '3|2')
        /// </summary>
        public string CMC { get; set; }

        /// <summary>
        /// Type of the card
        /// (use use '|' as separator for dual, e.g. 'Creature — Human Advisor|Creature — Human Mutant')
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Type of the card in foreign
        /// (use use '|' as separator for dual, e.g. 'Creature — Human Advisor|Creature — Human Mutant')
        /// </summary>
        public string zType { get; set; }

        /// <summary>
        /// Typecode in capital
        /// (no separator needed for multi-type, e.g. 'AC')
        /// (use use '|' as separator for dual, e.g. 'C|C')
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// the mana it can generate
        /// manacode in capital
        /// </summary>
        public string Mana { get; set; }

        /// <summary>
        /// Power of the card(creature)
        /// (use use '|' as separator for dual, e.g. '1|3')
        /// </summary>
        public string Pow { get; set; }

        /// <summary>
        /// Toughness of the card(creature)
        /// (use use '|' as separator for dual, e.g. '1|3')
        /// </summary>
        public string Tgh { get; set; }

        /// <summary>
        /// Loyalty of the card(planeswalker)
        /// (use use '|' as separator for dual, e.g. '3|0')
        /// </summary>
        public string Loyalty { get; set; }

        /// <summary>
        /// English text of the card
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Foreign text of the card
        /// </summary>
        public string zText { get; set; }

        /// <summary>
        /// English flavor of the card
        /// </summary>
        public string Flavor { get; set; }

        /// <summary>
        /// Foreign flavor of the card
        /// </summary>
        public string zFlavor { get; set; }

        /// <summary>
        /// Artist name of the card
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Rarity of the card
        /// </summary>
        public string Rarity { get; set; }

        /// <summary>
        /// Raritycode of the card
        /// </summary>
        public string RarityCode { get; set; }

        /// <summary>
        /// Number of the card
        /// (use use '|' as separator for dual, e.g. '121a|121b')
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Rulings of the card
        /// </summary>
        public string Rulings { get; set; }
        /// <summary>
        /// Legality of the card
        /// </summary>
        public string Legality { get; set; }

        /// <summary>
        /// Community rating of the card
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Mana symbol paths
        /// </summary>
        public List<string> cPic
        {
            get
            {
                List<string> paths = new List<string>();
                char[] separator = new char[] { '{', '}' };
                string[] array = new string[] { "" };
                array = Cost.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string mana = array[i];
                    if (mana != "|")
                        paths.Add(String.Format("/Resources/mana_{0}.png", mana.ToLower()));
                    else
                        paths.Add("/Resources/mana_sep.png");
                }

                return paths;
            }
        }

        /// <summary>
        /// Type icon paths
        /// </summary>
        public List<string> tPic
        {
            get
            {
                List<string> paths = new List<string>();
                foreach (var t in TypeCode)
                {
                    paths.Add(string.Format("/Resources/type_{0}.png", t.ToString().ToLower()));
                }
                return paths;
            }

        }

        /// <summary>
        /// Get split IDs
        /// </summary>
        public string[] IDs
        {
            get
            {
                if (!ID.Contains("|"))
                    return new string[] { ID };
                else
                {
                    return new string[]
                    {
                        ID.Remove(ID.IndexOf("|")),
                        ID.Substring(ID.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split zIDs
        /// </summary>
        public string[] zIDs
        {
            get
            {
                if (!zID.Contains("|"))
                    return new string[] { zID };
                else
                {
                    return new string[]
                    {
                        zID.Remove(zID.IndexOf("|")),
                        zID.Substring(zID.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Names
        /// </summary>
        public string[] Names
        {
            get
            {
                if (!Name.Contains("|"))
                    return new string[] { Name };
                else
                {
                    return new string[]
                    {
                        Name.Remove(Name.IndexOf("|")),
                        Name.Substring(Name.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split zNames
        /// </summary>
        public string[] zNames
        {
            get
            {
                if (!zName.Contains("|"))
                    return new string[] { zName };
                else
                {
                    return new string[]
                    {
                        zName.Remove(zName.IndexOf("|")),
                        zName.Substring(zName.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Colors
        /// </summary>
        public string[] Colors
        {
            get
            {
                if (!Color.Contains("|"))
                    return new string[] { Color };
                else
                {
                    return new string[]
                    {
                        Color.Remove(Color.IndexOf("|")),
                        Color.Substring(Color.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Costs
        /// </summary>
        public string[] Costs
        {
            get
            {
                if (!Cost.Contains("|"))
                    return new string[] { Cost };
                else
                {
                    return new string[]
                    {
                        Cost.Remove(Cost.IndexOf("|")),
                        Cost.Substring(Cost.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Types
        /// </summary>
        public string[] Types
        {
            get
            {
                if (!Type.Contains("|"))
                    return new string[] { Type };
                else
                {
                    return new string[]
                    {
                        Type.Remove(Type.IndexOf("|")),
                        Type.Substring(Type.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split zTypes
        /// </summary>
        public string[] zTypes
        {
            get
            {
                if (!zType.Contains("|"))
                    return new string[] { zType };
                else
                {
                    return new string[]
                    {
                        zType.Remove(zType.IndexOf("|")),
                        zType.Substring(zType.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Powers
        /// </summary>
        public string[] Pows
        {
            get
            {
                if (!Pow.Contains("|"))
                    return new string[] { Pow };
                else
                {
                    return new string[]
                    {
                        Pow.Remove(Pow.IndexOf("|")),
                        Pow.Substring(Pow.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Toughnesses
        /// </summary>
        public string[] Tghs
        {
            get
            {
                if (!Tgh.Contains("|"))
                    return new string[] { Tgh };
                else
                {
                    return new string[]
                    {
                        Tgh.Remove(Tgh.IndexOf("|")),
                        Tgh.Substring(Tgh.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Texts
        /// </summary>
        public string[] Texts
        {
            get
            {
                if (!Text.Contains("|"))
                    return new string[] { Text };
                else
                {
                    return new string[]
                    {
                        Text.Remove(Text.IndexOf("|")),
                        Text.Substring(Text.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split zTexts
        /// </summary>
        public string[] zTexts
        {
            get
            {
                if (!zText.Contains("|"))
                    return new string[] { zText };
                else
                {
                    return new string[]
                    {
                        zText.Remove(zText.IndexOf("|")),
                        zText.Substring(zText.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Flavors
        /// </summary>
        public string[] Flavors
        {
            get
            {
                if (!Flavor.Contains("|"))
                    return new string[] { Flavor };
                else
                {
                    return new string[]
                    {
                        Flavor.Remove(Flavor.IndexOf("|")),
                        Flavor.Substring(Flavor.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split zFlavors
        /// </summary>
        public string[] zFlavors
        {
            get
            {
                if (!zFlavor.Contains("|"))
                    return new string[] { zFlavor };
                else
                {
                    return new string[]
                    {
                        zFlavor.Remove(zFlavor.IndexOf("|")),
                        zFlavor.Substring(zFlavor.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Rarities
        /// </summary>
        public string[] Rarities
        {
            get
            {
                if (!Rarity.Contains("|"))
                    return new string[] { Rarity };
                else
                {
                    return new string[]
                    {
                        Rarity.Remove(Rarity.IndexOf("|")),
                        Rarity.Substring(Rarity.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Numbers
        /// </summary>
        public string[] Numbers
        {
            get
            {
                if (!Number.Contains("|"))
                    return new string[] { Number };
                else
                {
                    return new string[]
                    {
                        Number.Remove(Number.IndexOf("|")),
                        Number.Substring(Number.IndexOf("|") + 1)
                    };
                }
            }
        }

        /// <summary>
        /// Get split Variations
        /// </summary>
        public string[] Vars
        {
            get
            {
                if (!Var.Contains(":"))
                    return new string[] { Var };
                else
                {
                    string[] array = Var.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    return new string[]
                    {
                        array[0].Substring(2),
                        array[1].Substring(2),
                        array[2].Substring(2),
                        array[3].Substring(2),
                    };
                }
            }
        }

        /// <summary>
        /// Whether this card is doublefaced
        /// </summary>
        public bool IsDoubleFaced
        {
            get
            {
                return ID.Contains("|");
            }
        }

        /// <summary>
        /// Whwther this card is split
        /// </summary>
        public bool IsSplit
        {
            get
            {
                return Cost.Contains("|") && !ID.Contains("|");
            }
        }

    }
}
