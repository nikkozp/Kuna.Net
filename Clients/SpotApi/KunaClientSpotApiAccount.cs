using CryptoExchange.Net.Objects;
using Kuna.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients.SpotApi
{
    public class KunaClientSpotApiAccount
    {
        private readonly KunaClientSpotApi _baseClient;

        internal KunaClientSpotApiAccount(KunaClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        public WebCallResult<IEnumerable<KunaBalance>> GetBalance(CancellationToken ct = default) => GetBalanceAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaBalance>>> GetBalanceAsync(CancellationToken ct = default)
        {
            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/r/wallets" : "/v3/auth/r/wallets";

            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaBalance>>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct).ConfigureAwait(false);

            return result;
        }
    }
}
