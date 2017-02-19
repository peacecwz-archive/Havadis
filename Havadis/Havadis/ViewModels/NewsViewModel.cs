using Havadis.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Havadis.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel()
        {
            Title = "Havadis Günün Haberleri";
            LoadNews();
        }

        private ObservableCollection<Models.News> news = new ObservableCollection<Models.News>();

        public ObservableCollection<Models.News> News
        {
            get { return news; }
            set { news = value; }
        }

        private Command loadItemsCommand;
        public Command LoadItemsCommand
        {
            get { return loadItemsCommand ?? (loadItemsCommand = new Command(() => LoadNews())); }
        }

        private async void LoadNews()
        {
            if (IsBusy) return;

            IsBusy = true;
            List<Models.News> newsItemList = new List<Models.News>();
            try
            {
                newsItemList = await DataHelper.Instance.Get();
            }
            catch (Exception ex)
            {
                if (await MessageHelper.YesNoMessage(ex.Message))
                {
                    var appManage = DependencyService.Get<Dependecies.IAppManage>();
                    if (appManage != null) appManage.Close();
                }
            }
            News.Clear();
            foreach (Models.News newsItem in newsItemList)
                News.Add(newsItem);

            IsBusy = false;
        }
        
    }
}
