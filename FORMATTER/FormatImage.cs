using ACCESSOR;
using MODEL;
using System.IO;

namespace FORMATTER
{
    public class FormatImage
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="tmpPath"></param>
        /// <param name="lang"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public static bool DownloadImg(Card card, string tmpPath, LANGUAGE lang = LANGUAGE.English, Website site = Website.gatherer)
        {
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }

            string id = lang == LANGUAGE.English || card.zID == string.Empty ? card.ID : card.zID;

            #region Split multiple ids/nums
            string[] nums = new string[]
			{
				card.Number,
				string.Empty
			};
            string[] ids = new string[]
			{
				id,
				string.Empty
			};

            if (id.Contains("|"))
            {
                ids[0] = id.Remove(id.IndexOf("|"));
                ids[1] = id.Substring(id.IndexOf("|") + 1);
                nums[0] = card.Number.Remove(card.Number.IndexOf("|"));
                nums[1] = card.Number.Substring(card.Number.IndexOf("|") + 1);
            }
            else
            {
                if (card.Number.Contains("|"))
                {
                    nums[0] = card.Number.Remove(card.Number.IndexOf("|"));
                }
            }
            #endregion

            bool result = false;

            for (int i = 0; i < 2; i++)
            {
                if (ids[i] == string.Empty)
                    continue;

                if (!File.Exists(string.Format("{0}{1}.jpg", tmpPath, ids[i])) || new FileInfo(string.Format("{0}{1}.jpg", tmpPath, ids[i])).Length == 0L)
                {
                    string url = GetURL(ids[i], card.SetCode, nums[i], lang, site);
                    result = Downloader.Downloadfile(url, string.Format("{0}{1}.jpg", tmpPath, ids[i]));
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        public static bool DownloadImg(string id, string tmpPath)
        {
            if (!File.Exists(string.Format("{0}{1}.jpg", tmpPath, id)) || new FileInfo(string.Format("{0}{1}.jpg", tmpPath, id)).Length == 0L)
            {
                string url = GetURL(id, null, null);
                return Downloader.Downloadfile(url, string.Format("{0}{1}.jpg", tmpPath, id));
            }
            else return true;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setcode"></param>
        /// <param name="num"></param>
        /// <param name="lang"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        private static string GetURL(string id, string setcode, string num, LANGUAGE lang = LANGUAGE.English, Website site = Website.gatherer)
        {
            //Default is gatherer
            string result = string.Format("http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid={0}&type=card", id);

            switch (site)
            {
                case Website.gatherer:
                    break;
                case Website.magiccards:
                    result = string.Format("http://magiccards.info/scans/{0}/{1}/{2}.jpg", Language.GetLangCode(lang), setcode.ToLower(), num);
                    break;
                case Website.magicspoiler:
                    break;
                case Website.iplaymtg:
                    result = string.Format("http://data.iplaymtg.com/mtgdeck/card/{0}/{1}/{2}.jpg", Language.GetLangCode(lang), setcode.ToUpper(), num);
                    break;
                default:
                    break;
            }

            return result;

        }
    }
}
