using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Kairos.DataAccess.AppCarrier
{
    public class AppCarrier : Kairos.Core.Business.App.IAppCarrier
    {
        public Application CurrentApp { get; set; }

        public AppCarrier()
        {

        }
    }
}
