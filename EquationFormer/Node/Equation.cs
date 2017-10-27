using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationFormer.Node
{
    public class Equation
    {
        public List<Summand> LeftSide { get; private set; }
        public List<Summand> RightSide { get; private set; }

        public Equation(IEnumerable<Summand> leftSide, IEnumerable<Summand> rightSide)
        {
            LeftSide = leftSide.ToList();
            RightSide = rightSide.ToList();
        }

        public void Processing()
        {
            RightToLeft();
            SumLeftCompatibles();

            LeftSide = LeftSide.OrderByDescending(x => x).ToList();
        }

        private void RightToLeft()
        {
            foreach (var summand in RightSide)
            {
                summand.Multiplier *= -1;
            }

            LeftSide = LeftSide.Concat(RightSide).ToList();
            RightSide.Clear();
        }

        private void SumLeftCompatibles()
        {
            var newList = new List<Summand>();

            while (LeftSide.Count > 0)
            {
                var instance = LeftSide.First();
                LeftSide.RemoveAt(0);
                var compatibles = LeftSide.Where(x => x.Compatible(instance)).ToArray();
                foreach (var summand in compatibles) 
                {
                    instance = instance.Sum(summand);
                    LeftSide.Remove(summand);
                }

                newList.Add(instance);
            }

            newList.RemoveAll(x => Math.Abs(x.Multiplier) < Helper.Epsilon);

            LeftSide = newList;
        }

        private string PrepareExpression(List<Summand> expression)
        {
            string result;
            if (expression == null || expression.Count == 0)
            {
                result = "0";
            }
            else
            {
                result = string.Join(" ", LeftSide);
                if (result[0] == '+')
                {
                    result = result.Remove(0, 2);
                }
            }
            return result;
        }

        public override string ToString()
        {
            string leftSide = PrepareExpression(LeftSide);
            string rightSide = PrepareExpression(RightSide);


            return leftSide + " = " + rightSide;
        }

    }
}

