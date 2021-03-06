﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using SYDQ.Infrastructure.Domain;

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
            return paths.Aggregate(query, (current, path) => current.Include(path));
        }

        public IQueryable<T> GetAllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAllIncludeAsNoTracking(params Expression<Func<T, object>>[] paths)
        {
            var query = GetAllAsNoTracking();
            return paths.Aggregate(query, (current, path) => current.Include(path));
        }

        public List<TModel> SqlQuery<TModel>(string sql, params object[] sqlParams)
        {
            return _entities.Database.SqlQuery<TModel>(sql, sqlParams).ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] paths)
        {
            var query = GetAllAsNoTracking();
            if (paths != null)
            {
                paths.Aggregate(query, (current, path) => current.Include(path));
            }
            return query.Where(where).ToList();
        }


        public IEnumerable<TModel> SqlQuery<TModel>(string sql, Dictionary<string, object> sqlParams = null)
        {
            if (sqlParams != null)
            {
                SqlParameter[] parameters = new SqlParameter[sqlParams.Keys.Count];
                int index = 0;
                foreach (var param in sqlParams)
                {
                    parameters[index] = new SqlParameter(param.Key, param.Value);
                    index++;
                }
                return _entities.Database.SqlQuery<TModel>(sql, parameters).ToList();
            }
            return _entities.Database.SqlQuery<TModel>(sql).ToList();
        }
    }
}
