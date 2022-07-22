using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients.SpotApi
{
    public class KunaClientSpotApiTrading
    {
        private readonly KunaClientSpotApi _baseClient;

        internal KunaClientSpotApiTrading(KunaClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
