using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Kairos.Core.Business.App
{
    public interface IAppCarrier
    {
        Application CurrentApp { get; set; }
    }
}
