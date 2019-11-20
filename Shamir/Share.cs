using System.Text;

namespace Shamir
{
    public class Share
    {
        private readonly int x;     // shade number - in this case non-random number
        private readonly int m;     // calculated shade value

        // create the shade, it is be immutable
        public Share(int x, int m)
        {
            this.x = x;
            this.m = m;
        }

        // retunr the shade in single string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(x.ToString());
            sb.Append(", ").Append(m.ToString());
            return sb.ToString();
        }

        // getters
        public int GetX() { return x; }

        public int GetM() { return m; }
    }
}
