using System;
using System.Collections.Generic;
using System.Numerics;

namespace AlgorithmsLibrary
{
    public class ChineseRemainderTheorem
    {
        public struct Congruence
        {
            public BigInteger Modulus { get; set; }

            public BigInteger Remainder { get; set; }

            public Congruence(BigInteger r, BigInteger a)
            {
                this.Remainder = r;
                this.Modulus = a;
            }

            public static implicit operator Congruence((BigInteger r, BigInteger m) item)
            {
                return new Congruence(item.r, item.m);
            }
        }

        public static Congruence SolveSequenceOfCongruence(IEnumerable<Congruence> sequence)
        {
            BigInteger m = BigInteger.One;

            foreach (Congruence congruence in sequence)
            {
                if (!BigInteger.GreatestCommonDivisor(m, congruence.Modulus).IsOne)
                {
                    throw new ArgumentException();
                }
                m *= congruence.Modulus; 
            }

            BigInteger x = BigInteger.Zero;

            foreach (Congruence congruence in sequence)
            {
                BigInteger m_i = m / congruence.Modulus;
                Helper.ExtendedGCD(m_i % congruence.Modulus, congruence.Modulus,
                    out BigInteger revM_i, out _);
                if (revM_i.Sign < 0) revM_i += congruence.Modulus;
                x += congruence.Remainder * m_i * revM_i;
                x %= m;
            }

            return new Congruence() { Remainder = x, Modulus = m };
        }
    }
}
