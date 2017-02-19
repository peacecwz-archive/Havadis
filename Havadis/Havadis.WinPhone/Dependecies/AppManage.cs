using Havadis.Dependecies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Havadis.WinPhone.Dependecies
{
    public class AppManage : IAppManage
    {
        public void Close()
        {
            Application.Current.Exit();
        }
    }
}
