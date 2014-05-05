using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperCard.Classes
{
	internal class ManaIcon
	{
		/// <summary>
		/// Get mana icon path
		/// </summary>
		/// <param name="mana"></param>
		/// <returns></returns>
		public static string GetIconPath(string mana)
		{
			mana = mana.Replace("{", string.Empty).Replace("}", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).ToLower();
			if (mana.Length > 1)
			{
				mana = mana.Replace("gb", "bg").Replace("ug", "gu").Replace("wg", "gw").Replace("gr", "rg").Replace("bu", "ub").Replace("ru", "ur").Replace("bw", "wb").Replace("uw", "wu");
			}
			return String.Format("pack://application:,,,/Resources/mana_{0}.png", mana);
		}
	}
}
