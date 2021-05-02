using System.Collections.Generic;
using RuleServer.Models.Expression;

namespace RuleServer.Helpers
{
    public static class ExpressionTreeOptimizer
    {
        #region optimize expression trees
        private class SubtreeCounter
        {
            public int Count { get; set; } = 0;
            public ISimpleExpression Node { get; set; }
        }

        // return duplicated subtrees
        private static HashSet<ISimpleExpression> RemoveDuplicatedSubtrees(List<ISimpleExpression> roots)
        {
            HashSet<ISimpleExpression> duplicatedSubtrees;
            Dictionary<ISimpleExpression, ISimpleExpression> replacements;

            #region subtreeIds
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
            Dictionary<int, SubtreeCounter> subtreeCounters;
            // only save the node when it is the first count
            SubtreeCounter GetSubtreeCounter(int subtreeId, ISimpleExpression node)
            {
                if (!subtreeCounters.ContainsKey(subtreeId))
                {
                    subtreeCounters[subtreeId] = new()
                    {
                        Count = 0,
                        Node = node
                    };
                }
                return subtreeCounters[subtreeId];
            }
            #endregion
            // recursion
            int GetSubtreeIdFromNodeAndCount(ISimpleExpression node)
            {
                if (node == null)
                {
                    return 0;
                }
                List<int> childIds = new();
                if (node is SimpleExpressionBinary binaryNode)
                {
                    childIds.Add(GetSubtreeIdFromNodeAndCount(binaryNode.LeftOperand));
                    childIds.Add(GetSubtreeIdFromNodeAndCount(binaryNode.RightOperand));

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
                    childIds.Add(GetSubtreeIdFromNodeAndCount(unaryNode.Operand));

                    // replace child
                    if (replacements.ContainsKey(unaryNode.Operand))
                    {
                        unaryNode.Operand = replacements[unaryNode.Operand];
                    }
                }
                int subtreeId = GetSubtreeIdFromTree(node, childIds);

                // count
                SubtreeCounter counter = GetSubtreeCounter(subtreeId, node);
                counter.Count++;

                // `node` is marked as it is about to be replaced
                if (counter.Count > 1)
                {
                    replacements[node] = counter.Node;
                    // mark the original node as duplicated
                    if (counter.Node is SimpleExpressionBinary ||
                        counter.Node is SimpleExpressionUnary)
                    {
                        duplicatedSubtrees.Add(counter.Node);
                    }
                }

                return subtreeId;
            }

            // initiate
            subtreeCounters = new();
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
                GetSubtreeIdFromNodeAndCount(root);

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
