using System;
using EquationFormer.Builder;
using EquationFormer.IO;

namespace EquationFormer
{
    class Program
    {
        static void Main(string[] args)
        {
            IBracketOpener bracketOpener = new BracketOpener();
            ISummandBuilder summandBuilder = new SummandBuilder();
            IEquationBuilder equationBuilder = new EquationBuilder(bracketOpener, summandBuilder);

            IEquationIO equationIo;

            if (args.Length == 0)
            {
                equationIo = new EquationConsoleIO(equationBuilder);
            }
            else
            {
                equationIo = new EquationFileIO(equationBuilder, args[0]);
            }

            equationIo.Begin();
            
            Console.ReadKey();
        }
    }
}
