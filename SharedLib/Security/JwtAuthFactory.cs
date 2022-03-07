using ServiceStack;
using ServiceStack.Auth;
using SharedLib.Options;
using System;

namespace SharedLib.Security
{
	/// <summary>
	/// Генерация токена для JwtAuthProvider ServiceStack.Net
	/// </summary>
	public class JwtAuthFactory : IJwtAuthFactory
	{
		private AuthOptions _authOptions;

		public JwtAuthFactory(Microsoft.Extensions.Options.IOptions<AuthOptions> authOptions)
		{
			_authOptions = authOptions.Value;
		}

		public string Create(string key, Guid userId, string userEmail, string userRole)
		{
			var jwtProvider = new JwtAuthProvider 
			{
				AuthKeyBase64 = key,
				HashAlgorithm = Constants.JwtAuthHashAlgoritm
			};

			var header = JwtAuthProvider.CreateJwtHeader(jwtProvider.HashAlgorithm);
			var session = new AuthUserSession
			{
				UserAuthId = userId.ToString(),
				DisplayName = userEmail,
				Email = userEmail,
				IsAuthenticated = true,
			};
			var body = JwtAuthProvider.CreateJwtPayload(
				session: session,
				issuer: jwtProvider.Issuer,
				expireIn: _authOptions.Lifetime.Value,
				roles: new[] { userRole },
				permissions: new string[0]);

			var jwtToken = JwtAuthProvider.CreateJwt(header, body, jwtProvider.GetHashAlgorithm());

			return jwtToken;
		}
	}
}
