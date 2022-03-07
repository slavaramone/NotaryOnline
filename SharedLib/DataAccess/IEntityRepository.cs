using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedLib.DataAccess
{
	public interface IEntityRepository<TKey, TEntity> where TEntity : IEntity<TKey>
	{
		Task<TEntity> Get(TKey id);

		Task<List<TEntity>> GetAll();

		Task<TKey> Add(TEntity entity);

		Task<TKey> Update(TEntity entity);
	}
}
