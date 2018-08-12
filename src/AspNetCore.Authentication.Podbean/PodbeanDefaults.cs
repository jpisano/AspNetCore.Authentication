namespace AspNetCore.Authentication.Podbean
{
	public static class PodbeanDefaults
	{
		public const string AuthenticationScheme = "Podbean";

		public static readonly string DisplayName = "Podbean";

		public static readonly string AuthorizationEndpoint = "https://api.podbean.com/v1/dialog/oauth";

		public static readonly string TokenEndpoint = "https://api.podbean.com/v1/oauth/token";

		public static readonly string UserInformationEndpoint = "https://api.podbean.com/v1/podcast";
	}
}
