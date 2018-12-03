using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UI.Logic
{
    public class FactorialHelper
    {
        public bool CheckInput(byte val) => val >= 1 && val <= 100;
        public BigInteger Factorial(BigInteger n) => n == 0 ? 0 : n == 1 ? 1 : n * Factorial(n - 1);
    }
}
