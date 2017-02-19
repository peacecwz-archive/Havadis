using Havadis.ViewModels;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Havadis.Views
{
    public partial class NewsDetailPage : ContentPage
    {
        NewsDetailViewModel viewModel;
        public NewsDetailPage(Models.News news)
        {
            InitializeComponent();
            viewModel = new ViewModels.NewsDetailViewModel(news);
            this.BindingContext = viewModel;
            web.Source = new HtmlWebViewSource()
            {
                Html = news.HtmlContent
            };
        }

        private void ShareButton_Clicked(object sender, EventArgs e)
        {
            CrossShare.Current
                  .Share(new Plugin.Share.Abstractions.ShareMessage()
                  {
                      Text = viewModel.NewsTitle + " via @PeaceCwz #Havadis",
                      Title = "Havadis",
                      Url = "http://havadis.co"
                  });
        }
    }
}
