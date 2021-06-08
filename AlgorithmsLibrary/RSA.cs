using System;
using System.Numerics;
using Open.Numeric.Primes;

namespace AlgorithmsLibrary
{
    public class RSA
    {
        public struct PublicKey
        {
            public BigInteger E { get; private set; }
            public BigInteger N { get; private set; }

            public PublicKey(BigInteger E, BigInteger N)
            {
                if (E <= 0) throw new ArgumentException("E must be positive.");
                if (N <= 0) throw new ArgumentException("N must be positive.");
                if (E >= N) throw new ArgumentException("N must be greater than E.");
                this.E = E;
                this.N = N;
            }
        }

        public struct PrivateKey
        {
            public BigInteger D { get; private set; }
            public BigInteger N { get; private set; }

            public PrivateKey(BigInteger D, BigInteger N)
            {
                if (D <= 0) throw new ArgumentException("D must be positive.");
                if (N <= 0) throw new ArgumentException("N must be positive.");
                if (D >= N) throw new ArgumentException("N must be greater than D.");
                this.D = D;
                this.N = N;
            }
        }

        static public (PublicKey publicKey, PrivateKey privateKey) GenerateKeys(int sizeInBytes)
        {
            if (sizeInBytes < 0) throw new ArgumentException(
                $"{nameof(sizeInBytes)} must be positive.");

            BigInteger p, q;

            do
            {
                p = GeneratePrimeNumber(sizeInBytes);
                q = GeneratePrimeNumber(sizeInBytes);
            } while (p == q);

            BigInteger n = p * q;
            BigInteger phi_n = (p - 1) * (q - 1);
            (BigInteger e, BigInteger d) = GenerateOpenAndSecretExponent(phi_n);
            return (new PublicKey(e,n), new PrivateKey(d,n));
        }

        static public (PublicKey publicKey, PrivateKey privateKey) GenerateKeys(int sizeInBytes,
            BigInteger fixedE)
        {
            if (sizeInBytes < 0) throw new ArgumentException(
                $"{nameof(sizeInBytes)} must be positive.");
            if (fixedE <= 0) throw new ArgumentException();
            
            BigInteger n, phi_n, d;
            bool isEqual;

            do
            {
                BigInteger p = GeneratePrimeNumber(sizeInBytes);
                BigInteger q = GeneratePrimeNumber(sizeInBytes);
                isEqual = (p == q);
                n = p * q;
                phi_n = (p - 1) * (q - 1);
            } while (isEqual || !Helper.ExtendedGCD(fixedE, phi_n, out d, out _).IsOne);

            if (d.Sign < 0) d += phi_n;
            return (new PublicKey(fixedE, n), new PrivateKey(d, n));
        }

        private static BigInteger GeneratePrimeNumber(int sizeInBytes)
        {
            byte[] buffer = new byte[sizeInBytes];
            Random random = new(DateTime.Now.Millisecond);
            BigInteger number;

            do
            {
                random.NextBytes(buffer);
                number = new BigInteger(buffer);
                if (number.Sign < 0) number = -number;
            } while (!Number.IsPrime(number));

            return number;
        }

        private static (BigInteger e, BigInteger d) GenerateOpenAndSecretExponent(BigInteger phi_n)
        {
            Random random = new(DateTime.Now.Millisecond);
            byte[] buffer = new byte[phi_n.ToByteArray().Length];
            BigInteger e, d;

            do
            {
                random.NextBytes(buffer);
                e = new BigInteger(buffer);
                if (e.Sign < 0) e = -e;
            } while (!((!e.IsZero) && e < phi_n &&
                        Helper.ExtendedGCD(e, phi_n, out d, out _).IsOne));

            if (d.Sign < 0) d += phi_n;
            return (e, d);
        }

        public static BigInteger Encrypt(BigInteger message, PublicKey publicKey)
        {
            if (message >= publicKey.N) throw new 
                    ArgumentException("N must be greater than message.");

            return BigInteger.ModPow(message, publicKey.E, publicKey.N);
        }

        public static BigInteger Decrypt(BigInteger cipherText, PrivateKey privateKey)
        {
            if (cipherText >= privateKey.N) throw new 
                    ArgumentException("N must be greater than cipherText.");

            return BigInteger.ModPow(cipherText, privateKey.D, privateKey.N);
        }
    }
}
