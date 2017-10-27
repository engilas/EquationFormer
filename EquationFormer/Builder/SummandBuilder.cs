using System;
using System.Collections.Generic;
using System.Linq;
using EquationFormer.Node;

namespace EquationFormer.Builder
{
    public class SummandBuilder : ISummandBuilder
    {
        public Summand Create(string input)
        {
            input = input.ToLower();

            var summand = new Summand();

            double multiplier = 1;
            var multiplierSign = FindSignMultiplier(ref input);

            var parts = Helper.SplitVariables(input);

            //смотрим, является ли первая часть коэффициентом
            var possibleMultiplier = parts.First();
            if (Helper.TryParseDouble(possibleMultiplier, out double result))
            {
                multiplier = result;
                parts.RemoveAt(0);
            }

            var variables = FindVariables(parts).ToList();
            summand.Multiplier = multiplier * multiplierSign;
            summand.Variables = variables;

            return summand;
        }

        private IEnumerable<Variable> FindVariables(List<string> parts)
        {
            var result = new List<Variable>();
            foreach (var part in parts)
            {
                var variable = new Variable();

                var variableParts = part.Split('^');
                var leftPart = variableParts[0];

                if (leftPart.Length == 1 && char.IsLetter(leftPart.First()))
                {
                    variable.Letter = leftPart.First();
                }
                else
                {
                    throw new Exception("Variables finding error");
                }

                if (variableParts.Length == 2)
                {
                    var rightPart = variableParts[1];
                    variable.Exponent = int.Parse(rightPart);
                }


                result.Add(variable);
            }

            return result;
        }

        private int FindSignMultiplier(ref string value)
        {
            var signOccurancePos = value.IndexOfAny(new[] { '+', '-' });

            if (signOccurancePos == -1) return 1;

            int result;

            if (value[signOccurancePos] == '+')
            {
                result = 1;
            }
            else
            {
                result = -1;
            }

            value = value.Remove(signOccurancePos, 1);

            return result;
        }
    }
}
