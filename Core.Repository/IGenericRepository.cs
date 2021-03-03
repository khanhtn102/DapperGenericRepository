using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        IEnumerable<TEntity> GetList(string query, object parameters);
        Task<IEnumerable<TEntity>> GetListAsync(string query, object parameters);
        TEntity Get(object pksFields);
        Task<TEntity> GetAsync(object pksFields);
        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(object key);
        Task RemoveAsync(object key);
        int Update(TEntity entity, object pks);
        Task<int> UpdateAsync(TEntity entity, object pks);
        int InstertOrUpdate(TEntity entity, object pks);
        Task<int> InstertOrUpdateAsync(TEntity entity, object pks);
    }
}
