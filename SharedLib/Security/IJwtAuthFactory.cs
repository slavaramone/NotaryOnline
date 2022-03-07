using System;

namespace SharedLib.Security
{
	public interface IJwtAuthFactory
	{
		string Create(string key, Guid userId, string userEmail, string userRole);
	}
}
