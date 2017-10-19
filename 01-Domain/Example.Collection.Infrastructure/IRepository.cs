using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Example.Collection.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        object CurrentSession();

        int Count(Expression<Func<T, bool>> predicate);

        IQueryable<T> EntityFetch<TRelated>(Expression<Func<T, TRelated>> relatedObjectSelector);

        IQueryable<T> EntityFetch<TRelated>(Expression<Func<T, IEnumerable<TRelated>>> relatedObjectSelector);

        IQueryable<T> Get(Expression<Func<T, bool>> expression);

        IQueryable<T> Get(Expression<Func<T, bool>> expression,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                          bool isCacheable);

        IQueryable<T> Get(Expression<Func<T, bool>> expression,
                          bool isCacheable);

        IQueryable<T> Get(Expression<Func<T, bool>> expression,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IQueryable<T> Get(Expression<Func<T, bool>> expression,
                          Func<IQueryable<T>, IQueryable<T>> fetch,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IQueryable<T> Get(Expression<Func<T, bool>> expression,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                          int? pageNumber,
                          int? pageSize,
                          int? maxResult,
                          bool isCacheable);

        T Load(object id);

        T Load(object id, LockMode lockMode = LockMode.Read);
        
        T Add(T instance);

        T AddOrUpdate(T entity);

        void Update(T instance);

        T GetSingle(object id);

        void Delete(T instance);
    }
}
