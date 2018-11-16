using System.Collections.Generic;

namespace Common
{
    public interface IRequirement
    {
        bool Evaluate(EvaluationManager manager, IReadOnlyCollection<Trait> traits);
    }
}
