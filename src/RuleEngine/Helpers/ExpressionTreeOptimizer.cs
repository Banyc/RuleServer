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
            Dictionary<ISimpleExpression, ISimpleExpression> replacements;

            #region subtreeIds
            // {node, subtreeId of children} -> subtreeId of the node
            Dictionary<(ISimpleExpression, List<int>), int> subtreeIds;
            int GetSubtreeIdFromTree(ISimpleExpression node, List<int> childIds)
            {
                var tuple = (node, childIds);
                if (!subtreeIds.ContainsKey(tuple))
                {
                    subtreeIds[tuple] = subtreeIds.Count;
                }
                return subtreeIds[tuple];
            }
            #endregion
            #region counter
            // subtreeId -> subtree info
            Dictionary<int, SubtreeInfo> subtreeInfos;
            // only save the node when it is the first count
            SubtreeInfo GetSubtreeInfo(int subtreeId, ISimpleExpression node)
            {
                if (!subtreeInfos.ContainsKey(subtreeId))
                {
                    subtreeInfos[subtreeId] = new()
                    {
                        Count = 0,
                        Root = node
                    };
                }
                return subtreeInfos[subtreeId];
            }
            #endregion
            // recursion
            int ReplaceDuplicatedSubtressRecursiveImpl(ISimpleExpression node)
            {
                if (node == null)
                {
                    return 0;
                }
                List<int> childIds = new();
                if (node is SimpleExpressionBinary binaryNode)
                {
                    childIds.Add(ReplaceDuplicatedSubtressRecursiveImpl(binaryNode.LeftOperand));
                    childIds.Add(ReplaceDuplicatedSubtressRecursiveImpl(binaryNode.RightOperand));

                    // replace children
                    if (replacements.ContainsKey(binaryNode.LeftOperand))
                    {
                        binaryNode.LeftOperand = replacements[binaryNode.LeftOperand];
                    }
                    if (replacements.ContainsKey(binaryNode.RightOperand))
                    {
                        binaryNode.RightOperand = replacements[binaryNode.RightOperand];
                    }
                }
                else if (node is SimpleExpressionUnary unaryNode)
                {
                    childIds.Add(ReplaceDuplicatedSubtressRecursiveImpl(unaryNode.Operand));

                    // replace child
                    if (replacements.ContainsKey(unaryNode.Operand))
                    {
                        unaryNode.Operand = replacements[unaryNode.Operand];
                    }
                }
                int subtreeId = GetSubtreeIdFromTree(node, childIds);

                // count
                SubtreeInfo subtreeInfo = GetSubtreeInfo(subtreeId, node);
                subtreeInfo.Count++;

                // `node` is marked as it is about to be replaced
                if (subtreeInfo.Count > 1)
                {
                    replacements[node] = subtreeInfo.Root;
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
            replacements = new();
            duplicatedSubtrees = new();

            // remove duplicated subtrees
            int i;
            for (i = 0; i < roots.Count; i++)
            {
                ISimpleExpression root = roots[i];
                ReplaceDuplicatedSubtressRecursiveImpl(root);

                // replace root
                if (replacements.ContainsKey(root))
                {
                    roots[i] = replacements[root];
                }
            }
            return duplicatedSubtrees;
        }
        #endregion
    }
}
