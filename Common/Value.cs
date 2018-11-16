using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class Value : JObject
    {
        public Value(string stringValue)
        {
            var dyn = (dynamic) this;

            if (stringValue != null)
                dyn.StringValue = stringValue;

            dyn.Collection = null;
            dyn.Dictionary = null;
        }

        public Value(IEnumerable<string> collection)
        {
            var dyn = (dynamic) this;

            if (collection != null)
                dyn.Collection = new List<string>(collection).ToArray();

            dyn.StringValue = null;
            dyn.Dictionary = null;
        }

        public Value(IDictionary<string, string> dictionary)
        {
            var dyn = (dynamic) this;

            if (dictionary != null)
                dyn.Dictionary = new Dictionary<string, string>(dictionary);
        }

        public bool HasValue()
        {
            var dyn = (dynamic) this;

            return dyn.StringValue is string ||
                   dyn.Collection != null && dyn.Collection is IEnumerable<string> ||
                   dyn.Dictionary != null && dyn.Dictionary is IDictionary<string, string>;
        }

        public void SetValue(string newValue)
        {
            var dyn = (dynamic) this;

            dyn.Collection = null;
            dyn.Dictionary = null;
            dyn.StringValue = newValue;
        }

        public void SetValue(IEnumerable<string> newValue)
        {
            var dyn = (dynamic) this;

            dyn.Collection = new List<string>(newValue).ToArray();
            dyn.StringValue = null;
            dyn.Dictionary = null;
        }

        public void SetValue(IDictionary<string, string> newValue)
        {
            var dyn = (dynamic) this;

            dyn.Dictionary = new Dictionary<string, string>(newValue);
            dyn.StringValue = null;
            dyn.Collection = null;
        }

        public object GetValue()
        {
            return GetValue<string>() ?? (GetValue<IEnumerable<string>>() ?? GetValue<IDictionary<string, string>>());
        }

        public object GetValue<T>()
        {
            var dyn = (dynamic) this;

            if (dyn.StringValue != null && typeof(string) == typeof(T))
                return (string) dyn.StringValue;
            if (dyn.Collection != null && ContainsInterface<T>(typeof(IEnumerable<string>)))
                return dyn.Collection as IEnumerable<string>;
            if (dyn.Dictionary != null && ContainsInterface<T>(typeof(IDictionary<string, string>)))
                return dyn.Dictionary as IDictionary<string, string>;

            return null;
        }

        public string GetStringValue()
        {
            return (string) GetValue<string>();
        }

        public IEnumerable<string> GetCollectionValue()
        {
            return (IEnumerable<string>) GetValue<IEnumerable<string>>();
        }

        public IDictionary<string, string> GetDictionaryValue()
        {
            return (IDictionary<string, string>) GetValue<IDictionary<string, string>>();
        }

        private static bool ContainsInterface<T>(Type type)
        {
            return typeof(T).GetInterfaces().Contains(type);
        }
    }
}
