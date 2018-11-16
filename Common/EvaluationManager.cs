using System;
using System.Collections.Generic;

namespace Common
{
    public class EvaluationManager
    {
        public Definition Definition { get; }
        private readonly Dictionary<IRequirement, bool?> _requirements;

        private readonly HashSet<Trait> _traits;

        public EvaluationManager(Definition definition, IEnumerable<Trait> traits)
        {
            Definition = definition;
            _requirements = new Dictionary<IRequirement, bool?>();
            _traits = new HashSet<Trait>(traits);
        }

        public bool Evaluate()
        {
            foreach (var requirement in Definition.Requirements)
            {
                bool value;
                if (_requirements.ContainsKey(requirement))
                {
                    if (_requirements[requirement] == null)
                        throw new InvalidOperationException("loop detected in requirement evaluation! " + requirement.GetType());
                    value = _requirements[requirement].Value;
                }
                else
                {
                    _requirements.Add(requirement, null);
                    var evaluation = requirement.Evaluate(this, _traits);
                    _requirements[requirement] = evaluation;
                    value = evaluation;
                }
                if (!value)
                    return false;
            }
            return true;
        }
    }
}
