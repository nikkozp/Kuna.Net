using CryptoExchange.Net.Objects;
using Kuna.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients.SpotApi
{
    public class KunaClientSpotApiExchangeData
    {
        private readonly KunaClientSpotApi _baseClient;

        internal KunaClientSpotApiExchangeData(KunaClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Test Connectivity

        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternalAsync<object>(_baseClient.GetUrl("/v3/http_test"), HttpMethod.Post, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Check Server Time

        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<KunaCheckTime>(_baseClient.GetUrl("/v3/timestamp"), HttpMethod.Get, ct, ignoreRateLimit: true).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Currencies

        public WebCallResult<IEnumerable<KunaCurrency>> GetCurrencies(CancellationToken ct = default) => GetCurrenciesAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaCurrency>>(_baseClient.GetUrl($"/v3/currencies"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Markets

        public WebCallResult<IEnumerable<KunaMarket>> GetMarkets(CancellationToken ct = default) => GetMarketsAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaMarket>>> GetMarketsAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaMarket>>(_baseClient.GetUrl($"/v3/markets"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Tickers

        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(string symbol, CancellationToken ct = default) => GetTickersAsync(symbol, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(string symbol, CancellationToken ct = default) => await GetTickersAsync(new[] { symbol }, ct);

        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(IEnumerable<string> symbols, CancellationToken ct = default) => GetTickersAsync(symbols, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaTicker>>(_baseClient.GetUrl($"/v3/tickers?symbols={String.Join(',', symbols)}"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }

        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(CancellationToken ct = default) => GetTickersAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaTicker>>(_baseClient.GetUrl($"v3/tickers?symbols=ALL"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region OrderBook

        public WebCallResult<KunaOrderBook> GetOrderBook(string symbol, CancellationToken ct = default) => GetOrderBookAsync(symbol, ct).Result;
        public async Task<WebCallResult<KunaOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            //30.08.2022 Kuna Support: $"/v3/auth/book/{symbol}" or $"/v3/auth/pro/book/{symbol}" NOT WORKING!!!
            //var endpoint = _baseClient.Options.IsPro ? $"/v3/auth/book/{symbol}" : $"/v3/book/{symbol}";

            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaOrderBookEntry>>(_baseClient.GetUrl($"/v3/book/{symbol}"), HttpMethod.Get, ct).ConfigureAwait(false);

            KunaOrderBook data = null;

            if (result)
            {
                data = new KunaOrderBook()
                {
                    Asks = result.Data.Where(e => e.Quantity < 0).Select(e => { e.Quantity = Math.Abs(e.Quantity); return e; }).ToList(),
                    Bids = result.Data.Where(e => e.Quantity > 0).ToList(),
                };
            }

            return new WebCallResult<KunaOrderBook>(result.ResponseStatusCode, 
                                                    result.ResponseHeaders,
                                                    result.ResponseTime,
                                                    result.OriginalData,
                                                    result.RequestUrl,
                                                    result.RequestBody,
                                                    result.RequestMethod,
                                                    result.RequestHeaders,
                                                    data,
                                                    result.Error);
        }

        #endregion
    }
}
