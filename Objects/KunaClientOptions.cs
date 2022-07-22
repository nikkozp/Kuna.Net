using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects
{
    public class KunaClientOptions : BaseRestClientOptions
    {
        public static KunaClientOptions Default { get; set; } = new KunaClientOptions();

        public INonceProvider? NonceProvider { get; set; }

        public bool IsPro { get; set; } = false;

        private RestApiClientOptions _spotApiOptions = new RestApiClientOptions(KunaApiAddresses.Default.RestClientAddress)
        {
            //RateLimiters = new List<IRateLimiter>
            //{
            //        new RateLimiter()
            //            .AddApiKeyLimit(15, TimeSpan.FromSeconds(45), false, false)
            //            .AddEndpointLimit(new [] { "/private/AddOrder", "/private/CancelOrder", "/private/CancelAll", "/private/CancelAllOrdersAfter" }, 60, TimeSpan.FromSeconds(60), null, true),
            //}
        };

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions = new RestApiClientOptions(_spotApiOptions, value);
        }

        public KunaClientOptions() : this(Default)
        {
        }

        internal KunaClientOptions(KunaClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            NonceProvider = baseOn.NonceProvider;
            _spotApiOptions = new RestApiClientOptions(baseOn.SpotApiOptions, null);
        }
    }
}
