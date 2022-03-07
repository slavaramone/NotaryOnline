using NotaryOnline.Entities;
using SharedLib.DataAccess;
using System;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Interfaces
{
	public interface IAuthSessionRepository : IEntityRepository<Guid, AuthSession>
	{
		Task<AuthSession> Get(Guid sessionId, string code);
	}
}
