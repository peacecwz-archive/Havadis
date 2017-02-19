using Havadis.API.Crawler;
using Havadis.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havadis.API.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        [Route("api/hot")]
        public async Task<List<Models.News>> Hot()
        {
            return await HavadisCrawler.Instance.Get();
        }

        [HttpGet]
        [Route("api/news")]
        public async Task<List<Models.News>> News()
        {
            string json = await StorageHelper.GetFile($"{DateTime.Now.ToString("ddMMyyyy")}.json");
            if(string.IsNullOrEmpty(json))return await HavadisCrawler.Instance.Get();
            return JsonConvert.DeserializeObject<List<Models.News>>(json);
        }
    }
}
