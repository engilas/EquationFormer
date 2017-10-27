using EquationFormer.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationFormer.Test
{
    [TestClass]
    public class EquationTest
    {
        [TestMethod]
        public void EquationToStringTest()
        {
            IEquationBuilder equationBuilder = new EquationBuilder(new BracketOpener(), new SummandBuilder());

            string inputEquation = "(5x+ 5y)/5 -x^2=1";

            var equation = equationBuilder.Create(inputEquation);
            equation.Processing();

            Assert.AreEqual(equation.ToString(), "- x^2 + x + y - 1 = 0");
        }
    }
}
