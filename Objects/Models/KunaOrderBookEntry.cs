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
    public class KunaOrderBookEntry
    {
        [ArrayProperty(0)]
        public decimal Price { get; set; }

        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

        public decimal QuoteQuantity => Price * Quantity;

        [ArrayProperty(2)]
        public long Count { get; set; }
    }
}
