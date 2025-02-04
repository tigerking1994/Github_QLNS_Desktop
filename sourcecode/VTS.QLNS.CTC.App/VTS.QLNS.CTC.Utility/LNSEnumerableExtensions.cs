using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class LNSEnumerableExtensions
    {
        public static List<string> SplitLNS(IEnumerable<string> listLNS)
        {
            List<string> result = new List<string>();
            foreach (var lns in listLNS.Where(x => x.Length > 5))
            {
                string s1 = lns.Substring(0, 1);
                string s3 = lns.Substring(0, 3);
                string s5 = lns.Substring(0, 5);
                if (!result.Contains(s1)) result.Add(s1);
                if (!result.Contains(s3)) result.Add(s3);
                if (!result.Contains(s5)) result.Add(s5);
                if (!result.Contains(lns)) result.Add(lns);
            }
            return result;
        }
    }
}
