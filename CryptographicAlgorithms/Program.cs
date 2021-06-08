using System;
using System.Numerics;
using AlgorithmsLibrary;

namespace CryptographicAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i=0; i<10; i++)
            {
                Console.WriteLine();
                (RSA.PublicKey pubK, RSA.PrivateKey prK) = RSA.GenerateKeys(8, 3);
                Console.WriteLine("Public key: E = {0}, N = {1}", pubK.E, pubK.N);
                Console.WriteLine("Private key: D = {0}, N = {1}", prK.D, prK.N);

                BigInteger mess = pubK.N / 2;
                Console.WriteLine("Message = {0}", mess);
                BigInteger cipher = RSA.Encrypt(mess, pubK);
                Console.WriteLine("Cipher Text = {0}", cipher);
                Console.WriteLine("Decrypt res = {0}", RSA.Decrypt(cipher, prK));
            }
            
        }
    }
}
