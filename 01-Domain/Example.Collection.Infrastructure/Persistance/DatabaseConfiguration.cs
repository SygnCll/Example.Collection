using System.Collections.Generic;

namespace Example.Collection.Infrastructure.Persistance
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration() => this.EventListeners = new List<EventListener>();

        public string FactoryKey { get; set; }
        public string[] MappingAssemblies { get; set; }
        public string ConfigFile { get; set; }
        public IDictionary<string, string> ConfigProperties { get; set; }
        public IEnumerable<EventListener> EventListeners { get; set; }
    }
}
