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

            /*
            // ceofficients calculation
            for (int i = 0; i < degree; i++)
            {
                coefficients[i] = r.Next(p);
            }
            */

            // DEBUG
            coefficients[0] = 7;
            coefficients[1] = 8;
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
                int temp = 1;

                for (int j = 0; j < m; j++)
                {
                    if (i != j)
                    {
                        temp *= (ModularInverse(-shares[j].GetX(), p)) * (ModularInverse(shares[i].GetX() - shares[j].GetX(), p));
                        Console.Write("[" + ModularInverse(-shares[j].GetX(), p) + " * " + (ModularInverse(shares[i].GetX() - shares[j].GetX(), p)) + "]");
                    }
                }

                temp %= 13;

                Console.Write(" * " + shares[i].GetM());

                result += temp;
            }

            return result % p;
        }

        public int ModularInverse(int a, int n)
        {
            int i = n, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
    }
}
