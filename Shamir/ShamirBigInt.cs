using System;
using System.Numerics;

namespace Shamir
{
    class ShamirBigInt
    {
        private readonly BigInteger p;                  // prime number p > m && p > n
        private readonly int m;                         // treshold (polynomial degree + 1)
        private readonly int n;                         // number of shades
        private readonly BigInteger[] coefficients;     // a1, a2, ...

        public ShamirBigInt(BigInteger p, int n, int m)
        {
            this.p = p;
            this.m = m;
            this.n = n;

            // polynomial degree
            int degree = m - 1;
            Random r = new Random();
            coefficients = new BigInteger[degree];

            // ceofficients calculation
            for (int i = 0; i < degree; i++)
            {
                coefficients[i] = RandomBigInteger(p);
            }

        }

        public ShareBigInt[] Encrypt(BigInteger M)
        {
            // TODO

            return new ShareBigInt[1];
        }

        public BigInteger Decrypt(ShareBigInt[] shares)
        {
            // TODO

            return new BigInteger(0);
        }

        private BigInteger RandomBigInteger(BigInteger N)
        {
            Random random = new Random();
            BigInteger result = 0;

            do
            {
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                random.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);

            return result;
        }
    }
}
