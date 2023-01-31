using System.Net;

namespace MallMartLogic
{
    public class HTTPGet
    {
        public string Url { get; set; }

        public HTTPGet(string url)
        {
            Url = string.Format(url);
        }

        public string GetJsonFromApi()
        {
            WebRequest requestGet = WebRequest.Create(Url);
            requestGet.Method = "GET";

            HttpWebResponse responseGet = (HttpWebResponse)requestGet.GetResponse();

            string result = null;
            using (Stream stream = responseGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

    }
}