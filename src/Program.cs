using System;
using System.Net;
using System.Net.Http;
using System.Text;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Threading;
using System.Diagnostics;

namespace ILikeVocabulary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("请输入要获取的单词数量:");


            int wordNumber = Convert.ToInt32(Console.ReadLine());

            string result = string.Empty;

            string[] arr = new string[wordNumber];

            Stopwatch watchAll = new Stopwatch();
            watchAll.Start();
            Console.WriteLine($"开始获取单词, 总计 {wordNumber} 个...");
            Parallel.For(0, wordNumber, (index) =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                int wordIndex = index + 1;
                try
                {
                    arr[index] = Common.GetWords(wordIndex);
                    Console.WriteLine($"第 {wordIndex} 个单词获取完成, 花费时间 {watch.ElapsedMilliseconds} ms...");

                }
                catch
                {
                    // arr[index] = string.Empty;
                    Console.WriteLine($"第 {wordIndex} 个单词获取异常, 花费时间 {watch.ElapsedMilliseconds} ms...");
                }
                watch.Stop();
                Thread.Sleep(100);
            });
            foreach (string res in arr)
            {
                if (!string.IsNullOrEmpty(res))
                {
                    result += res;
                }
            }

            watchAll.Stop();
            Console.WriteLine($"获取完成, 总共花费 {watchAll.ElapsedMilliseconds} ms");
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
