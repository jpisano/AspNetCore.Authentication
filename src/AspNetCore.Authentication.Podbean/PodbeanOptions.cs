using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Security.Claims;

namespace AspNetCore.Authentication.Podbean
{
	/// <summary>
	/// Configuration options for <see cref="PodbeanHandler"/>.
	/// </summary>
	public class PodbeanOptions : OAuthOptions
	{
		/// <summary>
		/// Initializes a new <see cref="PodbeanOptions"/>.
		/// </summary>
		public PodbeanOptions()
		{
			CallbackPath = new PathString("/signin-podbean");
			AuthorizationEndpoint = PodbeanDefaults.AuthorizationEndpoint;
			TokenEndpoint = PodbeanDefaults.TokenEndpoint;
			UserInformationEndpoint = PodbeanDefaults.UserInformationEndpoint;
			Scope.Add("podcast_read");
			Scope.Add("episode_read");

			ClaimActions.MapJsonSubKey(ClaimTypes.NameIdentifier, "podcast", "id");
			ClaimActions.MapJsonSubKey(ClaimTypes.Name, "podcast", "title");
			ClaimActions.MapJsonSubKey("urn:podbean:logo", "podcast", "logo");
			ClaimActions.MapJsonSubKey(ClaimTypes.Webpage, "podcast", "link");
		}

		/// <summary>
		/// Check that the options are valid.  Should throw an exception if things are not ok.
		/// </summary>
		public override void Validate()
		{
			if (string.IsNullOrEmpty(ClientId))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(ClientId)), nameof(ClientId));
			}

			if (string.IsNullOrEmpty(ClientSecret))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(ClientSecret)), nameof(ClientSecret));
			}

			base.Validate();
		}
	}
}
