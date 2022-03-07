using NotaryOnline.DataAccess.Filters;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using SharedLib.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Repositories
{
	public class UserRepository : EntityRepository<Guid, User>, IUserRepository
	{
		public UserRepository(IDbConnectionFactory db) : base(db)
		{
		}

		public async Task<User> Get(string email, string password)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var user = await db.SingleAsync<User>(x => x.Email.Equals(email));
				if (user == null)
				{
					return null;
				}

				string hashedPassword = Crypto.HashPassword(password, user.PasswordSalt);

				if (hashedPassword.Equals(user.PasswordHash))
				{
					return user;
				}
				return null;
			}
		}

		public async Task<User> Get(string phone, bool isActive = true)
		{
			if (string.IsNullOrEmpty(phone))
			{
				return null;
			}

			using (var db = _dbConnectionFactory.Open())
			{
				User user = await db.SingleAsync<User>(x => x.Phone.Equals(phone));

				if (!isActive || (user != null && user.IsActive))
				{
					return user;
				}
				else
				{
					return null;
				}
			}
		}
		public async Task<List<User>> LoadByFilter(LoadUsersFilter filter)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var query = db.From<User>();

				query = query.OrderByDescending(e => e.CreatedDate);

				if (filter.Skip.HasValue)
				{
					query = query.Skip(filter.Skip.Value);
				}

				if (filter.Take.HasValue)
				{
					query = query.Take(filter.Take.Value);
				}

				var orders = await db.LoadSelectAsync(query);
				return orders;
			}
		}
	}
}
