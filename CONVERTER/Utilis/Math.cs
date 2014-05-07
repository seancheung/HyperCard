using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONVERTER.Utilis
{
	public class Math
	{
		/// <summary>
		/// Combination
		/// </summary>
		/// <param name="n"></param>
		/// <param name="m"></param>
		/// <returns></returns>
		public double Combin(int n, int m)
		{
			double num = 1.0;
			if (m > n - m)
			{
				m = n - m;
			}
			for (int i = m + 1; i <= n; i++)
			{
				num *= (double)i;
			}
			for (int i = n - m; i > 1; i--)
			{
				num /= (double)i;
			}
			return num;
		}

		public double Rr(int T, int H, int S, int L)
		{
			double num = 0.0;
			int num2 = H;
			while (num2 <= T + 6 && num2 <= 9)
			{
				num += this.Combin(L, num2) * this.Combin(S - L, T + 6 - num2) / this.Combin(S, T + 6);
				num2++;
			}
			return num;
		}
	}
}
