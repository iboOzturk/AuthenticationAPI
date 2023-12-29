using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AuthenticationAPI.APIKey.Helpers
{
	public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
	{
		private const string ApiKeyName = "ApiKey";
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyService apiKeyService)
                                : base(options, logger, encoder, clock)
        {
            _apiKeyService = apiKeyService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (!Request.Headers.TryGetValue(ApiKeyName, out var apiKeyHeaderValues))
			{
				Logger.LogError("ApiKey header not found");
				return AuthenticateResult.Fail("ApiKey header not found");
			}

			var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
            //--HardCoded ApiKey Saklama--//
            //if (string.IsNullOrEmpty(providedApiKey) || !IsValidApiKey(providedApiKey))

			//--Api İsteği ile Saklama--//
            if (string.IsNullOrEmpty(providedApiKey) || !_apiKeyService.IsApiKeyValidAsync(providedApiKey).Result)
			{
				Logger.LogError("Invalid API key");
				return AuthenticateResult.Fail("Invalid API key");
			}

			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, providedApiKey)           
        };

			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}

		//--HardCoded ApiKey Saklama--//
		//private bool IsValidApiKey(string apiKey)
		//{
		//	return apiKey.Equals("Value", StringComparison.OrdinalIgnoreCase);
		//}
	}
}
