using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsi.NodeMap.Nodes
{
    public class Node<TEnum, TNode>
        where TEnum : Enum
        where TNode : Node<TEnum, TNode>
    {
        public Vector2Int position;
        public TEnum type;

        private readonly List<TNode> next;
        public List<TNode> Next => next;

        public Node()
        {
            next = new List<TNode>();
        }

        public void AddNext(TNode node)
        {
            if(next.Count == 0)
            {
                next.Add(node);
                return;
            }
            
            for (int i = 0; i < next.Count; i++)
            {
                var n = next[i];
                if (node.position.x <= n.position.x)
                {
                    next.Insert(i, node);
                    return;
                }
            }

            next.Add(node);
        }

        public override string ToString()
        {
            var s = "Node\n";
            s += $"\tType: {type}\n";
            s += $"\tPosition: {position}\n";
            s += $"\tConnections: {next.Count}\n";
            return s;
        }
    }
}