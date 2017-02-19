using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
//using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http;

namespace Havadis.Helpers
{
    public class DataHelper
    {
        public static DataHelper Instance
        {
            get
            {
                return new DataHelper();
            }
        }

        public async Task<List<Models.News>> Get()
        {
            using (HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(App.ApiUrl)
            })
            {
                var response = await client.GetAsync("/api/news");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Models.News>>(await response.Content?.ReadAsStringAsync());
            }
            return new List<Models.News>();
        }
    }
}