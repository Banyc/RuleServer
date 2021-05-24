using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RuleEngine.Models.Expression;

namespace RuleEngine.Helpers
{
    public class SimpleExpressionTreeEqualityComparer : IEqualityComparer<(ISimpleExpression, List<int>)>
    {
        private readonly SimpleExpressionNodeEqualityComparer nodeEqualityComparer;

        public SimpleExpressionTreeEqualityComparer(SimpleExpressionNodeEqualityComparer nodeEqualityComparer)
        {
            this.nodeEqualityComparer = nodeEqualityComparer;
        }

        public bool Equals((ISimpleExpression, List<int>) x, (ISimpleExpression, List<int>) y)
        {
            ISimpleExpression xNode = x.Item1;
            List<int> xTreeIds = x.Item2;
            ISimpleExpression yNode = y.Item1;
            List<int> yTreeIds = y.Item2;

            if (this.nodeEqualityComparer.Equals(xNode, yNode))
            {
                if (xTreeIds.Count == yTreeIds.Count)
                {
                    int i;
                    for (i = 0; i < xTreeIds.Count; i++)
                    {
                        if (xTreeIds[i] != yTreeIds[i])
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
            List<int> treeIds = obj.Item2;

            int hashCodeSoFar = this.nodeEqualityComparer.GetHashCode(node);
            foreach (int subtreeId in treeIds)
            {
                hashCodeSoFar += subtreeId.GetHashCode();
            }
            return hashCodeSoFar;
        }
    }
}
