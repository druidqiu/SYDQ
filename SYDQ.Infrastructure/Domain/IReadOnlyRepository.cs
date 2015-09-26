using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SYDQ.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T> where T :class, IAggregateRoot ,new()
    {
        List<TModel> SqlQuery<TModel>(string sql, params object[] sqlParams);
        T GetEntity(object id);
        T GetEntity(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllAsNoTracking();
        /// <param name="path">split by ',',such as 'Orders.OrderLines,Messages'</param>
        IQueryable<T> GetAllInclude(string path);
        IQueryable<T> GetAllInclude<TProperty>(Expression<Func<T, TProperty>> path);
        /// <param name="path">split by ',',such as 'Orders.OrderLines,Messages'</param>
        IQueryable<T> GetAllIncludeAsNoTracking(string path);
        IQueryable<T> GetAllIncludeAsNoTracking<TProperty>(Expression<Func<T, TProperty>> path);
    }
}
