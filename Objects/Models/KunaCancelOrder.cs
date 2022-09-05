using CryptoExchange.Net.Converters;
using Kuna.Net.Converters;
using Kuna.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaCancelOrder
    {
        [JsonProperty("id")]
        public long OrderId { get; set; }

        public string Symbol { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedTime { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("avg_execution_price")]
        public decimal AvgPrice { get; set; }

        [JsonProperty("state"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }

        [JsonProperty("original_amount")]
        public decimal Quantity { get; set; }

        [JsonProperty("executed_amount")]
        public decimal QuantityFilled { get; set; }
    }
}
