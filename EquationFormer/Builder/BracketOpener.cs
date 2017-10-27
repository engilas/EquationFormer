using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationFormer.Builder
{
    public class BracketOpener : IBracketOpener
    {
        public bool ContainsBrackets(string input) => input.Contains('(') || input.Contains(')');
        
        public (double, string) OpenBrackets(List<string> parts)
        {
            var (first, last) = FindFirstAndLastBrackets(parts);

            if (first == -1 || last == -1)
                throw new Exception("Bracket opening error");
            
            int count = last - first + 1;
            var bracketParts = parts.GetRange(first, count);
            parts.RemoveRange(first, count);

            var str = new string(bracketParts.SelectMany(x => x).ToArray());
            return RemoveBracketsAndGetMultiplier(str);
        }

        private (int, int) FindFirstAndLastBrackets(List<string> parts)
        {
            int firstBracket = -1;
            int lastBracket = -1;

            int bracketsOpened = 0;
            for (int i = 0; i < parts.Count; i++)
            {
                
                if (parts[i].Contains('('))
                {
                    bracketsOpened += parts[i].Count(x => x == '(');
                    if (firstBracket == -1)
                    {
                        firstBracket = i;
                    }
                }

                if (parts[i].Contains(')'))
                {
                    lastBracket = i;
                    bracketsOpened--;

                    if (bracketsOpened == 0)
                        break;
                }
            }

            return (firstBracket, lastBracket);
        }

        private (double, string) RemoveBracketsAndGetMultiplier(string input)
        {
            double bracketMultiplier = 1;

            int firstBracketPosition = input.IndexOf('(');
            int lastBracketPosition = input.LastIndexOf(')');

            if (input[0] != '(')
            {
                var possibleNumer = input.Substring(0, firstBracketPosition);
                if (possibleNumer == "-")
                {
                    bracketMultiplier *= -1;
                }
                else if (possibleNumer != "+")
                {
                    bracketMultiplier *= double.Parse(possibleNumer, Helper.FormatProvider);
                }
            }

            if (input[input.Length - 1] != ')')
            {
                if (input[lastBracketPosition + 1] == '/')
                {
                    bracketMultiplier /= double.Parse(input.Substring(lastBracketPosition + 2), Helper.FormatProvider);
                }
                else
                {
                    bracketMultiplier *= double.Parse(input.Substring(lastBracketPosition + 1), Helper.FormatProvider);
                }
            }

            var result = input.Remove(lastBracketPosition).Remove(0, firstBracketPosition + 1);

            return (bracketMultiplier, result);
        }
    }
}
