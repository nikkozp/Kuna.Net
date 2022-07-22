using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Kuna.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients.SpotApi
{
    public class KunaClientSpotApi : RestApiClient
    {
        private readonly Log _log;
        private readonly KunaClient _baseClient;

        #region fields

        internal KunaClientOptions Options { get; }


        #endregion

        #region Api clients

        /// <inheritdoc />
        public KunaClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public KunaClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public KunaClientSpotApiTrading Trading { get; }

        /// <inheritdoc />
        public string ExchangeName => "Kuna";

        #endregion

        #region ctor

        internal KunaClientSpotApi(Log log, KunaClient baseClient, KunaClientOptions options)
            : base(options, options.SpotApiOptions)
        {
            _log = log;
            Options = options;
            _baseClient = baseClient;

            Account = new KunaClientSpotApiAccount(this);
            ExchangeData = new KunaClientSpotApiExchangeData(this);
            Trading = new KunaClientSpotApiTrading(this);

            requestBodyFormat = RequestBodyFormat.FormData;
        }

        #endregion

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KunaAuthenticationProvider(credentials, Options.NonceProvider ?? new KunaNonceProvider());

        internal Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress}{endpoint}");
        }

        internal async Task<WebCallResult<T>> SendRequestInternalAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight, ignoreRateLimit: ignoreRateLimit).ConfigureAwait(false);
            //if (!result && result.Error!.Code == -1021 && Options.SpotApiOptions.AutoTimestamp)
            //{
            //    _log.Write(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
            //    TimeSyncState.LastSyncTime = DateTime.MinValue;
            //}
            return result;
        }

        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.SpotApiOptions.AutoTimestamp, Options.SpotApiOptions.TimestampRecalculationInterval, new TimeSyncState("BtcTrade Api") { LastSyncTime = DateTime.UtcNow });

        public override TimeSpan GetTimeOffset() => TimeSpan.Zero;


        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => Task.FromResult(new WebCallResult<DateTime>(null, null, null, null, null, null, null, null, DateTime.UtcNow, null));
    }
}
