using System;
using System.Collections.Generic;
using System.Text;

namespace Shamir
{
    public class Shamir
    {
        private readonly int p;     // prime number p > m && p > n
        private readonly int m;     // treshold (polynomial degree + 1)
        private readonly int n;     // number of shades
        private readonly int[] coefficients;    // a1, a2, ...

        public Shamir(int p, int n, int m)
        {
            this.p = p;
            this.m = m;
            this.n = n;
        }

        public Share[] Encode(int plain)
        {
            // TODO
            return new Share[1];
        }

        public int Decode(Share[] shares)
        {
            // TODO
            return 0;
        }
    }
}
