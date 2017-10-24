using global::NHibernate.Cfg;

namespace Example.Collection.Infrastructure
{
    public interface INHibernateConfigurationFileCache
    {
        Configuration LoadConfiguration(string configKey, string configPath, string[] mappingAssemblies);

        void SaveConfiguration(string configKey, Configuration config);
    }
}
