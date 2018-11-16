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

        public Comparison(Definition definition, Operator op, string expectedValue)
        {
            if (definition == null)
                throw new ArgumentException("trait cannot be null");
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
                return trait.Value != null && trait.Value == ExpectedValue;
            if (Operator == Operator.NotEqual)
                return trait.Value != null && trait.Value != ExpectedValue;
            if (Operator == Operator.Contains)
                return trait.Value != null && trait.Value.Contains(ExpectedValue);
            if (Operator == Operator.DoesNotContain)
                return trait.Value != null && !trait.Value.Contains(ExpectedValue);
            if (Operator == Operator.IsNull)
                return trait.Value == null;
            if (Operator == Operator.IsNotNull)
                return trait.Value != null;
            if (Operator == Operator.LessThan || Operator == Operator.LessThanEqual ||
                Operator == Operator.GreaterThan || Operator == Operator.GreaterThanEqual)
            {
                return NumericCompare(trait.Value, Operator, ExpectedValue);
            }

            throw new NotImplementedException("don't know how to check for operator '" + Operator.Name + "'");
        }

        private bool NumericCompare(string left, Operator op, string right)
        {
            var leftType = GetNumberType(left);
            var rightType = GetNumberType(left);
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
