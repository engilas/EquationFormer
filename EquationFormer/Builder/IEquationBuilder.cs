using EquationFormer.Node;

namespace EquationFormer.Builder
{
    public interface IEquationBuilder
    {
        Equation Create(string input);
    }
}
