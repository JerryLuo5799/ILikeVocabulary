﻿using System;
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
using Microsoft.Extensions.DependencyInjection;

namespace ILikeVocabulary
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            IServiceCollection services = new ServiceCollection();


            Configure(services);

            var service = services.BuildServiceProvider();

            // Console.WriteLine("Creating a client...");
            //var YoudictClient = service.GetRequiredService<YoudictClient>();
            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var memcachedClient = serviceProvider.GetService<IHttpClientFactory>();

            Console.WriteLine("请输入要获取的单词数量:");


            int wordNumber = Convert.ToInt32(Console.ReadLine());

            string result = string.Empty;

            string[] arr = new string[wordNumber];

            Stopwatch watchAll = new Stopwatch();
            watchAll.Start();
            Console.WriteLine($"开始获取单词, 总计 {wordNumber} 个...");
            for (int index = 0; index < arr.Length; index++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                int wordIndex = index + 1;
                try
                {
                    arr[index] = Common.GetWords(wordIndex, memcachedClient.CreateClient());
                    Console.WriteLine($"第 {wordIndex} 个单词获取完成, 花费时间 {watch.ElapsedMilliseconds} ms...");

                }
                catch(Exception e)
                {
                    // arr[index] = string.Empty;
                    Console.WriteLine($"第 {wordIndex} 个单词获取异常, 异常内容 {e}, 花费时间 {watch.ElapsedMilliseconds} ms...");
                }
                watch.Stop();
                Thread.Sleep(100);
            }
            // Parallel.For(0, wordNumber, (index) =>
            // {
            //     Stopwatch watch = new Stopwatch();
            //     watch.Start();

            //     int wordIndex = index + 1;
            //     try
            //     {
            //         arr[index] = Common.GetWords(wordIndex, memcachedClient.CreateClient());
            //         Console.WriteLine($"第 {wordIndex} 个单词获取完成, 花费时间 {watch.ElapsedMilliseconds} ms...");

            //     }
            //     catch
            //     {
            //         // arr[index] = string.Empty;
            //         Console.WriteLine($"第 {wordIndex} 个单词获取异常, 花费时间 {watch.ElapsedMilliseconds} ms...");
            //     }
            //     watch.Stop();
            //     Thread.Sleep(10);
            // });
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

        public static void Configure(IServiceCollection services)
        {
            // services.AddHttpClient<YoudictClient>();
            services.AddHttpClient();
        }

    }
}
