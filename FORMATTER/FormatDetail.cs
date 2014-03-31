using ACCESSOR;
using MODEL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FORMATTER
{
    internal class FormatDetail
    {
        private FormatDetail() { }

        /// <summary>
        /// Get card details
        /// </summary>
        /// <param name="card">Card to process</param>
        /// <returns>A card with details filled</returns>
        private static Card GetCard(Card card)
        {
            string webdata = Request.GetWebData(GetURL(card.ID));

            if (!webdata.Contains("Card Name:"))
            {
                return card;
            }
            else
            {
                if (webdata.Contains("Other Variations"))
                {
                    int num = webdata.IndexOf("id=", webdata.IndexOf("<a href", webdata.IndexOf("Other Variations"))) + 4;
                    for (int j = 1; j < 5; j++)
                    {
                        int num2 = webdata.IndexOf("class=", num) - 2;
                        string text2 = webdata.Substring(num, num2 - num);
                        if (text2.Length > 10)
                        {
                            break;
                        }
                        card.Var += string.Format("({0}:{1})", j, text2);
                        num = webdata.IndexOf("id=", num2) + 4;
                    }
                }
                if (webdata.IndexOf("Card Name:") != webdata.LastIndexOf("Card Name:"))
                {
                    if (webdata.IndexOf("Converted Mana Cost:") != webdata.LastIndexOf("Converted Mana Cost:"))
                    {
                        card.issplit = true;
                        card.isdoubleface = false;
                    }
                    else
                    {
                        card.isdoubleface = true;
                        card.issplit = false;
                    }
                }
                else
                {
                    card.isdoubleface = false;
                    card.issplit = false;
                }
                int num3 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Card Name:")) + 20;
                int num4 = webdata.IndexOf("</div>", num3);
                card.Name = webdata.Substring(num3, num4 - num3).Trim();
                if (webdata.IndexOf("Converted Mana Cost:") > 0)
                {
                    int num5 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Converted Mana Cost:")) + 20;
                    int num6 = webdata.IndexOf("<br />", num5);
                    card.CMC = webdata.Substring(num5, num6 - num5).Trim();
                }
                int num7 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Types:")) + 20;
                int num8 = webdata.IndexOf("</div>", num7);
                card.Type = webdata.Substring(num7, num8 - num7).Trim();
                if (webdata.IndexOf("Flavor Text:") > 0)
                {
                    int num9 = webdata.IndexOf("</div>", webdata.IndexOf("Flavor Text:")) + 6;
                    int num10 = webdata.IndexOf("</div></div>", num9);
                    card.Flavor = webdata.Substring(num9, num10 - num9).Trim();
                    while (card.Flavor.Contains("<"))
                    {
                        int num11 = card.Flavor.IndexOf("<");
                        int num12 = card.Flavor.IndexOf(">");
                        card.Flavor = card.Flavor.Remove(num11, num12 - num11 + 1).Trim();
                    }
                }
                else
                {
                    card.Flavor = "";
                }
                if (webdata.IndexOf("P/T:") > 0)
                {
                    int num13 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("P/T:")) + 20;
                    int num14 = webdata.IndexOf("</div>", num13);
                    string text3 = webdata.Substring(num13, num14 - num13);
                    card.Pow = text3.Substring(0, text3.IndexOf("/")).Trim();
                    card.Tgh = text3.Substring(text3.IndexOf("/") + 1).Trim();
                }
                else
                {
                    card.Pow = "";
                    card.Tgh = "";
                }
                if (webdata.IndexOf("Loyalty:") > 0)
                {
                    int num15 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Loyalty:")) + 20;
                    int num16 = webdata.IndexOf("</div>", num15);
                    card.Loyalty = webdata.Substring(num15, num16 - num15).Trim();
                }
                else
                {
                    card.Loyalty = "";
                }
                int num17 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Rarity:")) + 20;
                int num18 = webdata.IndexOf("</div>", num17);
                card.Rarity = webdata.Substring(num17, num18 - num17).Trim();
                while (card.Rarity.Contains("<"))
                {
                    int num11 = card.Rarity.IndexOf("<");
                    int num12 = card.Rarity.IndexOf(">");
                    card.Rarity = card.Rarity.Remove(num11, num12 - num11 + 1).Trim();
                }
                if (webdata.Contains("Card Number:"))
                {
                    int num19 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Card Number:")) + 20;
                    int num20 = webdata.IndexOf("</div>", num19);
                    card.Number = webdata.Substring(num19, num20 - num19).Trim();
                }
                else
                {
                    card.Number = "";
                }
                int num21 = webdata.IndexOf("</div>", webdata.IndexOf("Artist:")) + 6;
                int num22 = webdata.IndexOf("</div>", num21);
                card.Artist = webdata.Substring(num21, num22 - num21).Trim();
                while (card.Artist.Contains("<"))
                {
                    int num11 = card.Artist.IndexOf("<");
                    int num12 = card.Artist.IndexOf(">");
                    card.Artist = card.Artist.Remove(num11, num12 - num11 + 1).Trim();
                }
                if (webdata.IndexOf("Hide Rulings") > 0)
                {
                    int num23 = webdata.IndexOf("</a>", webdata.IndexOf("Hide Rulings"));
                    int num24 = webdata.IndexOf("<b class=\"aa\"><b></b></b>", num23);
                    card.Rulings = webdata.Substring(num23, num24 - num23).Trim();
                    while (card.Rulings.Contains("<"))
                    {
                        int num11 = card.Rulings.IndexOf("<");
                        int num12 = card.Rulings.IndexOf(">");
                        card.Rulings = card.Rulings.Remove(num11, num12 - num11 + 1).Trim();
                    }
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[\\s]{2,}", options);
                    card.Rulings = regex.Replace(card.Rulings, "\n").Trim();
                }
                else
                {
                    card.Rulings = "";
                }
                if (card.isdoubleface)
                {
                    int num25 = webdata.IndexOf("multiverseid=", webdata.LastIndexOf("<img src=\"../../Handlers/Image.ashx?multiverseid=")) + 13;
                    int num26 = webdata.IndexOf("&amp", num25);
                    string text4 = webdata.Substring(num25, num26 - num25).Trim();
                    if (text4 == card.ID)
                    {
                        return null;
                    }
                    card.ID = String.Format("{0}|{1}", card.ID, text4);
                    int num27 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Name:")) + 20;
                    int num28 = webdata.IndexOf("</div>", num27);
                    card.Name = String.Format("{0}|{1}", card.Name, webdata.Substring(num27, num28 - num27).Trim());
                    int num29 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Types:")) + 20;
                    int num30 = webdata.IndexOf("</div>", num29);
                    card.Type = String.Format("{0}|{1}", card.Type, webdata.Substring(num29, num30 - num29).Trim());
                    if (webdata.IndexOf("Card Text:") > 0 && webdata.IndexOf("Card Text:") != webdata.LastIndexOf("Card Text:"))
                    {
                        int num31 = webdata.IndexOf("</div>", webdata.LastIndexOf("Card Text:")) + 6;
                        int num32 = webdata.IndexOf("</div></div>", num31);
                        card.Text = String.Format("{0}|{1}", card.Text, webdata.Substring(num31, num32 - num31).Trim());
                        int num34;
                        for (int k = card.Text.IndexOf("<img src="); k > 0; k = card.Text.IndexOf("<img src=", num34))
                        {
                            int num33 = card.Text.IndexOf("alt=", k) + 4;
                            num34 = card.Text.IndexOf("align=", num33);
                            string str = card.Text.Substring(num33, num34 - num33).Replace("\"", string.Empty).Trim();
                            card.Text = card.Text.Insert(k, String.Format("{{{0}}}", str));
                        }
                        while (card.Text.Contains("<"))
                        {
                            int num11 = card.Text.IndexOf("<");
                            int num12 = card.Text.IndexOf(">");
                            card.Text = card.Text.Remove(num11, num12 - num11 + 1).Trim();
                        }
                    }
                    else
                    {
                        card.Text += "|";
                    }
                    if (webdata.IndexOf("Flavor Text:") > 0 && webdata.IndexOf("Flavor Text:") != webdata.LastIndexOf("Flavor Text:"))
                    {
                        int num35 = webdata.IndexOf("</div>", webdata.LastIndexOf("Flavor Text:")) + 6;
                        int num36 = webdata.IndexOf("</div></div>", num35);
                        card.Flavor = String.Format("{0}|{1}", card.Flavor, webdata.Substring(num35, num36 - num35).Trim());
                        while (card.Flavor.Contains("<"))
                        {
                            int num11 = card.Flavor.IndexOf("<");
                            int num12 = card.Flavor.IndexOf(">");
                            card.Flavor = card.Flavor.Remove(num11, num12 - num11 + 1).Trim();
                        }
                    }
                    else
                    {
                        card.Flavor += "|";
                    }
                    if (webdata.IndexOf("P/T:") > 0 && webdata.IndexOf("P/T:") != webdata.LastIndexOf("P/T:"))
                    {
                        int num37 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("P/T:")) + 20;
                        int num38 = webdata.IndexOf("</div>", num37);
                        string text5 = webdata.Substring(num37, num38 - num37);
                        card.Pow = String.Format("{0}|{1}", card.Pow, text5.Substring(0, text5.IndexOf("/")).Trim());
                        card.Tgh = String.Format("{0}|{1}", card.Tgh, text5.Substring(text5.IndexOf("/") + 1).Trim());
                    }
                    else
                    {
                        card.Pow += "|";
                        card.Tgh += "|";
                    }
                    int num39 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Rarity:")) + 20;
                    int num40 = webdata.IndexOf("</div>", num39);
                    card.Rarity = String.Format("{0}|{1}", card.Rarity, webdata.Substring(num39, num40 - num39).Trim());
                    while (card.Rarity.Contains("<"))
                    {
                        int num11 = card.Rarity.IndexOf("<");
                        int num12 = card.Rarity.IndexOf(">");
                        card.Rarity = card.Rarity.Remove(num11, num12 - num11 + 1).Trim();
                    }
                    if (webdata.Contains("Card Number:") && webdata.IndexOf("Card Number:") != webdata.LastIndexOf("Card Number:"))
                    {
                        int num41 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Number:")) + 20;
                        int num42 = webdata.IndexOf("</div>", num41);
                        card.Number = String.Format("{0}|{1}", card.Number, webdata.Substring(num41, num42 - num41).Trim());
                    }
                    else
                    {
                        card.Number += "|";
                    }
                    if (webdata.LastIndexOf("Color Indicator:") > 0)
                    {
                        int num43 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Color Indicator:")) + 20;
                        int num44 = webdata.IndexOf("</div>", num43);
                        card.bcolor = webdata.Substring(num43, num44 - num43).Trim();
                    }
                }
                else
                {
                    if (card.issplit)
                    {
                        int num27 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Name:")) + 20;
                        int num28 = webdata.IndexOf("</div>", num27);
                        int num29 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Types:")) + 20;
                        int num30 = webdata.IndexOf("</div>", num29);
                        card.Type = String.Format("{0}|{1}", card.Type, webdata.Substring(num29, num30 - num29).Trim());
                        if (webdata.IndexOf("Converted Mana Cost:") > 0 && webdata.IndexOf("Converted Mana Cost:") != webdata.LastIndexOf("Converted Mana Cost:"))
                        {
                            int num5 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Converted Mana Cost:")) + 20;
                            int num6 = webdata.IndexOf("<br />", num5);
                            card.CMC = String.Format("{0}|{1}", card.CMC, webdata.Substring(num5, num6 - num5).Trim());
                        }
                        if (webdata.IndexOf("Mana Cost:") > 0 && webdata.IndexOf("Mana Cost:") != webdata.LastIndexOf("Mana Cost:"))
                        {
                            int num45 = webdata.IndexOf("<div class=\"value\">", num28) + 20;
                            int num46 = webdata.IndexOf("</div>", num45);
                            string text6 = webdata.Substring(num45, num46 - num45).Trim();
                            int num34;
                            for (int k = text6.IndexOf("<img src="); k > 0; k = text6.IndexOf("<img src=", num34))
                            {
                                int num33 = text6.IndexOf("alt=", k) + 4;
                                num34 = text6.IndexOf("align=", num33);
                                string str = text6.Substring(num33, num34 - num33).Replace("\"", string.Empty).Trim();
                                text6 = text6.Insert(k, String.Format("{{{0}}}", str));
                            }
                            while (text6.Contains("<"))
                            {
                                int num11 = text6.IndexOf("<");
                                int num12 = text6.IndexOf(">");
                                text6 = text6.Remove(num11, num12 - num11 + 1).Trim();
                            }
                            card.Cost = String.Format("{0}|{1}", card.Cost, text6);
                        }
                        if (webdata.IndexOf("Card Text:") > 0)
                        {
                            int num47 = webdata.IndexOf("</div>", webdata.IndexOf("Card Text:")) + 6;
                            int num48 = webdata.IndexOf("</div></div>", num47);
                            card.Text = webdata.Substring(num47, num48 - num47).Trim();
                            int num34;
                            for (int k = card.Text.IndexOf("<img src="); k > 0; k = card.Text.IndexOf("<img src=", num34))
                            {
                                int num33 = card.Text.IndexOf("alt=", k) + 4;
                                num34 = card.Text.IndexOf("align=", num33);
                                string str = card.Text.Substring(num33, num34 - num33).Replace("\"", string.Empty).Trim();
                                card.Text = card.Text.Insert(k, String.Format("{{{0}}}", str));
                            }
                            while (card.Text.Contains("<"))
                            {
                                int num11 = card.Text.IndexOf("<");
                                int num12 = card.Text.IndexOf(">");
                                card.Text = card.Text.Remove(num11, num12 - num11 + 1).Trim();
                            }
                        }
                        int num39 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Rarity:")) + 20;
                        int num40 = webdata.IndexOf("</div>", num39);
                        card.Rarity = String.Format("{0}|{1}", card.Rarity, webdata.Substring(num39, num40 - num39).Trim());
                        while (card.Rarity.Contains("<"))
                        {
                            int num11 = card.Rarity.IndexOf("<");
                            int num12 = card.Rarity.IndexOf(">");
                            card.Rarity = card.Rarity.Remove(num11, num12 - num11 + 1).Trim();
                        }
                        if (webdata.Contains("Card Number:") && webdata.IndexOf("Card Number:") != webdata.LastIndexOf("Card Number:"))
                        {
                            int num41 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Number:")) + 20;
                            int num42 = webdata.IndexOf("</div>", num41);
                            card.Number = String.Format("{0}|{1}", card.Number, webdata.Substring(num41, num42 - num41).Trim());
                        }
                        else
                        {
                            card.Number += "|";
                        }
                        if (webdata.LastIndexOf("Color Indicator:") > 0)
                        {
                            int num43 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Color Indicator:")) + 20;
                            int num44 = webdata.IndexOf("</div>", num43);
                            card.bcolor = webdata.Substring(num43, num44 - num43).Trim();
                        }
                    }
                }
                if (webdata.IndexOf("class=\"textRatingValue\">") > 0)
                {
                    int num49 = webdata.IndexOf("class=\"textRatingValue\">") + 24;
                    int num50 = webdata.IndexOf("</span>", num49);
                    card.Rating = webdata.Substring(num49, num50 - num49).Trim();
                }
            }

            return card;
        }

        /// <summary>
        /// Get url of the card list data
        /// </summary>
        /// <param name="id">Card ID</param>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL(string id)
        {
            if (id.Contains("|"))
            {
                id = id.Remove(id.IndexOf("|"));
            }

            return string.Format("http://gatherer.wizards.com/Pages/Card/Details.aspx?printed=true&multiverseid={0}", id);
        }

        /// <summary>
        /// Get a list of cards with details filled
        /// </summary>
        /// <param name="cards">Cards to process</param>
        /// <returns>A list of cards with details filled</returns>
        public static List<Card> GetCards(List<Card> cards)
        {
            List<Card> result = new List<Card>();
            foreach (var item in cards)
            {
                double per = 1.0 * (cards.IndexOf(item) + 1) / cards.Count;
                Consoler.Output(string.Format("Total {0:P1} complete\n 1.Getting card details: {1:P1}", 0.2 * per, per));

                Card card = GetCard(item);
                if (card != null)
                {
                    result.Add(card);
                }
            }

            return result;
        }
    }
}
