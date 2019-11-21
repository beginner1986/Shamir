using System;

namespace Shamir
{
    class Program
    {
        static void Main(string[] args)
        {
            // introduction
            Console.WriteLine("Kryptologia: Laoratorium nr 2- podział sekretów.");

            // create the encoder / decoder object
            Shamir shamir = new Shamir(13, 5, 3);

            // get the secret from the console
            int secret;
            Console.WriteLine("Sekret:");
            secret = Convert.ToInt32(Console.ReadLine());

            // encode the single secret
            Share[] shares = shamir.Encode(secret);

            // list the shares
            Console.WriteLine("Udziały:");
            foreach(Share share in shares)
            {
                Console.WriteLine(share.ToString());
            }

            // decrypted secret
            Console.WriteLine("Odtworzony sekret:");
            int restored = shamir.Decode(shares);
            Console.WriteLine(restored);

            // hold the screen
            Console.ReadKey(true);
        }
    }
}
