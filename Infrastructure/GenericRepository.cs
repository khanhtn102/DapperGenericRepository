using Autofac;
using Core.Repository;
using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection _conn;

        /// <summary>
        /// Manager Qry Constructor.
        /// </summary>
        private IPartsQryGenerator<TEntity> _partsQryGenerator;

        /// <summary>
        /// Manager to worker with Identified Fields
        /// </summary>
        private IIDentityInspector<TEntity> _identityInspector;

        /// <summary>
        /// Idetenfier parameter (@) to SqlServer (:) to Oracle
        /// </summary>
        private char ParameterIdentified { get; set; }

        protected string querySelect { get; set; }
        protected string queryInsert { get; set; }

        private string identityField;

        public GenericRepository(DbContext conn, char parameterIdentified = '@')
        {
            if (conn == null) throw new ArgumentNullException(nameof(conn), $"The parameter {nameof(conn)} can't be null");

            _conn = conn.GetConnection();
            ParameterIdentified = parameterIdentified;
            _partsQryGenerator = AutofacResolution.Instance.Resolve<PartsQryGenerator<TEntity>>(new NamedParameter("characterParameter", ParameterIdentified));
            _identityInspector = AutofacResolution.Instance.Resolve<IDentityInspector<TEntity>>(new NamedParameter("conn", conn));
        }

        public int Add(TEntity entity)
        {
            if (_conn == null) throw new ArgumentNullException(nameof(entity), $"The parameter {nameof(entity)} can't be null");

            CreateInsertQuery();

            int result = _conn.Execute(queryInsert, entity);

            return result;
        }

        public Task<int> AddAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                return Add(entity);
            });
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            ParameterValidator.ValidateEnumerable(entities, nameof(entities));

            CreateInsertQuery();

            int result = _conn.Execute(queryInsert, entities);

            return result;
        }

        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ParameterValidator.ValidateEnumerable(entities, nameof(entities));

            CreateInsertQuery();

            var result = _conn.ExecuteAsync(queryInsert, entities);

            return result;
        }

        public IEnumerable<TEntity> All()
        {
            CreateSelectQuery();

            IEnumerable<TEntity> result = _conn.Query<TEntity>(querySelect);

            return result;
        }

        public Task<IEnumerable<TEntity>> AllAsync()
        {
            return Task.Run(() =>
            {
                return All();
            });
        }

        public TEntity Get(object pksFields)
        {
            ParameterValidator.ValidateObject(pksFields, nameof(pksFields));

            var selectQry = _partsQryGenerator.GenerateSelect(pksFields);

            var result = _conn.Query<TEntity>(selectQry, pksFields).FirstOrDefault();

            return result;
        }

        public Task<TEntity> GetAsync(object pksFields)
        {
            return Task.Run(() =>
            {
                return Get(pksFields);
            });
        }

        public IEnumerable<TEntity> GetList(string query, object parameters)
        {
            ParameterValidator.ValidateString(query, nameof(query));
            ParameterValidator.ValidateObject(parameters, nameof(parameters));

            var result = _conn.Query<TEntity>(query, parameters);

            return result;
        }

        public Task<IEnumerable<TEntity>> GetListAsync(string query, object parameters)
        {
            ParameterValidator.ValidateString(query, nameof(query));
            ParameterValidator.ValidateObject(parameters, nameof(parameters));

            var result = _conn.QueryAsync<TEntity>(query, parameters);

            return result;
        }

        public int InstertOrUpdate(TEntity entity, object pks)
        {
            ParameterValidator.ValidateObject(entity, nameof(entity));
            ParameterValidator.ValidateObject(pks, nameof(pks));

            int result = 0;

            var entityInTable = Get(pks);

            if (entityInTable == null)
            {
                result = Add(entity);
            }
            else
            {
                result = Update(entity, pks);
            }

            return result;
        }

        public Task<int> InstertOrUpdateAsync(TEntity entity, object pks)
        {
            return Task.Run(() => InstertOrUpdate(entity, pks));
        }

        public void Remove(object key)
        {
            ParameterValidator.ValidateObject(key, nameof(key));

            var deleteQry = _partsQryGenerator.GenerateDelete(key);

            _conn.Execute(deleteQry, key);
        }

        public Task RemoveAsync(object key)
        {
            ParameterValidator.ValidateObject(key, nameof(key));

            var deleteQry = _partsQryGenerator.GenerateDelete(key);

            return _conn.ExecuteAsync(deleteQry, key);
        }

        public int Update(TEntity entity, object pks)
        {
            ParameterValidator.ValidateObject(entity, nameof(entity));
            ParameterValidator.ValidateObject(pks, nameof(pks));

            var updateQry = _partsQryGenerator.GenerateUpdate(pks);

            var result = _conn.Execute(updateQry, entity);

            return result;
        }

        public Task<int> UpdateAsync(TEntity entity, object pks)
        {
            ParameterValidator.ValidateObject(entity, nameof(entity));
            ParameterValidator.ValidateObject(pks, nameof(pks));

            var updateQry = _partsQryGenerator.GenerateUpdate(pks);

            var result = _conn.ExecuteAsync(updateQry, entity);

            return result;
        }

        private void CreateSelectQuery()
        {
            if (string.IsNullOrWhiteSpace(querySelect))
            {
                querySelect = _partsQryGenerator.GenerateSelect();
            }
        }

        private void CreateInsertQuery()
        {
            if (string.IsNullOrWhiteSpace(queryInsert))
            {
                identityField = _identityInspector.GetColumnsIdentityForType();
                queryInsert = _partsQryGenerator.GenerateInsert(identityField);
            }
        }
    }
}
