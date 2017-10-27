using System;

namespace EquationFormer.Node
{
    public class Variable : IEquatable<Variable>, IComparable<Variable>
    {
        private char _letter;

        public char Letter
        {
            get =>_letter; 
            set
            {
                if (value >= 'a' && value <= 'z')
                    _letter = value;
                else throw new Exception("Trying set wrong character as variable letter");
            }
        }

        public int Exponent { get; set; } = 1;

        public override string ToString()
        {
            return Letter + (Exponent == 1 ? "" : "^" + Exponent);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Variable);
        }

        public bool Equals(Variable other)
        {
            return other != null &&
                   Letter == other.Letter &&
                   Exponent == other.Exponent;
        }

        public int CompareTo(Variable other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (Letter < other.Letter)
                return 1;
            if (Letter > other.Letter)
                return -1;

            return Exponent.CompareTo(other.Exponent);
        }
    }
}
