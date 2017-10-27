using System.Collections.Generic;
using System.Linq;
using EquationFormer.Builder;
using EquationFormer.Node;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationFormer.Test
{
    [TestClass]
    public class EquationBuilderTest
    {
        [TestMethod]
        public void EquationBuildAndProcessing()
        {
            IEquationBuilder equationBuilder = new EquationBuilder(new BracketOpener(), new SummandBuilder());

            string inputEquation = "x^2 + 2y = 2((2x-1)/2 + 2.5(x + y^2) - 7.11)";

            var expectedLeft = new List<Summand>
            {
                new Summand {Multiplier = 1, Variables = new List<Variable> {new Variable {Exponent = 2, Letter = 'x'}}},
                new Summand {Multiplier = -7, Variables = new List<Variable> {new Variable {Exponent = 1, Letter = 'x'}}},
                new Summand {Multiplier = -5, Variables = new List<Variable> {new Variable {Exponent = 2, Letter = 'y'}}},
                new Summand {Multiplier = 2, Variables = new List<Variable> {new Variable {Exponent = 1, Letter = 'y'}}},
                new Summand {Multiplier = 15.22, Variables = new List<Variable>()}
            };

            expectedLeft = expectedLeft.OrderByDescending(x => x).ToList();

            var equation = equationBuilder.Create(inputEquation);
            equation.Processing();

            Assert.IsNotNull(equation);
            Assert.IsTrue(expectedLeft.SequenceEqual(equation.LeftSide));
            Assert.IsTrue(equation.RightSide.Count == 0);
        }
    }
}
