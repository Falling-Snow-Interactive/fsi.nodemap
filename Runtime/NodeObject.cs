using System;
using Fsi.NodeMap.Nodes;
using UnityEngine;

namespace Fsi.NodeMap
{
    public abstract class NodeObject<TEnum, TNode> : MonoBehaviour
        where TEnum : Enum
        where TNode : Node<TEnum, TNode>
    {
        public TNode Node { get; private set; }

        public virtual void SetNode(TNode node)
        {
            Node = node;
        }
    }
}
