using System; 
namespace Example.Collection.Infrastructure
{
    public class TimedData<T>
    {
        public T Data { get; private set; }
        public DateTime DateTime { get; private set; }

        public TimedData(T data)
        {
            this.Data = data;
            this.DateTime = DateTime.Now;
        }
    }
}
