using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Comparison : IRequirement
    {
        public Definition Definition { get; }
        public Operator Operator { get; }
        public string ExpectedValue { get; }

        internal Comparison(Definition definition, Operator op, string expectedValue)
        {
            if (definition == null)
                throw new ArgumentException("trait is null");
            if (op == null)
                throw new ArgumentException("operator is null");

            Definition = definition;
            Operator = op;
            ExpectedValue = expectedValue;
        }

        public override int GetHashCode()
        {
            return (Definition.Name + ":" + Operator + ":" + ExpectedValue).GetHashCode();
        }

        public bool Evaluate(EvaluationManager manager, IReadOnlyCollection<Trait> traits)
        {
            var targetTrait = traits.FirstOrDefault(x => x.Definition.Name == Definition.Name);
            return targetTrait != null && Compare(targetTrait);
        }

        private bool Compare(Trait trait)
        {
            if (Operator == Operator.Equal)
                return (string) trait.Value.GetValue<string>() == ExpectedValue;
            if (Operator == Operator.NotEqual)
                return (string) trait.Value.GetValue<string>() != ExpectedValue;
            if (Operator == Operator.Contains)
                return ((IEnumerable<string>) trait.Value.GetValue<IEnumerable<string>>()).Contains(ExpectedValue);
            if (Operator == Operator.DoesNotContain)
                return !((IEnumerable<string>) trait.Value.GetValue<IEnumerable<string>>()).Contains(ExpectedValue);
            if (Operator == Operator.IsNull)
                return !trait.Value.HasValue();
            if (Operator == Operator.IsNotNull)
                return trait.Value.HasValue();
            if (Operator == Operator.LessThan || Operator == Operator.LessThanEqual ||
                Operator == Operator.GreaterThan || Operator == Operator.GreaterThanEqual)
            {
                return NumericCompare((string) trait.Value.GetValue<string>(), ExpectedValue);
            }

            throw new NotImplementedException(Operator.Name);
        }

        private bool NumericCompare(string left, string right)
        {
            var leftType = GetNumberType(left);
            var rightType = GetNumberType(right);
            if (leftType == null || rightType == null)
                return false;
            if (leftType != rightType)
                return false;

            if (leftType == typeof(decimal))
            {
                var leftValue = decimal.Parse(left);
                var rightValue = decimal.Parse(right);

                if (Operator == Operator.LessThan)
                    return leftValue < rightValue;
                if (Operator == Operator.LessThanEqual)
                    return leftValue <= rightValue;
                if (Operator == Operator.GreaterThan)
                    return leftValue > rightValue;
                if (Operator == Operator.GreaterThanEqual)
                    return leftValue >= rightValue;
            }
            else
            {
                var leftValue = double.Parse(left);
                var rightValue = double.Parse(right);

                if (Operator == Operator.LessThan)
                    return leftValue < rightValue;
                if (Operator == Operator.LessThanEqual)
                    return leftValue <= rightValue;
                if (Operator == Operator.GreaterThan)
                    return leftValue > rightValue;
                if (Operator == Operator.GreaterThanEqual)
                    return leftValue >= rightValue;
            }

            return false;
        }

        private static Type GetNumberType(string value)
        {
            double doubleValue;
            decimal decimalValue;
            if (double.TryParse(value, out doubleValue))
                return typeof(double);
            if (decimal.TryParse(value, out decimalValue))
                return typeof(decimal);
            return null;
        }
    }
}
