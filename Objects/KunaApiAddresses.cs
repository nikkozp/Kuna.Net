using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects
{
    public class KunaApiAddresses
    {
        public string RestClientAddress { get; set; } = "";


        public static KunaApiAddresses Default = new KunaApiAddresses
        {
            RestClientAddress = "https://api.kuna.io",
        };
    }
}
