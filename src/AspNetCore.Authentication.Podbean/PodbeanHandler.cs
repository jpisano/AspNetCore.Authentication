using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AspNetCore.Authentication.Podbean
{

	public class PodbeanHandler : OAuthHandler<PodbeanOptions>
	{
		public PodbeanHandler(IOptionsMonitor<PodbeanOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{ }

		protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
		{
			var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, "access_token", tokens.AccessToken);

			var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_PodcastRetrievalFailure, response.StatusCode));
			}

			var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

			var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
			context.RunClaimActions();

			await Events.CreatingTicket(context);

			return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
		}
	}
}