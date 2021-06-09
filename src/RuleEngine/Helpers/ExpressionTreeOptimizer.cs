using System.Collections.Generic;
using RuleEngine.Models.Expression;

namespace RuleEngine.Helpers
{
    public static class ExpressionTreeOptimizer
    {
        #region optimize expression trees
        private class TreeInfo
        {
            public int Count { get; set; } = 0;
            public ISimpleExpression Root { get; set; }
        }

        // return duplicated subtrees
        public static HashSet<ISimpleExpression> RemoveDuplicatedSubtrees(List<ISimpleExpression> roots)
        {
            HashSet<ISimpleExpression> duplicatedSubtrees;

            #region treeIds
            // {root, treeId of children} -> treeId of the root
            Dictionary<(ISimpleExpression, List<int>), int> treeIds;
            int CreateOrGetTreeId(ISimpleExpression root, List<int> childTreeIds)
            {
                var tuple = (root, childTreeIds);
                if (!treeIds.ContainsKey(tuple))
                {
                    treeIds[tuple] = treeIds.Count + 1;
                }
                return treeIds[tuple];
            }
            #endregion
            #region treeInfos
            // treeId -> treeInfo
            Dictionary<int, TreeInfo> treeInfos;
            // only save the root when it is the first count
            TreeInfo CreateOrGetTreeInfo(int treeId, ISimpleExpression root)
            {
                if (!treeInfos.ContainsKey(treeId))
                {
                    treeInfos[treeId] = new()
                    {
                        Count = 0,
                        Root = root
                    };
                }
                return treeInfos[treeId];
            }
            #endregion
            // recursion
            int RemoveDuplicatedSubtreesRecursiveImpl(ISimpleExpression root)
            {
                if (root == null)
                {
                    return 0;
                }
                List<int> childTreeIds = new();
                if (root is SimpleExpressionBinary binaryNode)
                {
                    var leftChildTreeId = RemoveDuplicatedSubtreesRecursiveImpl(binaryNode.LeftOperand);
                    childTreeIds.Add(leftChildTreeId);
                    var rightChildTreeId = RemoveDuplicatedSubtreesRecursiveImpl(binaryNode.RightOperand);
                    childTreeIds.Add(rightChildTreeId);

                    // replace children
                    binaryNode.LeftOperand = treeInfos[leftChildTreeId].Root;
                    binaryNode.RightOperand = treeInfos[rightChildTreeId].Root;
                }
                else if (root is SimpleExpressionUnary unaryNode)
                {
                    var childTreeId = RemoveDuplicatedSubtreesRecursiveImpl(unaryNode.Operand);
                    childTreeIds.Add(childTreeId);

                    // replace child
                    unaryNode.Operand = treeInfos[childTreeId].Root;
                }
                int treeId = CreateOrGetTreeId(root, childTreeIds);

                // count
                TreeInfo treeInfo = CreateOrGetTreeInfo(treeId, root);
                treeInfo.Count++;

                // `node` is marked as it is about to be replaced
                if (treeInfo.Count > 1)
                {
                    // mark the original node as duplicated
                    if (treeInfo.Root is SimpleExpressionBinary ||
                        treeInfo.Root is SimpleExpressionUnary)
                    {
                        duplicatedSubtrees.Add(treeInfo.Root);
                    }
                }

                return treeId;
            }

            // initiate
            treeInfos = new();
            SimpleExpressionNodeEqualityComparer nodeEqualityComparer = new();
            SimpleExpressionTreeEqualityComparer treeEqualityComparer = new(nodeEqualityComparer);
            treeIds = new(treeEqualityComparer);
            duplicatedSubtrees = new();

            // remove duplicated subtrees
            int i;
            for (i = 0; i < roots.Count; i++)
            {
                ISimpleExpression root = roots[i];
                var rootTreeId = RemoveDuplicatedSubtreesRecursiveImpl(root);

                // replace root
                roots[i] = treeInfos[rootTreeId].Root;
            }
            return duplicatedSubtrees;
        }
        #endregion
    }
}
