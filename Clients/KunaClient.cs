using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kuna.Net.Clients.SpotApi;
using Kuna.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Clients
{
    public class KunaClient : BaseRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public KunaClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of KunaClient using the default options
        /// </summary>
        public KunaClient() : this(KunaClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of KunaClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public KunaClient(KunaClientOptions options) : base("Kuna", options)
        {
            SpotApi = AddApiClient(new KunaClientSpotApi(log, this, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KunaClientOptions options)
        {
            KunaClientOptions.Default = options;
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            try
            {
                if (!error.HasValues)
                    return new ServerError("");

                if (error["messages"] != null)
                    return new ServerError(0, (string)error["messages"][0]);

                if (error["error"] != null && error["error"]["message"] == null)
                    return new ServerError((int)error["code"], (string)error["error"]);

                if (error["error"] != null)
                    return new ServerError((int)error["error"]["code"], (string)error["error"]["message"]);

                return new ServerError(0, (string)error["messages"]);
            }
            catch
            {
                return new ServerError(0, JsonConvert.SerializeObject(error));
            }
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            return base.SendRequestAsync<T>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit);
        }
    }
}
