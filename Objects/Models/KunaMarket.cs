using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaMarket
    {
        [JsonProperty("id")]
        public string Symbol { get; set; }

        [JsonProperty("base_unit")]
        public string BaseSymbol { get; set; }

        [JsonProperty("quote_unit")]
        public string QuoteSymbol { get; set; }

        [JsonProperty("base_precision")]
        public long BasePrecision { get; set; }

        [JsonProperty("quote_precision")]
        public long QuotePecision { get; set; }

        [JsonProperty("display_precision")]
        public long DisplayPrecision { get; set; }

        [JsonProperty("price_change")]
        public decimal PriceChange { get; set; }
    }
}
