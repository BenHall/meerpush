using System;
using System.IO;
using System.Net;

namespace MeerPush.tests
{
    public static class Helper
    {
        public static string GetSite(string uri)
        {
            var request = HttpWebRequest.Create(new Uri(uri));
            var response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }
}
