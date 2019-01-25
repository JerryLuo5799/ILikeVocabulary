using System;
using System.Net;
using System.Net.Http;
using System.Text;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace ILikeVocabulary
{
    public class Common
    {
        private static HttpClient _httpClient;

        public static HttpClient GetHttpClient()
        {
            if (null == _httpClient)
            {
                _httpClient = new HttpClient() { BaseAddress = new Uri("https://www.youdict.com/") };
                _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            }

            return _httpClient;
        }
        public static string GetWords(int index)
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("https://www.youdict.com/").Result;
                response.EnsureSuccessStatusCode();
                string resultStr = response.Content.ReadAsStringAsync().Result;

                var parser = new HtmlParser();
                var document = parser.ParseDocument(resultStr);

                var word = document.QuerySelectorAll("h3#yd-word").FirstOrDefault();
                var pron = document.QuerySelectorAll("div#yd-word-pron").FirstOrDefault();
                var wordMean = document.QuerySelectorAll("div#yd-word-meaning ul li");
                string strMean = string.Empty;
                foreach (var str in wordMean)
                {
                    strMean += $"{str.TextContent} ";
                }
                var wordSentens = document.QuerySelectorAll("div#yd-liju dl dt").FirstOrDefault();

                result = $"{index}.  {GetResult(word)}";
                result += $"    {GetResult(pron)}";
                result += $"    {strMean}\r\n";
                result += $"    {GetResult(wordSentens)}";
                result += $"\r\n";
            }
            return result;
        }

        static string GetResult(IElement dom)
        {
            string result = string.Empty;
            if (null != dom)
            {
                result = dom.TextContent;
                result = result.Replace("\n", " ");
                result = result.Replace("1. ", "");
                result += "\r\n";
            }
            return result;
        }
    }
}