using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaTicker
    {
        [ArrayProperty(0)]
        public string Symbol { get; set; }

        [ArrayProperty(1)]
        public decimal BidPrice { get; set; }

        [ArrayProperty(2)]
        public decimal BidQuoantity { get; set; }

        [ArrayProperty(3)]
        public decimal AskPrice { get; set; }

        [ArrayProperty(4)]
        public decimal AskQuantity { get; set; }

        [ArrayProperty(5)]
        public decimal PriceChange { get; set; }

        [ArrayProperty(6)]
        public decimal PriceChangePercentage { get; set; }

        [ArrayProperty(7)]
        public decimal LastPrice { get; set; }

        [ArrayProperty(8)]
        public decimal TotalTradedBaseAssetVolume { get; set; }

        [ArrayProperty(9)]
        public decimal HighPrice { get; set; }

        [ArrayProperty(10)]
        public decimal LowPice { get; set; }
    }
}
