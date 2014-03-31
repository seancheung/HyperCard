using ACCESSOR;
using MODEL;
using System;
using System.Collections.Generic;

namespace FORMATTER
{
    internal class FormatForeignDetail
    {
        private FormatForeignDetail() { }

        /// <summary>
        /// Get card Foreign details
        /// </summary>
        /// <param name="card">Card to process</param>
        /// <returns>A card with foreign details filled</returns>
        private static Card GetCard(Card card)
        {
            string webdata = Request.GetWebData(GetURL(card.zID));

            if (!webdata.Contains("../../Handlers/Image.ashx?multiverseid="))
            {
                card.zName = "";
                card.zType = "";
                card.zText = "";
                card.zFlavor = "";
                return card;
            }

            if (webdata.Contains("Other Variations"))
            {
                int num = webdata.IndexOf("id=", webdata.IndexOf("<a href", webdata.IndexOf("Other Variations"))) + 4;
                for (int i = 1; i < 5; i++)
                {
                    int num2 = webdata.IndexOf("class=", num) - 2;
                    card.Var += string.Format("({0}:{1})", i, webdata.Substring(num, num2 - num));
                    num = webdata.IndexOf("id=", num2) + 4;
                }
            }
            int num3 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Card Name:")) + 20;
            int num4 = webdata.IndexOf("</div>", num3);
            card.zName = webdata.Substring(num3, num4 - num3).Trim();
            int num5 = webdata.IndexOf("<div class=\"value\">", webdata.IndexOf("Types:")) + 20;
            int num6 = webdata.IndexOf("</div>", num5);
            card.zType = webdata.Substring(num5, num6 - num5).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
            if (webdata.IndexOf("Flavor Text:") > 0)
            {
                int num7 = webdata.IndexOf("</div>", webdata.IndexOf("Flavor Text:")) + 6;
                int num8 = webdata.IndexOf("</div></div>", num7);
                card.zFlavor = webdata.Substring(num7, num8 - num7);
                while (card.zFlavor.Contains("<"))
                {
                    int num9 = card.zFlavor.IndexOf("<");
                    int num10 = card.zFlavor.IndexOf(">");
                    card.zFlavor = card.zFlavor.Remove(num9, num10 - num9 + 1).Trim();
                }
            }
            else
            {
                card.zFlavor = "";
            }
            if (webdata.IndexOf("Card Text:") > 0)
            {
                int num11 = webdata.IndexOf("</div>", webdata.IndexOf("Card Text:")) + 6;
                int num12 = webdata.IndexOf("</div></div>", num11);
                card.zText = webdata.Substring(num11, num12 - num11);
                int num14;
                for (int j = card.zText.IndexOf("<img src="); j > 0; j = card.zText.IndexOf("<img src=", num14))
                {
                    int num13 = card.zText.IndexOf("alt=", j) + 4;
                    num14 = card.zText.IndexOf("align=", num13);
                    string str = card.zText.Substring(num13, num14 - num13).Replace("\"", string.Empty).Trim();
                    card.zText = card.zText.Insert(j, String.Format("{{{0}}}", str));
                }
                while (card.zText.Contains("<"))
                {
                    int num9 = card.zText.IndexOf("<");
                    int num10 = card.zText.IndexOf(">");
                    card.zText = card.zText.Remove(num9, num10 - num9 + 1).Trim();
                }
            }
            else
            {
                card.zText = "";
            }
            if (card.isdoubleface)
            {
                int num15 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Name:")) + 20;
                int num16 = webdata.IndexOf("</div>", num15);
                card.zName = String.Format("{0}|{1}", card.zName, webdata.Substring(num15, num16 - num15).Trim());
                int num17 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Types:")) + 20;
                int num18 = webdata.IndexOf("</div>", num17);
                card.zType = String.Format("{0}|{1}", card.zType, webdata.Substring(num17, num18 - num17).Replace("-", string.Empty).Replace(" ", string.Empty).Trim());
                if (webdata.IndexOf("Flavor Text:") > 0 && webdata.IndexOf("Flavor Text:") != webdata.LastIndexOf("Flavor Text:"))
                {
                    int num19 = webdata.IndexOf("</div>", webdata.LastIndexOf("Flavor Text:")) + 6;
                    int num20 = webdata.IndexOf("</div></div>", num19);
                    card.zFlavor = String.Format("{0}|{1}", card.zFlavor, webdata.Substring(num19, num20 - num19));
                    while (card.zFlavor.Contains("<"))
                    {
                        int num9 = card.zFlavor.IndexOf("<");
                        int num10 = card.zFlavor.IndexOf(">");
                        card.zFlavor = card.zFlavor.Remove(num9, num10 - num9 + 1).Trim();
                    }
                }
                else
                {
                    card.zFlavor += "|";
                }
                if (webdata.IndexOf("Card Text:") > 0 && webdata.IndexOf("Card Text:") != webdata.LastIndexOf("Card Text:"))
                {
                    int num21 = webdata.IndexOf("multiverseid=", webdata.LastIndexOf("<img src=\"../../Handlers/Image.ashx?multiverseid=")) + 13;
                    int num22 = webdata.IndexOf("&amp", num21);
                    card.zID = String.Format("{0}|{1}", card.zID, webdata.Substring(num21, num22 - num21).Trim());
                    int num23 = webdata.IndexOf("</div>", webdata.LastIndexOf("Card Text:")) + 6;
                    int num24 = webdata.IndexOf("</div></div>", num23);
                    card.zText = String.Format("{0}|{1}", card.zText, webdata.Substring(num23, num24 - num23));
                    int num14;
                    for (int j = card.zText.IndexOf("<img src="); j > 0; j = card.zText.IndexOf("<img src=", num14))
                    {
                        int num13 = card.zText.IndexOf("alt=", j) + 4;
                        num14 = card.zText.IndexOf("align=", num13);
                        string str = card.zText.Substring(num13, num14 - num13).Replace("\"", string.Empty).Trim();
                        card.zText = card.zText.Insert(j, String.Format("{{{0}}}", str));
                    }
                    while (card.zText.Contains("<"))
                    {
                        int num9 = card.zText.IndexOf("<");
                        int num10 = card.zText.IndexOf(">");
                        card.zText = card.zText.Remove(num9, num10 - num9 + 1).Trim();
                    }
                }
                else
                {
                    card.zText += "|";
                }
            }
            else
            {
                if (card.issplit)
                {
                    int num15 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Card Name:")) + 20;
                    int num16 = webdata.IndexOf("</div>", num15);
                    card.zName = String.Format("{0}|{1}", card.zName, webdata.Substring(num15, num16 - num15).Trim());
                    int num17 = webdata.IndexOf("<div class=\"value\">", webdata.LastIndexOf("Types:")) + 20;
                    int num18 = webdata.IndexOf("</div>", num17);
                    card.zType = String.Format("{0}|{1}", card.zType, webdata.Substring(num17, num18 - num17).Replace("-", string.Empty).Replace(" ", string.Empty).Trim());
                }
            }

            return card;

        }

