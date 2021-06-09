using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLibrary
{
    class LLL_algorithm
    {
        public List<List<float>> b;
        public LLL_algorithm(List<List<float>> basis, float parameter)
        {
            b = new List<List<float>>(basis);
            List<List<float>> nb = GramSchmidt(basis);
            int k = 1;
            while (k < b.Count)
            {
                for (int j = k - 1; j >= 0; j--)
                {
                    var coef = coefGS(b[k], nb[j]);
                    if (Math.Abs(coef) > 0.5f)
                    {
                        for (int i = 0; i < b[k].Count; i++)
                        {
                            b[k][i] -= (float)Math.Round(coef, 0) * b[j][i];
                        }
                    }
                }
                var coefgs = coefGS(b[k], b[k - 1]);
                if ((parameter - coefgs * coefgs) * scalar_product(b[k - 1], b[k - 1]) <= scalar_product(b[k], b[k]))
                {
                    k++;
                }
                else
                {
                    var temp = b[k];
                    b[k] = b[k - 1];
                    b[k - 1] = temp;
                    k = Math.Max(k - 1, 1);
                    nb = GramSchmidt(b);
                }
            }
        }
        public List<List<float>> Answer ()
        {
            return b;
        }
        private List<List<float>> GramSchmidt(List<List<float>> a)
        {
            List<List<float>> b = new List<List<float>>();
            for (int i = 0; i < a.Count; i++)
            {
                b.Add(new List<float>(a[i]));
                for (int j = 0; j < b.Count - 1; j++)
                {
                    var pr = proj(a[i], b[j]);
                    for (int k = 0; k < pr.Count; k++)
                        b[i][k] -= pr[k];
                }
            }
            return b;
        }
        private float coefGS(List<float> a, List<float> b)
        {
            return scalar_product(a, b) / scalar_product(b, b);
        }
        private List<float> proj(List<float> a, List<float> b)
        {
            List<float> new_b = new List<float>();
            float coef = coefGS(a, b);
            for (int i = 0; i < b.Count; i++)
                new_b.Add(coef * b[i]);
            return new_b;
        }
        private float scalar_product(List<float> a, List<float> b)
        {
            float sum = 0.0f;
            for (int i = 0; i < a.Count; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }
    }
}
