using System;
using System.Data;
using System.Collections.Generic;

namespace Example.Collection.Infrastructure.NHibernate
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly IList<Action> successHandlers;
        private readonly ILocalData<string, IUnitOfWork> localData;

        public UnitOfWorkBase(ILocalData<string, IUnitOfWork> localData, string factorykey, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            successHandlers = new List<Action>();
            this.localData = localData;
            this.IsInTransaction = false;
            this.FactoryKey = factorykey;
        }

        public string FactoryKey { get; set; }

        public bool IsInTransaction { get; protected set; }

        protected bool IsDisposed { get; set; }

        public virtual void Begin()
        {
            this.IsInTransaction = true;
        }

        public virtual void Commit()
        {
            this.IsInTransaction = false;
        }

        public virtual void Rollback()
        {
            this.IsInTransaction = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void AddSuccessHandler(Action action)
        {
            this.successHandlers.Add(action);
        }

        protected void ExecuteSuccessHandlers()
        {
            if (this.successHandlers == null || this.successHandlers.Count <= 0)
            {
                return;
            }

            var exceptions = new List<Exception>();

            foreach (var successHandler in this.successHandlers)
            {
                try
                {
                    successHandler();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException("There are exceptions in success handlers of unit of work", exceptions);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            
            if (!this.IsDisposed && this.IsInTransaction)
            {
                this.Rollback();
            }

            this.Close();

            this.IsDisposed = true;
        }

        protected abstract void Close();
    }
}
