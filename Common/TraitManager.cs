using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class TraitManager
    {
        public IReadOnlyCollection<Trait> Traits => _traits;
        private readonly HashSet<Trait> _traits;

        public DefinitionManager Definitions { get; }

        public TraitManager(DefinitionManager definitonManager)
        {
            _traits = new HashSet<Trait>();
            Definitions = definitonManager;
        }

        public bool Add(Trait trait)
        {
            if (trait == null)
                throw new ArgumentException("trait is null");
            if (trait.Definition == null)
                throw new ArgumentException("trait definition is null");
            if (_traits.Any(x => x.Equals(trait)))
                throw new InvalidOperationException("trait named '" + trait.Definition.Name + "' already exists");

            var evaluationManager = new EvaluationManager(trait.Definition, _traits);
            if (!evaluationManager.Evaluate())
                return false;

            _traits.Add(trait);
            return true;
        }

        public Trait Add(string name, string value, Definition definition = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("trait is null or empty");

            if (definition == null)
                definition = new Definition(name);

            var newTrait = new Trait(definition, value);
            var success = Add(newTrait);

            if (!success)
                throw new InvalidOperationException("trait failed one or more requirements");

            return newTrait;
        }
    }
}
