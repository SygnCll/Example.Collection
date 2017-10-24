namespace Example.Collection.Infrastructure
{
    public class EventListener
    {
        public EventListener(string listenerType, string typeName)
        {
            this.ListenerType = listenerType;
            this.TypeName = typeName;
        }

        public string ListenerType { get; set; }
        public string TypeName { get; set; }
    }
}
