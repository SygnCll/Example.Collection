using System.Data;
using System.Collections.Generic;
using Example.Collection.Infrastructure.Persistance;

namespace Example.Collection.Infrastructure
{
    /// <summary>
    /// UnitOfWorkFactory Interface
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        bool Initialize();

        bool Initialize(string configurationFileName);

        bool Initialize(DatabaseConfiguration configuration);

        bool Initialize(IEnumerable<DatabaseConfiguration> configurationList);

        IUnitOfWork Create(string factoryKey = null, string unitOfWorkKey = null, IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        IUnitOfWork CreateByDroping(string factoryKey = null, IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        IUnitOfWork GetUnitofWork(string factoryKey, string unitOfWorkKey = null);

        IUnitOfWork GetCurrentUnitofWork();

        void BuildSchema(string factoryKey = null);

        void BuildSchemaByDroping(string factoryKey = null);

        void ClearCache(string factoryKey);

        void SetInterceptor<T>(T interceptor) where T : class, new();

        object CreateSession(string factoryKey);
    }
}
