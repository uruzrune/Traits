using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Definition
    {
        public string Name { get; }
        public IReadOnlyCollection<IRequirement> Requirements => _requirements;

        private readonly HashSet<IRequirement> _requirements;

        internal Definition(string name, IEnumerable<IRequirement> requirements = null)
        {
            Name = name;
            _requirements = requirements != null
                ? new HashSet<IRequirement>(requirements)
                : new HashSet<IRequirement>();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var trait = obj as Definition;
            return trait != null && trait.Name == Name;
        }

        public void AddRequirement(IRequirement requirement)
        {
            if (requirement == null)
                throw new ArgumentException("requirement cannot be null");

            if (_requirements.Any(x => x.Equals(requirement)))
                throw new InvalidOperationException("requirement already exists");

            _requirements.Add(requirement);
        }
    }
}
