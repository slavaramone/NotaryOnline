using System.Security.Claims;

namespace SharedLib.Security
{
	public interface IJwtFactory
	{
		string Create(string key, params Claim[] claims);
	}
}
