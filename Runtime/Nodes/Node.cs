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

        public List<TNode> next;

        public Node()
        {
            next = new List<TNode>();
        }

        public void Connect(TNode target)
        {
            if (!next.Contains(target))
            {
                next.Add(target);
            }
        }
    }
}