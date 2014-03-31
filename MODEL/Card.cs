
namespace MODEL
{
    /// <summary>
    /// Card class that contains all basic info
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Whether it is a double-faced card
        /// </summary>
        public bool isdoubleface { get; set; }

        /// <summary>
        /// Whether it is a split card
        /// </summary>
        public bool issplit { get; set; }

        /// <summary>
        /// the color of its B-side card(if double-faced)
        /// (use use ' ' as separator for multi-color, e.g. 'Red Blue')
        /// </summary>
        public string bcolor { get; set; }

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

        //public System.Collections.Generic.List<string> cPic
        //{
        //    get;
        //    set;
        //}
        //public System.Collections.Generic.List<string> tPic
        //{
        //    get;
        //    set;
        //}
    }
}
