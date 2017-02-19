using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havadis.ViewModels
{
    public class NewsDetailViewModel : BaseViewModel
    {
        public NewsDetailViewModel(Models.News news)
        {
            Title = news.Title + " Haberi";
            this.NewsTitle = news.Title;
            this.HasVideo = news.HasVideo;
        }

        public string NewsTitle { get; set; }
        public bool HasVideo { get; set; }
    }
}
