using Havadis.Dependecies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Havadis.iOS.Dependecies
{
    public class AppManage : IAppManage
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
