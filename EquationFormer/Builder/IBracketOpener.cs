using System.Collections.Generic;

namespace EquationFormer.Builder
{
    public interface IBracketOpener
    {
        bool ContainsBrackets(string input);

        /// <summary>
        /// Расрывает скобки, возвращая оставшиеся части после раскрытия и множитель.
        /// Удаляет из аргумента части, содержащие скобки.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns>Кортеж с множитилем с оставшейся строкой</returns>
        (double, string) OpenBrackets(List<string> parts);
    }
}
