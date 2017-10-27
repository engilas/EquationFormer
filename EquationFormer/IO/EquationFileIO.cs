using System;
using System.Collections.Generic;
using System.IO;
using EquationFormer.Builder;

namespace EquationFormer.IO
{
    class EquationFileIO : IEquationIO
    {
        private readonly IEquationBuilder _builder;
        private readonly string _fileName;

        public EquationFileIO(IEquationBuilder builder, string fileName)
        {
            _builder = builder;
            _fileName = fileName;
        }

        public void Begin()
        {
            var strings = ReadAll();

            var equations = new List<Node.Equation>();

            foreach (var str in strings)
            {
                var equation = _builder.Create(str);
                if (equation == null)
                {
                    Console.Error.WriteLine("Equation building error");
                    continue;
                }
                equation.Processing();
                equations.Add(equation);
            }

            WriteEquations(equations);
        }

        public string[] ReadAll()
        {
            try
            {
                return File.ReadAllLines(_fileName);
            }
            catch
            {
                Console.Error.WriteLine("File reading error");
                throw;
            }
        }

        public void WriteEquations(IEnumerable<Node.Equation> equations)
        {
            var outFileName = _fileName + ".out";

            using (StreamWriter sw = new StreamWriter(outFileName, false))
            {
                foreach (var equation in equations)
                {
                    sw.WriteLine(equation.ToString());
                }
            }

            Console.WriteLine("File processing succeeded. Output file name: " + outFileName);
        }
    }
}
