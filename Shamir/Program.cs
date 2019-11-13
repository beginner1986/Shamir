using System;

namespace Shamir
{
    class Program
    {
        static void Main(string[] args)
        { 

            Share share = new Share("1", 20);

            Console.WriteLine(share.ToString());
        }
    }
}
