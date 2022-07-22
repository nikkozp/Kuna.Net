using CryptoExchange.Net.Objects;
using Kuna.Net.Objects.Models;
using System;
using System.Collections.Generic;
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

        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(string symbol, CancellationToken ct = default) => GetTickersAsync(symbol, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(string symbol, CancellationToken ct = default) => await GetTickersAsync(new[] { symbol }, ct);

        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(IEnumerable<string> symbols, CancellationToken ct = default) => GetTickersAsync(symbols, ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaTicker>>(_baseClient.GetUrl($"v3/tickers?symbols={String.Join(',', symbols)}"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }


        public WebCallResult<IEnumerable<KunaTicker>> GetTickers(CancellationToken ct = default) => GetTickersAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaTicker>>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaTicker>>(_baseClient.GetUrl($"v3/tickers?symbols=ALL"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }
    }
}
