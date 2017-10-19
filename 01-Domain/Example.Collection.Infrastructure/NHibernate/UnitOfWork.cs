using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Collection.Infrastructure.NHibernate
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(ISession session, ILocalData<string, IUnitOfWork> localData, string factoryKey, IsolationLevel isolationLevel)
            : base(localData, factoryKey, isolationLevel)
        {
            this.Session = session;
            this.IsolationLevel = isolationLevel;
        }

        internal ISession Session { get; private set; }

        internal IsolationLevel IsolationLevel { get; private set; }

        #region Overriden Base Methods

        public override void Begin()
        {
            base.Begin();

            if (this.Session.Transaction.IsActive)
            {
                return;
            }

            this.Session.BeginTransaction(this.IsolationLevel);
        }

        public override void Commit()
        {
            base.Commit();

            if (!this.Session.Transaction.IsActive)
            {
                return;
            }

            this.Session.Transaction.Commit();

            this.ExecuteSuccessHandlers();
        }

        public override void Rollback()
        {
            base.Rollback();

            if (!this.Session.Transaction.IsActive)
            {
                return;
            }

            this.Session.Transaction.Rollback();
        }

        public override int GetHashCode()
        {
            return this.Session.GetSessionImplementation().SessionId.GetHashCode();
        }

        protected override void Close()
        {
            if (this.Session == null)
            {
                return;
            }

            //// CurrentSessionContext.Unbind(this.Session.SessionFactory);

            if (!this.Session.IsOpen)
            {
                return;
            }

            this.Session.Close();
        }
        #endregion
    }
}
