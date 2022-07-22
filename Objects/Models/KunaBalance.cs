using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaBalance
    {
        [ArrayProperty(1)]
        public string Asset { get; set; }

        [ArrayProperty(2)]
        public decimal Total { get; set; }

        [ArrayProperty(4)]
        public decimal Free { get; set; }
    }
}
