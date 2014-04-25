using MODEL;
using System;
using System.Net;

namespace ACCESSOR
{
    public class Downloader
    {
        private Downloader() { }

        /// <summary>
        /// Downlaod file from the provided url and store it to the provided path
        /// </summary>
        /// <param name="url">Download link</param>
        /// <param name="path">Stroring path</param>
        /// <returns>Status</returns>
        public static bool Downloadfile(string url, string path)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User_Agent", "Chrome");
                    webClient.DownloadFile(url, path);
                }

                return true;
            }
            catch (Exception ex)
            {
                LoggerError.Log(url + "\n" + path + "\n" + ex.Message);
                return false;
            }
        }
    }
}
