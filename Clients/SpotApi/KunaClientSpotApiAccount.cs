using CryptoExchange.Net.Objects;
using Kuna.Net.Enums;
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

        #region Account Info

        public WebCallResult<KunaAccountInfo> GetAccountInfo(CancellationToken ct = default) => GetAccountInfoAsync(ct).Result;
        public async Task<WebCallResult<KunaAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {

            var result = await _baseClient.SendRequestInternalAsync<KunaAccountInfo>(_baseClient.GetUrl("/v3/auth/me"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Wallet

        public WebCallResult<IEnumerable<KunaAssetBalance>> GetBalance(CancellationToken ct = default) => GetBalanceAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaAssetBalance>>> GetBalanceAsync(CancellationToken ct = default)
        {
            var endpoint = _baseClient.Options.IsPro ? "/v3/auth/pro/r/wallets" : "/v3/auth/r/wallets";

            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaAssetBalance>>(_baseClient.GetUrl(endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Subscription Plans

        public WebCallResult<IEnumerable<KunaSubscriptionPlan>> GetSubscriptionPlans(CancellationToken ct = default) => GetSubscriptionPlanstAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<KunaSubscriptionPlan>>> GetSubscriptionPlanstAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<IEnumerable<KunaSubscriptionPlan>>(_baseClient.GetUrl("/v3/auth/subscriptions/list"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

            if (result)
            {
                var findActive = result.Data.FirstOrDefault(e => e.Status == PlanStatus.Active);

                if (findActive != null)
                    _baseClient.Options.IsPro = true;
                else
                    _baseClient.Options.IsPro = false;
            }

            return result;
        }

        #endregion
    }
}
