using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RuleServer.Models.Expression;

namespace RuleServer.Helpers
{
    public class SimpleExpressionSubtreeEqualityComparer : IEqualityComparer<(ISimpleExpression, List<int>)>
    {
        private readonly SimpleExpressionNodeEqualityComparer nodeEqualityComparer;

        public SimpleExpressionSubtreeEqualityComparer(SimpleExpressionNodeEqualityComparer nodeEqualityComparer)
        {
            this.nodeEqualityComparer = nodeEqualityComparer;
        }

        public bool Equals((ISimpleExpression, List<int>) x, (ISimpleExpression, List<int>) y)
        {
            ISimpleExpression xNode = x.Item1;
            List<int> xSubtreeIds = x.Item2;
            ISimpleExpression yNode = y.Item1;
            List<int> ySubtreeIds = y.Item2;

            if (this.nodeEqualityComparer.Equals(xNode, yNode))
            {
                if (xSubtreeIds.Count == ySubtreeIds.Count)
                {
                    int i;
                    for (i = 0; i < xSubtreeIds.Count; i++)
                    {
                        if (xSubtreeIds[i] != ySubtreeIds[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public int GetHashCode([DisallowNull] (ISimpleExpression, List<int>) obj)
        {
            ISimpleExpression node = obj.Item1;
            List<int> subtreeIds = obj.Item2;

            int hashCodeSoFar = this.nodeEqualityComparer.GetHashCode(node);
            foreach (int subtreeId in subtreeIds)
            {
                hashCodeSoFar += subtreeId.GetHashCode();
            }
            return hashCodeSoFar;
        }
    }
}
