using System;
using EquationFormer.Builder;

namespace EquationFormer.IO
{
    class EquationConsoleIO : IEquationIO
    {
        private readonly IEquationBuilder _builder;
        public EquationConsoleIO(IEquationBuilder builder)
        {
            _builder = builder;
        }

        public void Begin()
        {
            Console.CancelKeyPress += (sender, args) => Environment.Exit(0);

            while (true)
            {
                Console.WriteLine("Enter equation: ");
                var input = Console.ReadLine();

                if (input == null) continue;

                var result = _builder.Create(input);
                if (result != null)
                {
                    result.Processing();
                    Console.WriteLine(result);
                }
                else Console.Error.WriteLine("Error on building equation");
            }
        }
    }
}
