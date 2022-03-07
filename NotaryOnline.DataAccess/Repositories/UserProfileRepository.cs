using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Repositories
{
	public class UserProfileRepository : EntityRepository<int, UserProfile>, IUserProfileRepository
	{
		public UserProfileRepository(IDbConnectionFactory db) : base(db)
		{
		}

		public async Task<List<UserProfile>> Get(List<Guid> userIds)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var entities = await db.SelectAsync<UserProfile>(x => userIds.Contains(x.UserId));
				return entities;
			}
		}
	}
}
