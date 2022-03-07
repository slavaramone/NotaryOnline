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
	public class OrderDocumentRepository : EntityRepository<int, OrderDocument>, IOrderDocumentRepository
	{
		public OrderDocumentRepository(IDbConnectionFactory db) : base(db)
		{
		}

		public async Task<List<OrderDocument>> Get(Guid orderId)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				return await db.SelectAsync<OrderDocument>(x => x.OrderId == orderId);
			}
		}

		public async Task<OrderDocument> Get(string documentId)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				return await db.SingleAsync<OrderDocument>(x => x.DocumentId == documentId);
			}
		}
	}
}
