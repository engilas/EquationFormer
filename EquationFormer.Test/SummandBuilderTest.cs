using System.Collections.Generic;
using EquationFormer.Builder;
using EquationFormer.Node;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationFormer.Test
{
    [TestClass]
    public class SummandBuilderTest
    {
        [TestMethod]
        public void SummandCreateTest()
        {
            ISummandBuilder builder = new SummandBuilder();

            string input = "5.811z^4y^3xa^0";

            var expectedSummand = new Summand
            {
                Multiplier = 5.811,
                Variables = new List<Variable>
                {
                    new Variable {Exponent = 1, Letter = 'x'},
                    new Variable {Exponent = 3, Letter = 'y'},
                    new Variable {Exponent = 4, Letter = 'z'},
                }
            };


            var summand = builder.Create(input);

            Assert.IsNotNull(summand);
            Assert.IsTrue(expectedSummand.Equals(summand));

        }
    }
}
