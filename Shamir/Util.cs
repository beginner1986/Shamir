using System;
using Org.BouncyCastle.Math;

namespace Shamir
{
    public static class Util
    {
        // get a random BigInteger number
        // function based on: https://stackoverflow.com/questions/17357760/how-can-i-generate-a-random-biginteger-within-a-certain-range
        // original function was written for System.Numeric.BigInteger
        public static BigInteger RandomBigInteger(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;
            Random random = new Random();

            do
            {
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger(bytes);
            } while (R.CompareTo(N) >= 0);

            return R.Abs();
        }
    }
}
