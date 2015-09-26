using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SYDQ.Infrastructure.Domain
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class, IAggregateRoot,new()
    {
        void Insert(T entity);
        void BulkInsert(IList<T> list);
        void BulkInsertAsNoTracking(IList<T> list);
        void Update(T entity);
        void BulkUpdate(IList<T> list);
        void BulkUpdateAsNoTracking(IList<T> list);
        void Delete(T entity);
        void BulkDelete(Expression<Func<T, bool>> expression);
        int ExcuteSql(string sql, params object[] sqlParams);
    }
}
