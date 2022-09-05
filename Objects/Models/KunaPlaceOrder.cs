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
    public class KunaPlaceOrder : KunaOrder
    {
        [ArrayProperty(21)]
        public decimal? StopPrice { get; set; }
    }
}
