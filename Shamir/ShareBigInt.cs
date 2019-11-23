using System.Text;
using Org.BouncyCastle.Math;

namespace Shamir
{
    public class ShareBigInt
    {
        private readonly BigInteger x;     // shade number - in this case non-random number
        private readonly BigInteger m;     // calculated shade value

        // create the shade, it is be immutable
        public ShareBigInt(BigInteger x, BigInteger m)
        {
            this.x = x;
            this.m = m;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(x.ToString());
            sb.Append(", ").Append(m.ToString());
            return sb.ToString();
        }

        // getters
        public BigInteger GetX() { return x; }

        public BigInteger GetM() { return m; }
    }
}
