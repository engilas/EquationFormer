using EquationFormer.Node;

namespace EquationFormer.Builder
{
    public interface ISummandBuilder
    {
        Summand Create(string input);
    }
}
