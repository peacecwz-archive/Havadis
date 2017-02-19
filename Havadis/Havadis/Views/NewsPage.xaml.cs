using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Havadis.Views
{
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.NewsViewModel();
            ToolbarItems.Add(new ToolbarItem()
            {
                Text = "+"
            });
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            Navigation.PushAsync(new Views.NewsDetailPage(e.Item as Models.News), true);
            ((ListView)sender).SelectedItem = null;
        }
    }
}
