using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORMATTER
{
    public class ManaExtractor
    {
        /// <summary>
        /// Split text by mana expressions
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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

            if (idx.Count > 0)
            {
                if (idx[0] > 0) yield return text.Substring(0, idx[0]);
            }
            else
            {
                yield return text;
                yield break;
            }
            
            for (int i = 0; i < idx.Count; i++)
            {
                if (i + 1 >= idx.Count) break;
                if (idx[i] + 1 == idx[i + 1]) continue;

                var s = text.Substring(idx[i], idx[i + 1] - idx[i] + 1);
                if (s.StartsWith("}")) s = s.Substring(1);
                if (s.EndsWith("{")) s = s.Remove(s.Length - 1);

                yield return s;
            }

            yield return text.Substring(idx[idx.Count - 1] + 1);
        }
    }
}
