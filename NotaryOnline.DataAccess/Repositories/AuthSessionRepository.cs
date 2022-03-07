using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Repositories
{
	public class AuthSessionRepository : EntityRepository<Guid, AuthSession>, IAuthSessionRepository
	{
		public AuthSessionRepository(IDbConnectionFactory db) : base(db)
		{ }

		public async Task<AuthSession> Get(Guid sessionId, string code)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var session = await db.SingleAsync<AuthSession>(x => x.Id == sessionId && x.Code.Equals(code));
				return session;
			}
		}
	}
}
