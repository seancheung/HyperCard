using System.IO;
using System.Net;
using MODEL;

namespace ACCESSOR
{
    public class Request
    {
        private Request() { }

        /// <summary>
        /// Get Data from the provided url
        /// </summary>
        /// <param name="url">A url to create request</param>
        /// <returns>Data from the response</returns>
        public static string GetWebData(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.AllowAutoRedirect = false;
            HttpWebResponse httpWebResponse = null;
            string data = string.Empty;

            int tryCount = 10;

            while (tryCount > 0)
            {
                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    if (!httpWebResponse.StatusDescription.Equals("Found"))
                    {
                        using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            data = streamReader.ReadToEnd();
                        }
                    }

                    break;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        tryCount--;
                    }
                    else
                    {
                        httpWebResponse = (HttpWebResponse)ex.Response;
                        LoggerError.Log(ex.Message);
                        break;
                    }
                }
            }

            

            httpWebResponse.Close();

            return data;

        }

    }
}
