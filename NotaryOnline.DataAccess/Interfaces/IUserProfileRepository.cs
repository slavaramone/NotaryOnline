using NotaryOnline.Entities;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Interfaces
{
	public interface IUserProfileRepository : IEntityRepository<int, UserProfile>
	{
		Task<List<UserProfile>> Get(List<Guid> userIds);
	}
}
