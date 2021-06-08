using System.Numerics;

namespace AlgorithmsLibrary
{
    class Helper
    {
		/// <summary>
		/// </summary>
		/// <returns><c>gcd = <paramref name="a"/> * <paramref name="x"/> +
		/// <paramref name="b"/> * <paramref name="y"/></c></returns>
		public static BigInteger ExtendedGCD(BigInteger a, BigInteger b,
			out BigInteger x, out BigInteger y) //gcd=ax+by
		{
			(BigInteger s, BigInteger old_s) = (0, 1);
			(BigInteger r, BigInteger old_r) = (b, a);

			while (!r.IsZero)
			{
				BigInteger q = BigInteger.DivRem(old_r, r, out BigInteger newRem);
				(old_r, r) = (r, newRem);
				(old_s, s) = (s, old_s - q * s);
			}

			x = old_s;
			y = b.IsZero ? 0 : (old_r - old_s * a) / b;
			if (old_r.Sign < 0)
			{
				old_r *= -1;
				x *= -1;
				y *= -1;
			}
			return old_r;
		}

		public static BigInteger GCD(BigInteger a, BigInteger b)
        {
			if (a.IsZero) return b;
			if (b.IsZero) return a;

			while (!b.IsZero)
            {
				a %= b;
				BigInteger temp = a;
				a = b;
				b = temp;
            }

			return a;
        }
	}
}