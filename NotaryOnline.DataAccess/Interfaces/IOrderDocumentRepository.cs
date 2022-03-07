using NotaryOnline.Entities;
using SharedLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotaryOnline.DataAccess.Interfaces
{
	public interface IOrderDocumentRepository : IEntityRepository<int, OrderDocument>
	{
		Task<List<OrderDocument>> Get(Guid orderId);

		Task<OrderDocument> Get(string documentId);
	}
}
