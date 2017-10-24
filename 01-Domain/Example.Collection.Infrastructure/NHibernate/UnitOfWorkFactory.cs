using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using global::NHibernate;
using global::NHibernate.Cfg;
using global::NHibernate.Event;
using global::NHibernate.Tool.hbm2ddl;
using global::FluentNHibernate.Cfg;
using Example.Collection.Infrastructure.Persistance;

namespace Example.Collection.Infrastructure.NHibernate
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public const string DefaultFactoryKey = "nhibernate.cfg";
        public const string UnitOfWorkKey = "UnitOfWork.Key";
        public const string DefaultDatabaseConfigFile = "Nhibernate.config";
        private const string LOCAL_DATA_REGION = "NHibernateUnitOfWork";

        private static readonly ILocalData<string, IUnitOfWork> UnitOfWorkLocalData;
        private readonly Dictionary<string, ISessionFactory> sessionFactories = new Dictionary<string, ISessionFactory>();
        private readonly Dictionary<string, Configuration> configurations = new Dictionary<string, Configuration>();
        private Dictionary<string, string> globalDatabaseConfigurationList;
        private INHibernateConfigurationFileCache configurationCache;
        private dynamic Interceptor { get; set; }

        #region IUnitOfWorkFactory Members

        public IUnitOfWork Create(string factoryKey = DefaultFactoryKey, string unitOfWorkKey = UnitOfWorkKey, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            // TODO:
            if (string.IsNullOrEmpty(factoryKey))
            {
                factoryKey = DefaultFactoryKey;
            }
            if (string.IsNullOrEmpty(unitOfWorkKey))
            {
                unitOfWorkKey = UnitOfWorkKey;
            }

            if (this.sessionFactories.ContainsKey(factoryKey))
            {
                IUnitOfWork uow = this.GetUnitofWork(factoryKey, unitOfWorkKey);

                if (uow == null)
                {
                    uow = new UnitOfWork(this.sessionFactories[factoryKey].OpenSession(), UnitOfWorkLocalData, factoryKey + unitOfWorkKey, isolationLevel);
                    this.SetCurrentUnitofWork(factoryKey, unitOfWorkKey, uow);
                }

                return uow;
            }
            else
            {
                // TODO: throw parameter exception
                throw new ApplicationException("Invalid contextName : " + factoryKey);
            }
        }

        public IUnitOfWork CreateByDroping(string factoryKey = DefaultFactoryKey, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            // TODO:
            return null;
        }

        public IUnitOfWork GetCurrentUnitofWork()
        {
            return UnitOfWorkLocalData.GetLastValue();
        }

        public IUnitOfWork GetUnitofWork(string factoryKey = DefaultFactoryKey, string unitOfWorkKey = UnitOfWorkKey)
        {
            if (string.IsNullOrEmpty(factoryKey))
            {
                factoryKey = DefaultFactoryKey;
            }
            if (string.IsNullOrEmpty(unitOfWorkKey))
            {
                unitOfWorkKey = UnitOfWorkKey;
            }

            UnitOfWorkLocalData.TryGetValue(factoryKey + unitOfWorkKey, out IUnitOfWork value);
            return value;
        }

        public void BuildSchema(string factoryKey = null)
        {
            SchemaExport schema = GetSchemaExport(factoryKey);

            if (schema != null)
            {
                schema.Execute(true, false, false);
            }
        }

        public void BuildSchemaByDroping(string factoryKey = null)
        {
            SchemaExport schema = GetSchemaExport(factoryKey);

            if (schema != null)
            {
                schema.Execute(true, true, false);
            }
        }

        public void ClearCache(string factoryKey)
        {
            var sessionFactory = sessionFactories[factoryKey];

            sessionFactory.EvictQueries();

            foreach (var collectionMetadata in sessionFactory.GetAllCollectionMetadata())
                sessionFactory.EvictCollection(collectionMetadata.Key);

            foreach (var classMetadata in sessionFactory.GetAllClassMetadata())
                sessionFactory.EvictEntity(classMetadata.Key);
        }

        public void SetInterceptor<T>(T interceptor) where T : class, new()
        {
            try
            {
                if (!typeof(T).GetInterfaces().Contains(typeof(IInterceptor)))
                {
                    return;
                }

                Interceptor = interceptor;
            }
            catch (Exception)
            {
                Interceptor = null;
                throw new InvalidCastException("Interceptor cannot be assigned to configuration, please check your implementation interface or assignable interceptor type.");
            }
        }

        public object CreateSession(string factoryKey)
        {
            return this.sessionFactories[factoryKey].OpenSession();
        }

        #endregion


        #region Nhibernet Configuration & Initialize

        static UnitOfWorkFactory()
        {
            UnitOfWorkLocalData = LocalDataFactory<string, IUnitOfWork>.GetOrAdd(LOCAL_DATA_REGION);
        }

        public UnitOfWorkFactory()
        {
        }

        public INHibernateConfigurationFileCache ConfigurationCache
        {
            get
            {
                if (this.configurationCache == null)
                {
                    this.configurationCache = new NHibernateConfigurationFileCache();
                }

                return this.configurationCache;
            }

            set
            {
                if (this.configurationCache != null)
                {
                    throw new InvalidOperationException("Configuration Cache has been allready setted");
                }

                this.configurationCache = value;
            }
        }

        public bool Initialize(DatabaseConfiguration configuration)
        {
            return this.Initialize(
                    configuration.FactoryKey,
                    configuration.MappingAssemblies,
                    configuration.EventListeners,
                    configuration.ConfigFile,
                    configuration.ConfigProperties);
        }

        public bool Initialize(IEnumerable<DatabaseConfiguration> configurationList)
        {
            configurationList
                .ToList()
                .ForEach(q => this.Initialize(q));

            return true;
        }

        public bool Initialize()
        {
            return Initialize(DefaultDatabaseConfigFile);
        }

        public bool Initialize(string configurationFileName)
        {
            XDocument config = null;

            if (File.Exists(configurationFileName))
            {
                try
                {
                    config = XDocument.Load(configurationFileName);
                }
                catch
                {
                    throw new FormatException();
                }
            }

            if (config == null)
            {
                throw new FileNotFoundException("Config file not found", configurationFileName);
            }

            try
            {
                IEnumerable<DatabaseConfiguration> databaseConfigurationList = config.Descendants("configuration")
                .Elements("databaseCfg")
                .Select(s => new DatabaseConfiguration
                {
                    FactoryKey = s.Attribute("FactoryKey").Value,
                    ConfigFile = s.Attribute("ConfigFile").Value,
                    MappingAssemblies = s.Element("mappingAssemblies").Elements("add")
                                                    .Select(q => q.Value).ToArray<string>(),
                    ConfigProperties = s.Element("ConfigProperties").Elements("property")
                                                    .Select(q => new { key = q.Attribute("name").Value, value = q.Value })
                                                    .ToDictionary(q => q.key, q => q.value),
                    EventListeners = s.Element("eventListeners") == null ? new List<EventListener>() : s.Element("eventListeners").Elements("add")
                                                    .Select(q => new EventListener(q.Attribute("listenerType").Value, q.Attribute("typeName").Value)).ToList<EventListener>()
                });

                this.globalDatabaseConfigurationList = config.Descendants("configuration")
                    .Elements("GlobalNHibernateProperties")
                    .Elements("session-factory")
                    .Where(p => p.Attribute("name").Value.Contains("NHibernate"))
                    .Elements("property")
                    .ToDictionary(q => q.Attribute("name").Value, q => q.Value);

                databaseConfigurationList
                    .ToList()
                    .ForEach(q => this.Initialize(q));
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Invalid configuration file format", ex);
            }

            return true;
        }

        #endregion


        #region Private Methods

        private IUnitOfWork SetCurrentUnitofWork(string factoryKey, string unitOfWorkKey, IUnitOfWork uow)
        {
            UnitOfWorkLocalData[factoryKey + unitOfWorkKey] = uow;
            return uow;
        }

        private SchemaExport GetSchemaExport(string factoryKey = null)
        {
            if (string.IsNullOrEmpty(factoryKey))
            {
                factoryKey = DefaultFactoryKey;
            }

            if (this.configurations.ContainsKey(factoryKey))
            {
                return new SchemaExport(this.configurations[factoryKey]);
            }
            else
            {
                throw new ApplicationException("Invalid factoryKey : " + factoryKey);
            }
        }

        private bool Initialize(
            string factoryKey,
            string[] mappingAssemblies,
            IEnumerable<EventListener> eventListeners,
            string cfgFile,
            IDictionary<string, string> configProperties)
        {
            try
            {
                // TODO:CHECK!!
                if (string.IsNullOrEmpty(factoryKey))
                {
                    factoryKey = DefaultFactoryKey;
                }

                Configuration config;

                config = this.AddConfiguration(
                    factoryKey,
                    mappingAssemblies,
                    eventListeners,
                    this.ConfigureNHibernate(factoryKey, cfgFile, configProperties));

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Initialize failed!", ex);
            }
        }

        private Configuration AddConfiguration(
           string factoryKey,
           string[] mappingAssemblies,
            IEnumerable<EventListener> eventListeners,
           global::NHibernate.Cfg.Configuration cfg)
        {
            var sessionFactory = this.CreateSessionFactoryFor(mappingAssemblies, eventListeners, cfg);

            return this.AddConfiguration(factoryKey, sessionFactory, cfg);
        }

        private Configuration AddConfiguration(
            string factoryKey,
            ISessionFactory sessionFactory,
            Configuration cfg)
        {
            if (!this.sessionFactories.ContainsKey(factoryKey))
            {
                this.sessionFactories.Add(factoryKey, sessionFactory);
                this.configurations.Add(factoryKey, cfg);
                return cfg;
            }
            else
            {
                throw new Exception("A session factory has already been configured with the key of " + factoryKey);
            }
        }

        private ISessionFactory CreateSessionFactoryFor(
            IEnumerable<string> mappingAssemblies,
            IEnumerable<EventListener> eventListeners,
            global::NHibernate.Cfg.Configuration cfg)
        {
            try
            {

                var fluentConfiguration = Fluently.Configure(cfg);

                fluentConfiguration.Mappings(
                    m =>
                    {
                        foreach (var mappingAssembly in mappingAssemblies)
                        {

                            var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MakeLoadReadyAssemblyName(mappingAssembly));
                            if (!File.Exists(assemblyPath))
                                throw new Exception(string.Format("Mapping assembly is not exist. the file name is {0}", assemblyPath));

                            var assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);

                            m.HbmMappings.AddFromAssembly(assembly);
                            m.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
                        }
                    });



                fluentConfiguration.ExposeConfiguration(
                    e =>
                    {
                        e.EventListeners.FlushEventListeners = new IFlushEventListener[] { new FixedDefaultFlushEventListener() };

                        eventListeners
                            .ToList()
                            .ForEach(eventListener =>
                            {
                                global::NHibernate.Event.ListenerType listenerType = global::NHibernate.Event.ListenerType.NotValidType;

                                System.Enum.TryParse<global::NHibernate.Event.ListenerType>(eventListener.ListenerType, true, out listenerType);

                                if (listenerType == global::NHibernate.Event.ListenerType.NotValidType)
                                    throw new ApplicationException("NHibernate.Event.ListenerType not a valid type");

                                Type eventListenerType = Type.GetType(eventListener.TypeName);

                                if (eventListenerType == null)
                                    throw new ApplicationException("NHibernate.Event Assembly type cannot be null.");

                                object eventListenerObject = Activator.CreateInstance(eventListenerType);

                                switch (listenerType)
                                {
                                    case global::NHibernate.Event.ListenerType.PostInsert:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PostInsert,
                                            new global::NHibernate.Event.IPostInsertEventListener[] { (global::NHibernate.Event.IPostInsertEventListener)eventListenerObject });
                                        break;
                                    case global::NHibernate.Event.ListenerType.PostUpdate:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PostUpdate,
                                            new global::NHibernate.Event.IPostUpdateEventListener[] { (global::NHibernate.Event.IPostUpdateEventListener)eventListenerObject });
                                        break;
                                    case global::NHibernate.Event.ListenerType.PostDelete:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PostDelete,
                                            new global::NHibernate.Event.IPostDeleteEventListener[] { (global::NHibernate.Event.IPostDeleteEventListener)eventListenerObject });
                                        break;
                                    case global::NHibernate.Event.ListenerType.PreDelete:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PreDelete,
                                            new global::NHibernate.Event.IPreDeleteEventListener[] { (global::NHibernate.Event.IPreDeleteEventListener)eventListenerObject });
                                        break;
                                    case global::NHibernate.Event.ListenerType.PreInsert:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PreInsert,
                                            new global::NHibernate.Event.IPreInsertEventListener[] { (global::NHibernate.Event.IPreInsertEventListener)eventListenerObject });
                                        break;
                                    case global::NHibernate.Event.ListenerType.PreUpdate:
                                        e.AppendListeners(global::NHibernate.Event.ListenerType.PreUpdate,
                                            new global::NHibernate.Event.IPreUpdateEventListener[] { (global::NHibernate.Event.IPreUpdateEventListener)eventListenerObject });
                                        break;
                                }
                            });
                    });

                return fluentConfiguration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll") == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim();
        }

        private Configuration ConfigureNHibernate(string factoryKey, string configFile, IDictionary<string, string> configProperties)
        {
            var cfg = new Configuration();

            if (this.globalDatabaseConfigurationList != null && this.globalDatabaseConfigurationList.Count > 0)
            {
                cfg.AddProperties(this.globalDatabaseConfigurationList);
            }

            if (Interceptor != null)
            {
                cfg.SetInterceptor(Interceptor);
            }

            configProperties
                .ToList()
                .ForEach(q =>
                {
                    if (q.Key == "connection.connection_string" && System.Configuration.ConfigurationManager.GetSection("ProtectedSection") != null)
                    {
                        var connectionString = ((System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ProtectedSection"))[string.Format("ConnectionString_{0}", factoryKey)];

                        if (string.IsNullOrEmpty(connectionString))
                        {
                            cfg.SetProperty(q.Key, q.Value);
                        }
                        else
                        {
                            cfg.SetProperty(q.Key, connectionString);
                        }
                    }
                    else
                    {
                        cfg.SetProperty(q.Key, q.Value);
                    }
                });

            if (string.IsNullOrEmpty(configFile) == false)
            {
                if (File.Exists(configFile))
                {
                    return cfg.Configure(configFile);
                }
            }

            if (File.Exists("Nhibernate.cfg.xml"))
            {
                return cfg.Configure();
            }

            return cfg;
        }

        #endregion

    }
}
