using System;
using System.Net;
using System.Net.Http;

namespace ILikeVocabulary
{
    public class YoudictClient
    {
        public HttpClient Client { get; private set; }

        public YoudictClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://www.youdict.com/");
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            Client = httpClient;
        }
    }
}