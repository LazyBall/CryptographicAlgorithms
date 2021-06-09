using System;
using System.Collections.Generic;
using System.Numerics;
using AlgorithmsLibrary;

namespace CryptographicAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //for(int i=0; i<10; i++)
            //{
            //    Console.WriteLine();
            //    (RSA.PublicKey pubK, RSA.PrivateKey prK) = RSA.GenerateKeys(8, 3);
            //    Console.WriteLine("Public key: E = {0}, N = {1}", pubK.E, pubK.N);
            //    Console.WriteLine("Private key: D = {0}, N = {1}", prK.D, prK.N);

            //    BigInteger mess = pubK.N / 2;
            //    Console.WriteLine("Message = {0}", mess);
            //    BigInteger cipher = RSA.Encrypt(mess, pubK);
            //    Console.WriteLine("Cipher Text = {0}", cipher);
            //    Console.WriteLine("Decrypt res = {0}", RSA.Decrypt(cipher, prK));
            //}

            List<(RSA.PublicKey pbK, BigInteger cipherText)> list = new();
            int count = 103;
            BigInteger message = int.MaxValue;
            Console.WriteLine(message);
            BigInteger mod = BigInteger.One;

            while (list.Count < count)
            {
                var pk = RSA.GenerateKeys(8, count).publicKey;
                if (BigInteger.GreatestCommonDivisor(mod, pk.N).IsOne)
                {
                    list.Add((pk, RSA.Encrypt(message, pk)));
                    mod *= pk.N;
                }
            }

            Console.WriteLine(HastadAttack.RecoverMessage(list));

            //List < ChineseRemainderTheorem.Congruence > list1 = new()
            //{
            //    (1,2),
            //    (2,3),
            //    (6,7)
            //};

            //Console.WriteLine(ChineseRemainderTheorem.SolveSequenceOfCongruence(list1).Remainder);
        }
    }
}
