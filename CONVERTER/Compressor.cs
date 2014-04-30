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
        private static object _lock = new object();

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
            string[] ids = lang == LANGUAGE.English || card.zID == string.Empty ? card.IDs : card.zIDs;

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
                if (!zipFile.ContainsEntry(string.Format("{0}.jpg", ids[0])) || (ids.Length > 1 && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[1]))))
                {
                    if (!NetworkInterface.GetIsNetworkAvailable())
                    {
                        return;
                    }
                    try
                    {
                        lock (_lock)
                        {
                            FormatImage.DownloadImg(card, TempPath, lang, site);
                        }

                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (ids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[i])))
                                lock (_lock)
                                {
                                    zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, ids[i]), "\\");
                                }

                        }
                        lock (_lock)
                        {
                            zipFile.Save();
                        }
                    }
                    catch (Exception ex)
                    {

                        LoggerError.Log(string.Format("Unzip Error:\nID:{0}\nLanguage:{1}\nSite:{2}\nError:{3}", card.ID, lang, site.ToString(), ex.Message));
                    }
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (ids[i] != string.Empty)
                                lock (_lock)
                                {
                                    zipFile[ids[i] + ".jpg"].Extract(TempPath, ExtractExistingFileAction.DoNotOverwrite);
                                }
                        }
                    }

                    catch (Exception ex)
                    {
                        LoggerError.Log(string.Format("Unzip Error:\nID:{0}\nLanguage:{1}\nSite:{2}\nError:{3}", card.ID, lang, site.ToString(), ex.Message));
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
                        lock (_lock)
                        {
                            zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, id), "\\");
                            zipFile.Save();
                        }
                }
            }
            catch (Exception ex)
            {
                LoggerError.Log(string.Format("Zip Error:\nID:{0}SetCode:{1}\nError:{2}", id, setcode, ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        public static void ZipEx(Card card)
        {
            string zipPath = string.Format("{0}{1}.zip", ImagePath, card.Set);
            string[] names = card.Names;

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
                    zipFile.AddDirectoryByName("EN");
                    zipFile.Save();
                }
            }

            try
            {
                using (ZipFile zipFile = ZipFile.Read(zipPath))
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.full.jpg", names[i])))
                        {
                            if (!zipFile.ContainsEntry(string.Format("\\EN\\{0}.jpg", card.IDs[i])))
                            {
                                
                                lock (_lock)
                                {
                                    FormatImage.DownloadImg(card, TempPath, LANGUAGE.English, Website.magiccards);
                                    zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, card.IDs[i]), "\\EN\\");
                                }
                            }
                            else
                                lock (_lock)
                                {
                                    zipFile["\\EN\\" + card.IDs[i] + ".jpg"].FileName = "\\EN\\" + names[i] + ".full.jpg";
                                }
                        }
                    }
                    lock (_lock)
                    {
                        zipFile.Save();
                    }

                }
            }
            catch (Exception ex)
            {
                LoggerError.Log(string.Format("ZipEx Error:\nIDs:{0}\nError:{1}", card.ID, ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        public static void Zip(Card card)
        {
            string zipPath = string.Format("{0}{1}.zip", ImagePath, card.SetCode);

            string[] ids = card.IDs;

            string[] zids = card.zIDs;

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
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (ids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", ids[i])))
                        {
                            FormatImage.DownloadImg(ids[i], TempPath);
                            lock (_lock)
                            {
                                zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, ids[i]), "\\");
                            }
                        }
                        if (zids[i] != string.Empty && !zipFile.ContainsEntry(string.Format("{0}.jpg", zids[i])))
                        {
                            FormatImage.DownloadImg(zids[i], TempPath);
                            lock (_lock)
                            {
                                zipFile.AddFile(String.Format("{0}{1}.jpg", TempPath, zids[i]), "\\");
                            }
                        }
                    }
                    lock (_lock)
                    {
                        zipFile.Save();
                    }

                }
            }
            catch (Exception ex)
            {
                LoggerError.Log(string.Format("Zip Error:\nID:{0}\nzID:{1}\nError:{2}", card.ID, card.zID, ex.Message));
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

            string[] ids = lang == LANGUAGE.English || card.zID == string.Empty ? card.IDs : card.zIDs;

            Unzip(card, lang);

            if (card.IsDoubleFaced)
                uri = isFront ? string.Format("{0}{1}.jpg", TempPath, ids[0]) : string.Format("{0}{1}.jpg", TempPath, ids[1]);
            else
                uri = isFront ? string.Format("{0}{1}.jpg", TempPath, ids[0]) : @"\Resources\frame_back.jpg";

            return uri;
        }
    }
}
