using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kuna.Net.Converters;
using Kuna.Net.Enums;
using Kuna.Net.Objects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients.SpotApi
{
    public class KunaClientSpotApiTrading
    {
        private readonly KunaClientSpotApi _baseClient;

        internal KunaClientSpotApiTrading(KunaClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Open Orders

        public WebCallResult<IEnumerable<KunaOrder>> OpenOrders(CancellationToken ct = default) => OpenOrdersAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaOrder>>> OpenOrdersAsync(CancellationToken ct = default)
        {
            var endpoint = _baseClient.Options.IsPro ? $"/v3/auth/pro/r/orders" : $"/v3/auth/r/orders";

            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaOrder>>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            if (result)
            {
                foreach(var order in result.Data)
                {
                    var quantity = order.Quantity;
                    var quantityNotFilled = order.QuantityNotFilled;

                    order.QuantityNotFilled = quantity;
                    order.Quantity = quantityNotFilled;
                }
            }

            return result;
        }


        public WebCallResult<IEnumerable<KunaOrder>> OpenOrders(string symbol, CancellationToken ct = default) => OpenOrdersAsync(symbol, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaOrder>>> OpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var endpoint = _baseClient.Options.IsPro ? $"/v3/auth/pro/r/orders/{symbol}" : $"/v3/auth/r/orders/{symbol}";

            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaOrder>>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Place Order

        public WebCallResult<KunaPlaceOrder> PlaceOrder(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, decimal? stopPrice = null, CancellationToken ct = default) => PlaceOrderAsync(symbol, side, type, quantity, price, stopPrice, ct).Result;
        public async Task<WebCallResult<KunaPlaceOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, decimal? stopPrice = null, CancellationToken ct = default)
        {
            quantity = side == OrderSide.Buy ? quantity : quantity * -1;
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stop_price", stopPrice?.ToString(CultureInfo.InvariantCulture));

            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/w/order/submit" : "/v3/auth/w/order/submit";

            var result = await _baseClient.SendRequestInternalAsync<KunaPlaceOrder>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Query Order

        public WebCallResult<KunaOrder> GetOrder(long orderId, CancellationToken ct = default) => GetOrderAsync(orderId, ct).Result;
        public async Task<WebCallResult<KunaOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", orderId }
            };

            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/r/orders/details" : "/v3/auth/r/orders/details";

            var result = await _baseClient.SendRequestInternalAsync<KunaOrder>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Trades

        public WebCallResult<IEnumerable<KunaTrade>> GetTrades(string symbol, long orderId, CancellationToken ct = default) => GetTradesAsync(symbol, orderId, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTrade>>> GetTradesAsync(string symbol, long orderId, CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaTrade>>(_baseClient.GetUrl($"/v3/auth/r/order/{symbol}:{orderId}/trades"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Cancel Order

        public WebCallResult<KunaCancelOrder> CancelOrder(long orderId, CancellationToken ct = default) => CancelOrderAsync(orderId, ct).Result;
        public async Task<WebCallResult<KunaCancelOrder>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "order_id", orderId }
            };

            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/order/cancel" : "/v3/order/cancel";

            var result = await _baseClient.SendRequestInternalAsync<KunaCancelOrder>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Cancel Multiple Orders

        public WebCallResult<KunaCancelMultipleOrder> CancelMultipleOrders(IEnumerable<long> orderIds, CancellationToken ct = default) => CancelMultipleOrdersAsync(orderIds, ct).Result;
        public async Task<WebCallResult<KunaCancelMultipleOrder>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "order_ids", orderIds }
            };

            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/order/cancel/multi" : "/v3/order/cancel/multi";

            var result = await _baseClient.SendRequestInternalAsync<KunaCancelMultipleOrder>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
