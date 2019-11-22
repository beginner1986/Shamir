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
                coefficients[i] = r.Next(1, p);
            }

            /*
            // DEBUG
            coefficients[0] = 7;
            coefficients[1] = 8;
            */
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
                        //temp *= (ModularInverse(-shares[j].GetX(), p)) * (ModularInverse(ModNegative(shares[i].GetX() - shares[j].GetX(), p), p));
                        //Console.Write("[" + ModularInverse(-shares[j].GetX(), p) + " * " + (ModularInverse(ModNegative(shares[i].GetX() - shares[j].GetX(), p), p)) + "]");

                        long part1 = ModNegative(-shares[j].GetX(), p);
                        long part2 = ModSecond(shares[i].GetX() - shares[j].GetX(), p);
                        Console.WriteLine(part1 + " * " + part2 + " mod " + p);
                        temp *= part1 * part2 % p;
                    }
                }

                temp *= shares[i].GetM();
                temp %= 13;

               // Console.Write(" * " + shares[i].GetM());

                result += (int)temp;
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
            return (Math.Abs(a * m) + a) % m;
        }

        private int ModularInverse(int a, int m)
        {
            int i = m, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= m;
            if (v < 0) v = (v + m) % m;
            return v;
        }
    }
}
