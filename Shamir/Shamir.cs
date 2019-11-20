using System;

namespace Shamir
{
    public class Shamir
    {
        private readonly int p;     // prime number p > m && p > n
        private readonly int m;     // treshold (polynomial degree + 1)
        private readonly int n;     // number of shades
        private int[] coefficients;    // a1, a2, ...

        public Shamir(int p, int n, int m)
        {
            this.p = p;
            this.m = m;
            this.n = n;

            // polynomial degree
            int degree = m - 1;
            Random r = new Random();
            coefficients = new int[degree];

            // ceofficients calculation
            for (int i = 0; i < degree; i++)
            {
                coefficients[i] = r.Next(p);
            }
        }

        public Share[] Encode(int M)
        {
            Share[] result = new Share[n];

            // i - number of shares
            for (int x = 1; x <= n; x++)
            {
                // W(x) value
                int W = 0;

                for(int i=1; i<m; i++)
                {
                    W += coefficients[i - 1] * (int) Math.Pow(x, i - 1);
                }

                // M = a0 - our secret
                W += M;

                // the big prime
                W %= p;

                result[x - 1] = new Share(x, W);
            }

            return result;
        }

        public int Decode(Share[] shares)
        {
            // TODO
            return 0;
        }
    }
}
