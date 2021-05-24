using System.Collections.Generic;
using RuleEngine.Models.Expression;

namespace RuleEngine.Helpers
{
    public static class ExpressionTreeOptimizer
    {
        #region optimize expression trees
        private class SubtreeInfo
        {
            public int Count { get; set; } = 0;
            public ISimpleExpression Root { get; set; }
        }

        // return duplicated subtrees
        public static HashSet<ISimpleExpression> RemoveDuplicatedSubtrees(List<ISimpleExpression> roots)
        {
            HashSet<ISimpleExpression> duplicatedSubtrees;

            #region subtreeIds
            // {root, subtreeId of children} -> subtreeId of the root
            Dictionary<(ISimpleExpression, List<int>), int> subtreeIds;
            int GetSubtreeIdFromTree(ISimpleExpression root, List<int> childIds)
            {
                var tuple = (root, childIds);
                if (!subtreeIds.ContainsKey(tuple))
                {
                    subtreeIds[tuple] = subtreeIds.Count;
                }
                return subtreeIds[tuple];
            }
            #endregion
            #region subtreeInfos
            // subtreeId -> subtree info
            Dictionary<int, SubtreeInfo> subtreeInfos;
            // only save the root when it is the first count
            SubtreeInfo GetSubtreeInfo(int subtreeId, ISimpleExpression root)
            {
                if (!subtreeInfos.ContainsKey(subtreeId))
                {
                    subtreeInfos[subtreeId] = new()
                    {
                        Count = 0,
                        Root = root
                    };
                }
                return subtreeInfos[subtreeId];
            }
            #endregion
            // recursion
            int RemoveDuplicatedSubtreesRecursiveImpl(ISimpleExpression root)
            {
                if (root == null)
                {
                    return 0;
                }
                List<int> childIds = new();
                if (root is SimpleExpressionBinary binaryNode)
                {
                    var leftChildSubtreeId = RemoveDuplicatedSubtreesRecursiveImpl(binaryNode.LeftOperand);
                    childIds.Add(leftChildSubtreeId);
                    var rightChildSubtreeId = RemoveDuplicatedSubtreesRecursiveImpl(binaryNode.RightOperand);
                    childIds.Add(rightChildSubtreeId);

                    // replace children
                    binaryNode.LeftOperand = subtreeInfos[leftChildSubtreeId].Root;
                    binaryNode.RightOperand = subtreeInfos[rightChildSubtreeId].Root;
                }
                else if (root is SimpleExpressionUnary unaryNode)
                {
                    var childSubtreeId = RemoveDuplicatedSubtreesRecursiveImpl(unaryNode.Operand);
                    childIds.Add(childSubtreeId);

                    // replace child
                    unaryNode.Operand = subtreeInfos[childSubtreeId].Root;
                }
                int subtreeId = GetSubtreeIdFromTree(root, childIds);

                // count
                SubtreeInfo subtreeInfo = GetSubtreeInfo(subtreeId, root);
                subtreeInfo.Count++;

                // `node` is marked as it is about to be replaced
                if (subtreeInfo.Count > 1)
                {
                    // mark the original node as duplicated
                    if (subtreeInfo.Root is SimpleExpressionBinary ||
                        subtreeInfo.Root is SimpleExpressionUnary)
                    {
                        duplicatedSubtrees.Add(subtreeInfo.Root);
                    }
                }

                return subtreeId;
            }

            // initiate
            subtreeInfos = new();
            SimpleExpressionNodeEqualityComparer nodeEqualityComparer = new();
            SimpleExpressionSubtreeEqualityComparer subtreeEqualityComparer = new(nodeEqualityComparer);
            subtreeIds = new(subtreeEqualityComparer);
            duplicatedSubtrees = new();

            // remove duplicated subtrees
            int i;
            for (i = 0; i < roots.Count; i++)
            {
                ISimpleExpression root = roots[i];
                var rootSubtreeId = RemoveDuplicatedSubtreesRecursiveImpl(root);

                // replace root
                roots[i] = subtreeInfos[rootSubtreeId].Root;
            }
            return duplicatedSubtrees;
        }
        #endregion
    }
}
