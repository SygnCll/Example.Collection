using System; 

namespace Example.Collection.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets or sets that indicates the unit of work factory key in LocalData
        /// </summary>
        string FactoryKey { get; set; }

        /// <summary>
        /// Gets value that indicates the unit of work in transaction
        /// </summary>
        bool IsInTransaction { get; }

        /// <summary>
        /// Begins transaction
        /// </summary>
        void Begin();

        /// <summary>
        /// Commits transaction and ends unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// AddSuccessHandler
        /// </summary>
        /// <param name="action">Success Action after unit of work commit</param>
        void AddSuccessHandler(Action action);
    }
}
