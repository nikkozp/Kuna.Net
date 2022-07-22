using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kuna.Net.Objects;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Kuna.Net
{
    public class KunaAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA384 _encryptor384;
        private readonly INonceProvider _nonceProvider;

        public KunaAuthenticationProvider(ApiCredentials credentials, INonceProvider? nonceProvider) : base(credentials)
        {
            _encryptor384 = new HMACSHA384(Encoding.UTF8.GetBytes(Credentials.Secret.GetString()));

            _nonceProvider = nonceProvider ?? new KunaNonceProvider();
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;

            var nonce = _nonceProvider.GetNonce();

            headers.Add("Kun-Nonce", nonce.ToString());
            headers.Add("Kun-ApiKey", Credentials.Key.GetString());

            string path = uri.PathAndQuery.ToString();

            string signature = BytesToHexString(_encryptor384.ComputeHash(Encoding.UTF8.GetBytes($"{path}{headers["Kun-Nonce"]}{JsonConvert.SerializeObject(parameters)}"))).ToLower();

            headers.Add("Kun-Signature", signature);
        }
    }
}