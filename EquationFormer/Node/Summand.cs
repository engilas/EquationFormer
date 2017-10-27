using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationFormer.Node
{
    public class Summand : IComparable<Summand>
    {
        private List<Variable> _variables;

        public List<Variable> Variables
        {
            get => _variables;
            set
            {
                if (value == null) return;

                if (value.Count == 0)
                {
                    _variables = value;
                    return;
                }

                _variables = value.Where(x => x.Exponent != 0).OrderByDescending(x => x).ToList();
            }
        }

        public double Multiplier { get; set; }
        

        public bool Compatible(Summand s)
        {
            return Variables.SequenceEqual(s.Variables);
        }

        public Summand Sum(Summand s)
        {
            if (!Compatible(s))
                throw new Exception();

            var newSummand = new Summand
            {
                Variables = this.Variables,
                Multiplier = this.Multiplier + s.Multiplier
            };

            return newSummand;
        }

        public override string ToString()
        {
            var sign = Multiplier > 0 ? "+" : "-";
            string multiplier;
            double absMultiplier = Math.Abs(Multiplier);
            if (Math.Abs(absMultiplier - 1) < Helper.Epsilon && Variables.Count > 0)
            {
                multiplier = "";
            }
            else
            {
                multiplier = absMultiplier.ToString("0.###",Helper.FormatProvider);
            }

            var variables = new string(Variables.OrderBy(x => x.Letter).SelectMany(x => x.ToString()).ToArray());
            return $"{sign} {multiplier}{variables}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Summand);
        }

        protected bool Equals(Summand other)
        {
            return other != null
                   && Compatible(other)
                   && Math.Abs(Multiplier - other.Multiplier) < Helper.Epsilon;
        }

        public int CompareTo(Summand other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (Variables.Count > 0 && other.Variables.Count > 0)
            {
                if (Variables.Max(x => x.Exponent) > other.Variables.Max(x => x.Exponent))
                    return 1;
                if (Variables.Max(x => x.Exponent) < other.Variables.Max(x => x.Exponent))
                    return -1;
            }


            if (Variables.Count > other.Variables.Count) return 1;
            if (Variables.Count < other.Variables.Count) return -1;

            if (Variables.Count == other.Variables.Count)
            {
                if (Variables.First().Letter < other.Variables.First().Letter)
                    return 1;
                if (Variables.First().Letter > other.Variables.First().Letter)
                    return -1;
            }

            return 0;
        }
    }
}
