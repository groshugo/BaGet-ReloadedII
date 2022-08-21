using System;
using System.Threading;
using Microsoft.Extensions.Options;

namespace BaGet.Core
{
    public class ApiKeyAuthenticationService : IAuthenticationService
    {
        private readonly string _apiKey;

        public ApiKeyAuthenticationService(IOptionsSnapshot<BaGetOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _apiKey = string.IsNullOrEmpty(options.Value.MasterKey) ? null : options.Value.MasterKey;
        }

        public bool Authenticate(string apiKey, string packageKey, CancellationToken token) => Authenticate(apiKey, packageKey);

        public bool Authenticate(string apiKey, string packageKey)
        {
            // Check Master Key
            if (!string.IsNullOrEmpty(_apiKey) && _apiKey == apiKey)
                return true;

            // Check Optional Package Key
            if (!string.IsNullOrEmpty(packageKey))
                return apiKey == packageKey;

            // Authenticate if neither is set.
            return true;
        }
    }
}
