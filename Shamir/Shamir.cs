using System;

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

            // polynomial degree
            int degree = m - 1;
            Random r = new Random();
            coefficients = new int[degree];

            // ceofficients calculation
            for (int i = 0; i < degree; i++)
            {
                coefficients[i] = r.Next(1, p);
            }
        }

        public Share[] Encrypt(int M)
        {
            Share[] result = new Share[n];

            // create n shades
            for (int i = 0; i < n; i++)
            {
                int W = 0;

                // create a single shade value
                int x = i + 1;

                for (int j = 0; j < coefficients.Length; j++)
                {
                    int n = coefficients.Length - j;
                    W += coefficients[j] * (int)Math.Pow(x, n);
                }

                // add a0 = M
                W += M;
                W %= p;

                // create the new shade and add it to result
                result[i] = new Share(x, W);
            }

            // return the array of shades
            return result;
        }

        public int Decrypt(Share[] shares)
        {
            // secret = W(0)
            int result = 0;

            for (int i = 0; i < m; i++)
            {
                long temp = 1;

                for (int j = 0; j < m; j++)
                {
                    if (i != j)
                    {
                        long part1 = ModNegative(-shares[j].GetX(), p);
                        long part2 = ModSecond(shares[i].GetX() - shares[j].GetX(), p);
                        temp *= part1 * part2 % p;
                    }
                }

                temp *= shares[i].GetM();

                result += (int)temp;
                result %= p;
            }

            return result % p;
        }

        private int ModSecond(int a, int m)
        {
            if(a < 0)
            {
                int temp = ModNegative(a, m);
                return ModularInverse(temp, m);
            }

            return ModularInverse(a, m);
        }

        private int ModNegative(int a, int m)
        {
            int r = a % m;
            return r < 0 ? r + m : r;
        }

        // source: https://rosettacode.org/wiki/Modular_inverse#C.23
        private int ModularInverse(int a, int m)
        {
            if (m == 1) return 0;
            int m0 = m;
            (int x, int y) = (1, 0);

            while (a > 1)
            {
                int q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }
    }
}
