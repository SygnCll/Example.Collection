using System;
using global::NHibernate;
using global::NHibernate.Event;
using global::NHibernate.Event.Default;

namespace Example.Collection.Infrastructure.Persistance
{
    [Serializable]
    public class FixedDefaultFlushEventListener : DefaultFlushEventListener
    {
        protected override void PerformExecutions(IEventSource session)
        {
            try
            {
                session.ConnectionManager.FlushBeginning();
                session.PersistenceContext.Flushing = true;
                session.ActionQueue.PrepareActions();
                session.ActionQueue.ExecuteActions();
            }
            catch (HibernateException exception)
            {
                throw exception;
            }
            finally
            {
                session.PersistenceContext.Flushing = false;
                session.ConnectionManager.FlushEnding();
            }

        }
    }
}
