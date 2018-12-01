using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UI.Annotations;

namespace UI.Logic
{
    public class ThreadWorker
    {
        public void Start()
        {

        }

        public bool CheckTask1Val(int val)
        {
            long res = long.MaxValue;
            BigInteger a = Factorial(25);
          
            return val >= 1 && val <= 100;
        }

        public BigInteger Factorial(uint n)
        {
            BigInteger res = 1;
            for (uint i = n; i > 1; i--)
                res = BigInteger.Multiply(res, i);
            return res;
        }

    }
}
