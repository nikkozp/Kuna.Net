using CryptoExchange.Net.Converters;
using Kuna.Net.Enums;

namespace Kuna.Net.Converters
{
    public class OrderStatusConverter : BaseConverter<OrderStatus>
    {
        public OrderStatusConverter() : this(true) { }
        public OrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderStatus, string>> Mapping => new List<KeyValuePair<OrderStatus, string>>
        {
            //new KeyValuePair<OrderStatus, string>(OrderStatus.Pending, "WAIT"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.New, "ACTIVE"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.PartiallyFilled, "EXECUTED"),
            //new KeyValuePair<OrderStatus, string>(OrderStatus.Canceling, "CANCEL"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "WAIT"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "CANCEL"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "CANCELED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Filled, "DONE")
        };
    }
}
