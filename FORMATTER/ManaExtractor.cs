using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORMATTER
{
    public class ManaExtractor
    {
        public static IEnumerable<string> Extract(string text)
        {
            List<int> idx = new List<int>();

            int t = 0;
            while (text.IndexOf("{") >= 0)
            {
                int a = text.IndexOf("{", t);
                if (a >= 0)
                {
                    idx.Add(a);
                    int b = text.IndexOf("}", a);
                    if (b >= 0) idx.Add(b);
                    else break;

                    t = b;
                }
                else break;
            }

            for (int i = 0; i < idx.Count; i++)
            {
                yield return text.Substring(idx[i], idx[i + 1] - idx[i] + 1);
                i++;
            }
        }
    }
}
