using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RuleServer.Helpers
{
    public class ListEqualityComparer<TItem> : IEqualityComparer<List<TItem>>

    {
        public bool Equals(List<TItem> x, List<TItem> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode([DisallowNull] List<TItem> obj)
        {
            int hashcode = 0;
            foreach (TItem t in obj)
            {
                if (t == null)
                {
                    hashcode ^= 0;
                }
                else
                {
                    hashcode ^= t.GetHashCode();
                }
            }
            return hashcode;
        }
    }
}
