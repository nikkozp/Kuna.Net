using CryptoExchange.Net.Converters;
using Kuna.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaTrade
    {
        [ArrayProperty(0)]
        public long TradeId { get; set; }

        [ArrayProperty(1)]
        public string Symbol { get; set; }

        [ArrayProperty(2)]
        public DateTime TradeTime { get; set; }

        [ArrayProperty(3)]
        public long OrderId { get; set; }

        [ArrayProperty(4)]
        public decimal Quantity { get; set; }

        [ArrayProperty(5)]
        public decimal Price { get; set; }

        [ArrayProperty(8), JsonConverter(typeof(MakerConverter))]
        public bool IsMaker { get; set; }

        [ArrayProperty(9)]
        public decimal CommissionFee { get; set; }

        [ArrayProperty(10)]
        public string CommissionAsset { get; set; }
    }
}
