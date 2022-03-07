using NotaryOnline.Api.ServiceModel;
using NotaryOnline.Entities;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Interfaces
{
	public interface IOrderRepository : IEntityRepository<Guid, Order>
	{
		Task<List<Order>> GetUserOrders(Guid userId);

		Task<List<Order>> Get(OrderStatus status);
	}
}
