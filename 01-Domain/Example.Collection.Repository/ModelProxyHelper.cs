using NHibernate;

namespace Example.Collection.Repository
{
    public static class ModelProxyHelper
    {
        public static bool IsProxyAvailable(object proxy)
        {
            return proxy != null;
        }

        /// <summary>
        /// Determines whether [is proxy initialized] [the specified proxy].
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <returns>
        /// <c>true</c> if [is proxy initialized] [the specified proxy]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProxyInitialized(object proxy)
        {
            return NHibernateUtil.IsInitialized(proxy);
        }

        /// <summary>
        /// Initializes the NHibernate proxy.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public static void ProxyInitialize(this object proxy)
        {
            if (proxy == null)
                return;

            NHibernateUtil.Initialize(proxy);
        }
    }
}
