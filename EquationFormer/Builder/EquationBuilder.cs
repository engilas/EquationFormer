using System;
using System.Collections.Generic;
using System.Linq;
using EquationFormer.Node;

namespace EquationFormer.Builder
{
    public class EquationBuilder : IEquationBuilder
    {
        private readonly IBracketOpener _bracketOpener;
        private readonly ISummandBuilder _summandBuilder;

        public EquationBuilder(IBracketOpener bracketOpener, ISummandBuilder summandBuilder)
        {
            _bracketOpener = bracketOpener;
            _summandBuilder = summandBuilder;
        }

        public Equation Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var twoSides = input.Replace(" ", "").Split('=');
            if (twoSides.Length != 2)
            {
                Console.WriteLine("Need to be two sides of equation");
                return null;
            }

            var leftSide = twoSides[0];
            var rightSide = twoSides[1];

            try
            {
                var leftSummands = CreateSummandGroup(leftSide);
                var rightSummands = CreateSummandGroup(rightSide);
                var equation = new Node.Equation(leftSummands, rightSummands);

                return equation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bad input string: " + ex.Message);
                return null;
            }
        }

        private List<Summand> CreateSummandGroup(string part)
        {
            var parts = Helper.SplitSummands(part);

            var summands = new List<Summand>();

            while (parts.Any(x => _bracketOpener.ContainsBrackets(x)))
            { 
            
                var (multiplier, str) = _bracketOpener.OpenBrackets(parts);

                var summandInsideBrackets = CreateSummandGroup(str);

                foreach (var summand in summandInsideBrackets)
                {
                    summand.Multiplier *= multiplier;
                }

                summands.AddRange(summandInsideBrackets);
            }

            foreach (var element in parts)
            {
                var summand = _summandBuilder.Create(element);
                summands.Add(summand);
            }

            return summands;
        }
    }
}
