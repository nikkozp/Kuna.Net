using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    [JsonConverter(typeof(ArrayConverter))]
    public class KunaAssetBalance
    {
        [ArrayProperty(1)]
        public string Asset { get; set; }

        [ArrayProperty(2)]
        public decimal Total { get; set; }

        [ArrayProperty(4)]
        public decimal Free { get; set; }
    }
}
