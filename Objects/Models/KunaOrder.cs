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
    [JsonConverter(typeof(ArrayConverter))]
    public class KunaOrder
    {
        [ArrayProperty(0)]
        public long OrderId { get; set; }

        [ArrayProperty(3)]
        public string Symbol { get; set; }

        [ArrayProperty(4), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }

        [ArrayProperty(5), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedTime { get; set; }

        private decimal quantityNotFilled;
        [ArrayProperty(6)]
        public decimal QuantityNotFilled
        {
            get { return Math.Abs(quantityNotFilled); }
            set {  quantityNotFilled = value; }
        }

        public decimal QuantityFilled => Quantity - QuantityNotFilled;

        private decimal quantity;
        [ArrayProperty(7)]
        public decimal Quantity
        {
            get { return Math.Abs(quantity); }
            set { quantity = value; }
        }

        private OrderSide? side;
        public OrderSide Side
        {
            get 
            { 
                if (side == null)
                    return (quantityNotFilled < 0 || quantity < 0) ? OrderSide.Sell : OrderSide.Buy;
                else
                    return side!.Value;
            }
            set
            {
                side = value;
            }
        }

        [ArrayProperty(10), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        private OrderStatus status;
        [ArrayProperty(13), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status
        {
            get { return QuantityNotFilled == 0 ? OrderStatus.Filled : status; }
            set {  status = value; }
        }

        [ArrayProperty(16)]
        public decimal Price { get; set; }

        [ArrayProperty(17)]
        public decimal AvgPrice { get; set; }
    }
}
