using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using System.IO;
using MODEL;
using System.Net.NetworkInformation;
using FORMATTER;

namespace CONVERTER
{
    public class Compressor
    {
        /// <summary>
        /// Default Image folder path
        /// </summary>
        public static string ImagePath { get { return @"IMAGES\"; } }

        /// <summary>
        /// Default temp directory
        /// </summary>
        public static string TempPath { get { return @"C:\Windows\Temp\hypercard\"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="lang"></param>
        /// <param name="site"></param>
        public static void Unzip(Card card, LANGUAGE lang, Website site = Website.gatherer)
        {
            string zipPath = string.Format("{0}{1}.zip", ImagePath, card.SetCode);
            string id = lang == LANGUAGE.English || card.zID == string.Empty ? card.ID : card.zID;

            string[] ids = new string[] { id, string.Empty };
            if (id.Contains("|"))
            {
                ids[0] = id.Remove(id.IndexOf("|"));
                ids[1] = id.Substring(id.IndexOf("|") + 1);
            }

            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            if (!File.Exists(zipPath))
            {
                using (ZipFile zipFile = new ZipFile(zipPath))
                {
                    zipFile.Save();
                }
            }

            using (ZipFile zipFile = ZipFile.Read(zipPath))
            {
                if (!zipFile.ContainsEntry(string.Format("{0}.jpg", ids[0])) || (ids[1] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[1]))))
                {
                    if (!NetworkInterface.GetIsNetworkAvailable())
                    {
                        return;
                    }
                    try
                    {
                        FormatImage.DownloadImg(card, TempPath, lang, site);

                        for (int i = 0; i < 2; i++)
                        {
                            if (ids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[i])))
                                zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, ids[i]), "\\");
                        }

                        zipFile.Save();
                    }
                    catch (System.Exception ex)
                    {
                        LoggerError.Log(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (ids[i] != string.Empty)
                                zipFile[ids[i] + ".jpg"].Extract(TempPath, ExtractExistingFileAction.DoNotOverwrite);
                        }
                    }

                    catch (System.Exception ex)
                    {
                        LoggerError.Log(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setcode"></param>
        public static void Zip(string id, string setcode)
        {
            string zipPath = string.Format("{0}{1}.zip", ImagePath, setcode);

            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            if (!File.Exists(zipPath))
            {
                using (ZipFile zipFile = new ZipFile(zipPath))
                {
                    zipFile.Save();
                }
            }

            try
            {
                using (ZipFile zipFile = ZipFile.Read(zipPath))
                {
                    if (!zipFile.ContainsEntry(string.Format("{0}.jpg", id)) && File.Exists(String.Format("{0}{1}.jpg", TempPath, id)))
                        zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, id), "\\");
                    zipFile.Save();
                }
            }
            catch (System.Exception ex)
            {
                LoggerError.Log(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        public static void Zip(Card card)
        {
            string zipPath = string.Format("{0}{1}.zip", ImagePath, card.SetCode);

            string[] ids = new string[] { card.ID, string.Empty };
            if (card.ID.Contains("|"))
            {
                ids[0] = card.ID.Remove(card.ID.IndexOf("|"));
                ids[1] = card.ID.Substring(card.ID.IndexOf("|") + 1);
            }

            string[] zids = new string[] { card.zID, string.Empty };
            if (card.zID.Contains("|"))
            {
                ids[0] = card.zID.Remove(card.zID.IndexOf("|"));
                ids[1] = card.zID.Substring(card.zID.IndexOf("|") + 1);
            }

            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            if (!File.Exists(zipPath))
            {
                using (ZipFile zipFile = new ZipFile(zipPath))
                {
                    zipFile.Save();
                }
            }

            try
            {
                using (ZipFile zipFile = ZipFile.Read(zipPath))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (ids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[i])))
                        {
                            FormatImage.DownloadImg(ids[i], TempPath);
                            zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, ids[i]), "\\");
                        }
                        if (zids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", zids[i])))
                        {
                            FormatImage.DownloadImg(zids[i], TempPath);
                            zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, zids[i]), "\\");
                        }
                    }

                    zipFile.Save();

                }
            }
            catch (System.Exception ex)
            {
                LoggerError.Log(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="lang"></param>
        /// <param name="isFront"></param>
        /// <returns></returns>
        public static string GetImagePath(Card card, LANGUAGE lang, bool isFront = true)
        {
            string uri = null;

            string id = lang == LANGUAGE.English || card.zID == string.Empty ? card.ID : card.zID;

            Unzip(card, lang);

            if (id.Contains("|"))
            {
                if (isFront)
                {
                    id = id.Remove(id.IndexOf("|"));
                }
                else
                {
                    id = id.Substring(id.IndexOf("|") + 1);
                }

                uri = string.Format("{0}{1}.jpg", TempPath, id);
            }
            else
            {
                if (!isFront)
                {
                    uri = @"\Resources\frame_back.jpg";
                }
                else
                {
                    uri = string.Format("{0}{1}.jpg", TempPath, id);
                }
                
            }

            //if (id.Contains("|"))
            //    if (isFront) id = id.Remove(id.IndexOf("|"));
            //    else id = id.Substring(id.IndexOf("|") + 1);
            //if (isFront) 
            //    uri = string.Format("{0}{1}.jpg", TempPath, id);
            //else
            //    uri = @"\Resources\frame_back.jpg";

            return uri;
        }
    }
}
