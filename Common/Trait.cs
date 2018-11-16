using System.Collections.Generic;

namespace Common
{
    public class Trait
    {
        public Definition Definition { get; }
        public Value Value { get; }

        internal Trait(Definition definition, string value = null)
        {
            Definition = definition;
            Value = new Value(value);
        }

        internal Trait(Definition definition, IEnumerable<string> value = null)
        {
            Definition = definition;
            Value = new Value(value);
        }

        internal Trait(Definition definition, IDictionary<string, string> value = null)
        {
            Definition = definition;
            Value = new Value(value);
        }
    }
}
