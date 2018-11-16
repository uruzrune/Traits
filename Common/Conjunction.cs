using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Conjunction : IRequirement
    {
        public IReadOnlyCollection<IRequirement> Requirements => _requirements;
        private readonly HashSet<IRequirement> _requirements;

        internal Conjunction(ICollection<IRequirement> requirements)
        {
            if (requirements == null || requirements.Count < 2)
                throw new ArgumentException("requirements must have two or more elements");
            if (requirements.Count != requirements.Distinct().Count())
                throw new InvalidOperationException("requirements elements must all be distinct");

            _requirements = new HashSet<IRequirement>(requirements);
        }

        public bool Evaluate(EvaluationManager manager, IReadOnlyCollection<Trait> traits)
        {
            return _requirements.Aggregate(true, (current, requirement) => current && requirement.Evaluate(manager, traits));
        }
    }
}
