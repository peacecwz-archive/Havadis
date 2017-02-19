using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Havadis.API.Crawler
{
    public class HavadisCrawler
    {
        public static HavadisCrawler Instance
        {
            get
            {
                return new HavadisCrawler();
            }
        }

        public string BaseUrl { get; set; } = "http://havadis.co/";
        public async Task<List<Models.News>> Get()
        {
            using (HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            })
            {
                List<Models.News> newsList = new List<Models.News>();
                var response = await client.GetAsync("/");
                string html = await response.Content?.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNode dateNode = doc.DocumentNode.SelectSingleNode("//h1[@class='entry-title']");
                string date = dateNode.InnerText.Replace("Günlük Haberler", "").Trim();
                DateTime createdDate = ConvertStringToDate(date);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='et_pb_text et_pb_module et_pb_bg_layout_light et_pb_text_align_left  et_pb_text_0']//p");
                int i = 0;
                Models.News news = new Models.News();
                while (i < nodes.Count)
                {
                    HtmlNode node = nodes[i];
                    if (node.Descendants("b").Count() == 1)
                    {
                        news.Title = WebUtility.HtmlDecode(node.Descendants("b").FirstOrDefault().InnerText);
                    }
                    else if (node.Descendants("a").Count() == 1 && node.Descendants("a").First().GetAttributeValue("href", "").Contains("youtube.com") && node.Descendants("a").First().InnerText.ToLower().Contains("haberin videosu"))
                    {
                        news.HasVideo = true;
                        news.VideoUrl = WebUtility.HtmlDecode(node.Descendants("a").FirstOrDefault().GetAttributeValue("href", ""));
                    }
                    else if (string.IsNullOrEmpty(node.InnerHtml.Replace("&nbsp;", "")))
                    {
                        news.CreatedDate = createdDate;
                        newsList.Add(news);
                        news = new Models.News();
                    }
                    else
                    {
                        news.HtmlContent += node.InnerHtml;
                        news.TextContent += WebUtility.HtmlDecode(node.InnerText);
                    }
                    i++;
                }

                return newsList;
            }
        }

        private DateTime ConvertStringToDate(string date)
        {
            string[] param = date.Split(' ');
            int day, month, year;
            day = int.Parse(param[0]);
            month = GetMonth(param[1]);
            year = int.Parse(param[2]);
            return new DateTime(year, month, day);
        }

        private int GetMonth(string month)
        {
            switch (month.ToLower())
            {
                case "ocak":
                    return 1;
                case "şubat":
                    return 2;
                case "mart":
                    return 3;
                case "nisan":
                    return 4;
                case "mayıs":
                    return 5;
                case "haziran":
                    return 6;
                case "temmuz":
                    return 7;
                case "ağustos":
                    return 8;
                case "eylül":
                    return 9;
                case "ekim":
                    return 10;
                case "kasım":
                    return 11;
                case "aralık":
                    return 12;
                default:
                    return 1;
            }
        }
    }
}
