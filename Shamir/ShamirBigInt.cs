using Org.BouncyCastle.Math;

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
            coefficients = new BigInteger[degree];

            // ceofficients calculation
            for (int i = 0; i < degree; i++)
            {
                coefficients[i] = Util.RandomBigInteger(p);
            }

        }

        public ShareBigInt[] Encrypt(BigInteger M)
        {
            ShareBigInt[] result = new ShareBigInt[n];

            // create n shades
            for (int i = 0; i < n; i++)
            {
                //BigInteger W = 0;
                BigInteger W = new BigInteger("0");

                // create a single shade value
                //BigInteger x = i + 1;
                BigInteger x = new BigInteger("1").Add(new BigInteger(i.ToString()));
                

                for (int j = 0; j < coefficients.Length; j++)
                {
                    int n = coefficients.Length - j;
                    //W += coefficients[j] * BigInteger.Pow(x, n);
                    BigInteger temp = x.ModPow(x, new BigInteger(n.ToString()));
                    W = W.Add(coefficients[j].Multiply(temp));
                }

                // add a0 = M
                //W += M;
                W = W.Add(M);
                //W %= p;
                W = W.Mod(p);

                // create the new shade and add it to result
                result[i] = new ShareBigInt(x, W);
            }

            // return the array of shades
            return result;
        }

        public BigInteger Decrypt(ShareBigInt[] shares)
        {
            // TODO

            return new BigInteger("0");
        }
    }
}
