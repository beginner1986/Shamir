using System;
using Org.BouncyCastle.Math;

namespace Shamir
{
    class Program
    {
        static void Main(string[] args)
        {
            // introduction
            Console.WriteLine("Adam Emieljaniuk, N2C");
            Console.WriteLine("Kryptologia: Laoratorium nr 2 - podział sekretów.\n");

            // ------------------ INT PART ------------------
            // create the encoder / decoder object
            Shamir shamir = new Shamir(101, 10, 4);

            // get the secret from the console
            int secret;
            Console.WriteLine("Sekret:");
            secret = Convert.ToInt32(Console.ReadLine());

            // encrypt the single secret
            Share[] shares = shamir.Encrypt(secret);

            // list the shares
            Console.WriteLine("\nUdziały:");
            foreach(Share share in shares)
            {
                Console.WriteLine(share.ToString());
            }

            // decrypted secret
            Console.WriteLine();
            Console.WriteLine("Odtworzony sekret:");
            int restored = shamir.Decrypt(shares);
            Console.WriteLine(restored);
            Console.WriteLine();
            // ------------------ /INT PART ------------------

            // ---------------- BIG INT PART ------------------
            
            // secret value limitation
            BigInteger maxSecretValue = new BigInteger("99999999999999999999999999999999999999999999999999");
            // get new random secret
            BigInteger secretBigInt = Util.RandomBigInteger(maxSecretValue);
            // generate BigInteger prime number
            BigInteger bigP = BigInteger.ProbablePrime(secretBigInt.BitLength + 1, new Random()).Abs();

            // BigInteger encryptor / decryptor object
            ShamirBigInt shamirBigInt = new ShamirBigInt(bigP, 10, 4);
            Console.WriteLine("Sekret: ");
            Console.WriteLine(secretBigInt.ToString());

            // encrypt the BigInteger secret
            ShareBigInt[] sharesBigInt = shamirBigInt.Encrypt(secretBigInt);

            // list the shares
            Console.WriteLine("\nUdziały:");
            foreach (ShareBigInt share in sharesBigInt)
            {
                Console.WriteLine(share.ToString());
            }

            // decrypted secret
            Console.WriteLine();
            Console.WriteLine("Odtworzony sekret:");
            BigInteger restoredBigInt = shamirBigInt.Decrypt(sharesBigInt);
            Console.WriteLine(restoredBigInt);
            // ---------------- /BIG INT PART ------------------

            // hold the screen
            Console.ReadKey(true);
        }
    }
}
