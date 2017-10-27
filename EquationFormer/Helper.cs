using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EquationFormer
{
    public static class Helper
    {
        public static readonly IFormatProvider FormatProvider = new CultureInfo("en-US");
        public const double Epsilon = 0.001;

        public static bool TryParseDouble(string value, out double result)
        {
            try
            {
                result = double.Parse(value, FormatProvider);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        public static List<string> SplitSummands(string input) =>
            Regex.Split(input, @"(?=[+-])").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        public static List<string> SplitVariables(string input) =>
            Regex.Split(input, @"(?=[a-z])").Where(x => x != "").ToList();
    }
}
