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
        /// <param name="paths">ep:cf=> cf.Packages or cf => cf.Packages.Select(cp => cp.PackageRegistration</param>
        IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths);
        IQueryable<T> GetAllAsNoTracking();
        /// <param name="paths">ep:cf=> cf.Packages or cf => cf.Packages.Select(cp => cp.PackageRegistration</param>
        IQueryable<T> GetAllIncludeAsNoTracking(params Expression<Func<T, object>>[] paths);
    }
}
