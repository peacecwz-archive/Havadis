using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Havadis
{
    public static class MessageHelper
    {
        public static async void ShowMessage(string message)
        {
            await new ContentPage().DisplayAlert(App.AppName, message, "Tamam");
        }

        public static async Task<bool> YesNoMessage(string message)
        {
            return await new ContentPage().DisplayAlert(App.AppName, message, "Evet", "Hayır");
        }

    }
}
