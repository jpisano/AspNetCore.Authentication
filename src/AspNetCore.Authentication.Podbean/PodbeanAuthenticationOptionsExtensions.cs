using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCore.Authentication.Podbean
{
	public static class PodbeanAuthenticationOptionsExtensions
	{
		public static AuthenticationBuilder AddPodbean(this AuthenticationBuilder builder)
			=> builder.AddPodbean(PodbeanDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddPodbean(this AuthenticationBuilder builder, Action<PodbeanOptions> configureOptions)
			=> builder.AddPodbean(PodbeanDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddPodbean(this AuthenticationBuilder builder, string authenticationScheme, Action<PodbeanOptions> configureOptions)
			=> builder.AddPodbean(authenticationScheme, PodbeanDefaults.DisplayName, configureOptions);

		public static AuthenticationBuilder AddPodbean(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<PodbeanOptions> configureOptions)
			=> builder.AddOAuth<PodbeanOptions, PodbeanHandler>(authenticationScheme, displayName, configureOptions);
	}
}
