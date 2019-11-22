using System;

namespace Shamir
{
    class Program
    {
        static void Main(string[] args)
        {
            // introduction
            Console.WriteLine("Adam Emieljaniuk, N2C");
            Console.WriteLine("Kryptologia: Laoratorium nr 2 - podział sekretów.");

            // create the encoder / decoder object
            Shamir shamir = new Shamir(101, 10, 4);

            // get the secret from the console
            int secret;
            Console.WriteLine("Sekret:");
            secret = Convert.ToInt32(Console.ReadLine());

            // encrypt the single secret
            Share[] shares = shamir.Encrypt(secret);

            // list the shares
            Console.WriteLine("Udziały:");
            foreach(Share share in shares)
            {
                Console.WriteLine(share.ToString());
            }

            // decrypted secret
            // Share[] test = { shares[1], shares[2], shares[4] };
            Console.WriteLine();
            Console.WriteLine("Odtworzony sekret:");
            int restored = shamir.Decrypt(shares);
            Console.WriteLine("\n\n" + restored);

            // hold the screen
            Console.ReadKey(true);
        }
    }
}
