using System;
using System.Web;
using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;

namespace Example.Collection.Infrastructure
{
    public class LocalData<TKey, TValue> : ILocalData<TKey, TValue>
    {
        public string Region { get; private set; }

        public LocalData(string region)
        {
            Region = region;
        }

        public bool ContainsKey(TKey key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public TValue this[TKey key]
        {
            get { return this.Dictionary[key].Data; }
            set { this.Dictionary[key] = new TimedData<TValue>(value); }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            TimedData<TValue> timed;

            bool found = this.Dictionary.TryGetValue(key, out timed);
            value = found ? timed.Data : default(TValue);

            return found;
        }

        public TValue GetLastValue()
        {
            var value = this.Dictionary.Values.OrderByDescending(v => v.DateTime).FirstOrDefault();
            return value != null ? value.Data : default(TValue);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public void Remove(TKey key)
        {
            Dictionary.Remove(key);
        }

        public int Count
        {
            get { return Dictionary.Count; }
        }

        private Dictionary<TKey, TimedData<TValue>> Dictionary
        {
            get
            {
                if (RunningInWcf)
                {
                    var extension = OperationContext.Current.Extensions.Find<WcfStateExtension>();

                    if (extension == null)
                    {
                        extension = new WcfStateExtension();
                        OperationContext.Current.Extensions.Add(extension);
                    }

                    return extension.Dictionary;
                }

                if (RunningInWeb)
                {
                    var webDictionary = HttpContext.Current.Items[this.Region] as Dictionary<TKey, TimedData<TValue>>;

                    if (webDictionary == null)
                    {
                        webDictionary = new Dictionary<TKey, TimedData<TValue>>();
                        HttpContext.Current.Items[this.Region] = webDictionary;
                    }

                    return webDictionary;
                }

                return localData ?? (localData = new Dictionary<TKey, TimedData<TValue>>());
            }
        }

        private static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }
        }

        private static bool RunningInWcf
        {
            get { return OperationContext.Current != null; }
        }

        [ThreadStatic]
        private static Dictionary<TKey, TimedData<TValue>> localData;

        private class WcfStateExtension : IExtension<OperationContext>
        {
            public WcfStateExtension()
            {
                this.Dictionary = new Dictionary<TKey, TimedData<TValue>>();
            }

            public Dictionary<TKey, TimedData<TValue>> Dictionary { get; private set; }

            // we don't really need implementations for these methods in this case
            public void Attach(OperationContext owner)
            {
            }

            public void Detach(OperationContext owner)
            {
            }
        }
    }
}
