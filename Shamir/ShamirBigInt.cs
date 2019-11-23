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
                BigInteger W = BigInteger.Zero;

                // create a single shade value
                BigInteger x = BigInteger.One.Add(new BigInteger(i.ToString()));
                

                for (int j = 0; j < coefficients.Length; j++)
                {
                    int n = coefficients.Length - j;
                    W = W.Add(coefficients[j].Multiply(x.Pow(n)));
                }

                // add a0 = M
                W = W.Add(M).Mod(p);

                // create the new shade and add it to result
                result[i] = new ShareBigInt(x, W);
            }

            // return the array of shades
            return result;
        }

        public BigInteger Decrypt(ShareBigInt[] shares)
        {
            // secret = W(0)
            BigInteger result = BigInteger.Zero;

            for (int i = 0; i < m; i++)
            {
                BigInteger temp = new BigInteger("1");

                for (int j = 0; j < m; j++)
                {
                    if (i != j)
                    {
                        BigInteger part1 = ModNegative(shares[j].GetX().Multiply(new BigInteger("-1")), p);
                        BigInteger part2 = ModSecond(shares[i].GetX().Subtract(shares[j].GetX()), p);
                        temp = temp.Multiply(part1.Multiply(part2).Mod(p));
                    }
                }

                temp = temp.Multiply(shares[i].GetM());

                result = result.Add(temp);
                result = result.Mod(p);
            }

            return result.Mod(p);
        }

        private BigInteger ModSecond(BigInteger a, BigInteger m)
        {
            if (a.CompareTo(BigInteger.Zero) < 0)
            {
                BigInteger temp = ModNegative(a, m);
                return temp.ModInverse(m);
            }

            return a.ModInverse(m);
        }

        private BigInteger ModNegative(BigInteger a, BigInteger m)
        {
            BigInteger r = a.Mod(m);
            return r.CompareTo(BigInteger.Zero) < 0 ? r.Add(m) : r;
        }
    }
}
