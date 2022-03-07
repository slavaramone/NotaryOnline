using Microsoft.IdentityModel.Tokens;
using SharedLib.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedLib.Security
{
	public sealed class JwtFactory : IJwtFactory
	{
		private AuthOptions _authOptions;

		public JwtFactory(Microsoft.Extensions.Options.IOptions<AuthOptions> authOptions)
		{
			_authOptions = authOptions.Value;
		}
		public string Create(string key, params Claim[] claims)
		{
			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), "HS256")
			};

			JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			if (_authOptions.Lifetime.HasValue)
				jwtSecurityTokenHandler.TokenLifetimeInMinutes = (int)_authOptions.Lifetime?.TotalMinutes;
			SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

			return jwtSecurityTokenHandler.WriteToken(securityToken);
		}
	}
}