        /// <summary>
        /// Get url of the foreign data
        /// </summary>
        /// <param name="zid">Card Foreign ID</param>
        /// <returns>the url for webrequesting</returns>
        private static string GetURL(string zid)
        {
            if (zid.Contains("|"))
            {
                zid = zid.Remove(zid.IndexOf("|"));
            }

            return string.Format("http://gatherer.wizards.com/Pages/Card/Details.aspx?printed=true&multiverseid={0}", zid);
        }

        /// <summary>
        /// Get a list of cards with foreign details filled
        /// </summary>
        /// <param name="cards">Cards to process</param>
        /// <returns>A list of cards with foreign details filled</returns>
        public static List<Card> GetCards(List<Card> cards)
        {
            List<Card> result = new List<Card>();
            foreach (var item in cards)
            {
                double per = 1.0 * (cards.IndexOf(item) + 1) / cards.Count;
                Consoler.Output(string.Format("Total {0:P1} complete\n 3.Getting card foreign details: {1:P1}", 0.4 + 0.2 * per, per));

                if (!string.IsNullOrEmpty(item.zID))
                {
                    Card card = GetCard(item);
                }
                else
                {
                    item.zName = "";
                    item.zType = "";
                    item.zText = "";
                    item.zFlavor = "";
                }
                result.Add(item);
            }

            return result;
        }
    }
}
