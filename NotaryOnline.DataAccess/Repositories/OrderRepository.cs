using NotaryOnline.Api.ServiceModel;
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
	public class OrderRepository : EntityRepository<Guid, Order>, IOrderRepository
	{
		public OrderRepository(IDbConnectionFactory db) : base(db)
		{
		}

		public async Task<List<Order>> GetUserOrders(Guid userId)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				return await db.SelectAsync<Order>(x => x.UserId == userId);
			}
		}

		public async Task<List<Order>> Get(OrderStatus status)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				return await db.SelectAsync<Order>(x => x.Status == status);
			}
		}
	}
}
