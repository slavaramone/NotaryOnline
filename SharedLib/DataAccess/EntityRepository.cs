using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedLib.DataAccess
{
	public class EntityRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity> where TEntity : IEntity<TKey>
	{
		protected readonly IDbConnectionFactory _dbConnectionFactory;

		public EntityRepository(IDbConnectionFactory db)
		{
			_dbConnectionFactory = db ?? throw new ArgumentNullException(nameof(db)); ;
		}

		public async Task<TEntity> Get(TKey id)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var entity = await db.SingleByIdAsync<TEntity>(id);
				return entity;
			}
		}

		public async Task<List<TEntity>> Get(List<TKey> ids)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var entities = await db.SelectAsync<TEntity>(x => ids.Contains(x.Id));
				return entities;
			}
		}

		public async Task<List<TEntity>> GetAll()
		{
			using (var db = _dbConnectionFactory.Open())
			{
				var entities = await db.SelectAsync<TEntity>();
				return entities;
			}
		}

		public async Task<TKey> Add(TEntity entity)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				await db.InsertAsync(entity);
				return entity.Id;
			}
		}

		public async Task<TKey> Update(TEntity entity)
		{
			using (var db = _dbConnectionFactory.Open())
			{
				await db.UpdateAsync(entity);
				return entity.Id;
			}
		}
	}
}
