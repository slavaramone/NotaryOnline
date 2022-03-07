using NotaryOnline.DataAccess.Filters;
using NotaryOnline.Entities;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Interfaces
{
	public interface IUserRepository : IEntityRepository<Guid, User>
	{
		Task<User> Get(string email, string password);

		Task<List<User>> Get(List<Guid> ids);

		Task<User> Get(string phone, bool isActive = true);

		Task<List<User>> LoadByFilter(LoadUsersFilter filter);
	}
}
