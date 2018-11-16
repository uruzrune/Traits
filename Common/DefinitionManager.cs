using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class DefinitionManager
    {
        private IReadOnlyCollection<Definition> Definitions => _definitions;
        private readonly HashSet<Definition> _definitions;

        public DefinitionManager()
        {
            _definitions = new HashSet<Definition>();
        }

        public void Add(Definition definition)
        {
            if (definition == null)
                throw new ArgumentException("definition cannot be null");

            if (_definitions.Contains(definition))
                throw new InvalidOperationException("definition with the name '" + definition.Name + "' already exists");

            _definitions.Add(definition);
        }

        public Definition Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("definition name cannot be null or empty");

            if (_definitions.Any(x => x.Name == name))
                throw new InvalidOperationException("trait '" + name + "' already exists");

            var definition = new Definition(name);
            _definitions.Add(definition);

            return definition;
        }

        public void Remove(Definition definition)
        {
            if (definition == null)
                throw new ArgumentException("trait cannot be null");

            if (!_definitions.Contains(definition))
                throw new InvalidOperationException("trait '" + definition.Name + "' does not exist");

            _definitions.Remove(definition);
        }

        public void Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("trait name cannot be null or empty");

            if (_definitions.All(x => x.Name != name))
                throw new InvalidOperationException("trait '" + name + "' does not exist");

            _definitions.RemoveWhere(x => x.Name == name);
        }
    }
}
