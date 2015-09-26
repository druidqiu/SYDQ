using SYDQ.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using SYDQ.Infrastructure.Domain;
using SYDQ.Repository.EF;

namespace SYDQ.Repository.EF
{
    public class EntityRepository<T> : IRepository<T>
        where T : class, IAggregateRoot, new()
    {
        private readonly EntitiesContext _entities;
        private readonly IDbSet<T> _dbSet;

        public EntityRepository()
        {
            _entities = EntitiesContextFactory.GetEntitiesContext();
            _dbSet = _entities.Set<T>();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void BulkInsert(IList<T> list)
        {
            foreach (T entity in list)
            {
                _dbSet.Add(entity);
            }
        }

        public void BulkInsertAsNoTracking(IList<T> list)
        {
            _entities.Configuration.AutoDetectChangesEnabled = false;
            foreach (T entity in list)
            {
                _dbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void BulkUpdate(IList<T> list)
        {
            foreach (T entity in list)
            {
                Update(entity);
            }
        }

        public void BulkUpdateAsNoTracking(IList<T> list)
        {
            _entities.Configuration.AutoDetectChangesEnabled = false;
            foreach (T entity in list)
            {
                Update(entity);
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void BulkDelete(Expression<Func<T, bool>> expression)
        {
            _dbSet.Where(expression).Delete();
        }

        public int ExcuteSql(string sql, params object[] sqlParams)
        {
            return _entities.Database.ExecuteSqlCommand(sql, sqlParams);
        }

        public T GetEntity(object id)
        {
            return _dbSet.Find(id);
        }

        public T GetEntity(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths)
        {
            var query = GetAll();
            foreach (var path in paths)
            {
                query = query.Include(path);
            }
            return query;
        }

        public IQueryable<T> GetAllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAllIncludeAsNoTracking(params Expression<Func<T, object>>[] paths)
        {
            var query = GetAllAsNoTracking();
            foreach (var path in paths)
            {
                query = query.Include(path);
            }
            return query;
        }

        public List<TModel> SqlQuery<TModel>(string sql, params object[] sqlParams)
        {
            return _entities.Database.SqlQuery<TModel>(sql, sqlParams).ToList();
        }
    }
}
