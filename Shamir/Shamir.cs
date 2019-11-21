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

                        temp *= (-shares[j].GetX()) / (shares[i].GetX() - shares[j].GetX());
                        Console.Write("[" + (-shares[j].GetX()) + " / (" + shares[i].GetX() + " - " + shares[j].GetX() + ")]");
                    }
                }

                Console.Write(" * " + shares[i].GetM());
                temp *= shares[i].GetM();
                temp %= p;
                Console.Write(" mod " + p + " + ");

                result += temp;
            }

            return result % p;
        }

        public int ModularInverse(int a, int m)
        {
            if(a < 0)
            {
                return a + m;
            }

            a %= m;
            
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }
}
