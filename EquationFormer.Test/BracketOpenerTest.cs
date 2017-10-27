using System.Linq;
using EquationFormer.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationFormer.Test
{
    [TestClass]
    public class BracketOpenerTest
    {
        [TestMethod]
        public void BracketOpeningTest()
        {
            IBracketOpener opener = new BracketOpener();

            var input = Helper.SplitSummands("2.5(3x + z)/0.50 - 5.66(5y - z)");

            var (multiplier, str) = opener.OpenBrackets(input);

            Assert.AreEqual(multiplier, 5);
            Assert.AreEqual(str, "3x + z");
            Assert.IsTrue(input.SequenceEqual(new[] {"- 5.66(5y ", "- z)"}));
        }
    }
}
