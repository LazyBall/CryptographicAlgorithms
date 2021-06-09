using System;
using System.Collections.Generic;
using System.Numerics;

namespace AlgorithmsLibrary
{
    public class HastadAttack
    {
       
        public static BigInteger RecoverMessage
            (IEnumerable<(RSA.PublicKey key, BigInteger cipherText)> data)
        {
            List<ChineseRemainderTheorem.Congruence> congruences = new();

            foreach(var (key, cipherText) in data)
            {
                congruences.Add(new ChineseRemainderTheorem.Congruence(cipherText, key.N));
            }

            var answer = ChineseRemainderTheorem.SolveSequenceOfCongruence(congruences);

            return FindRoot(answer.Remainder, congruences.Count);
        }


        private static BigInteger FindRoot(BigInteger number, int degree)
        {
            BigInteger root = new(Math.Pow(Math.E, BigInteger.Log(number) / degree));
            BigInteger rootInDegree = BigInteger.Pow(root, degree);
            int compRes = BigInteger.Compare(rootInDegree, number);

            while(compRes != 0)
            {
                if (compRes < 0) root++;
                else root--;
                rootInDegree = BigInteger.Pow(root, degree);
                compRes = BigInteger.Compare(rootInDegree, number);
            }

            return root;
        }
    }
}
