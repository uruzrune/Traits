using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class TraitManager
    {
        public IReadOnlyCollection<Trait> Traits => _traits;
        private readonly HashSet<Trait> _traits;

        public TraitManager()
        {
            _traits = new HashSet<Trait>();
        }

        public bool Add(Trait trait)
        {
            if (trait == null)
                throw new ArgumentException("trait cannot be null");
            if (trait.Definition == null)
                throw new ArgumentException("trait's definition is null");
            if (_traits.Any(x => x.Equals(trait)))
                throw new InvalidOperationException("trait named '" + trait.Definition.Name + "' already exists");

            //if (!Evaluate(trait.Definition))
            //    return false;

            _traits.Add(trait);
            return true;
        }

        public Trait Add(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("trait name cannot be null or empty");
            if (_traits.Any(x => x.Definition.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                throw new InvalidOperationException("trait named '" + name + "' already exists");

            var newTrait = new Trait(new Definition(name), value);
            _traits.Add(newTrait);

            return newTrait;
        }
    }
}
