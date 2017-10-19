using NHibernate;
using Example.Collection.Infrastructure;
using Example.Collection.Infrastructure.Exceptions;

namespace Example.Collection.Repository
{
    public abstract class BaseEntityRepository<TEntity> where TEntity : class
    {
        protected BaseEntityRepository(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected IRepository<TEntity> Repository { get; }

        public TEntity Add(TEntity instance)
        {
            return Repository.Add(instance);
        }

        public TEntity AddOrUpdate(TEntity instance)
        {
            return Repository.AddOrUpdate(instance);
        }

        public TEntity GetSingle(object id)
        {
            return Repository.GetSingle(id);
        }

        public void Update(TEntity instance)
        {
            Repository.Update(instance);
        }

        public void Delete(TEntity instance)
        {
            Repository.Delete(instance);
        }

        public ISession GetSessionSafely()
        {
            var session = Repository.CurrentSession() as ISession;

            if (session == null)
            {
                throw new BusinessException("Nhibernate session is null, this is a critical error!");
            }

            return session;
        }
    }
}
