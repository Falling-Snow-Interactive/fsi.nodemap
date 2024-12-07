using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fsi.NodeMap.Nodes
{
    [Serializable]
    public class Node<TEnum>
        where TEnum : Enum
    {
        public Vector2 position;
        public TEnum type;

        public List<Node<TEnum>> next;
        public List<Node<TEnum>> prev;

        public Node()
        {
            next = new List<Node<TEnum>>();
            prev = new List<Node<TEnum>>();
        }

        public void Connect(Node<TEnum> target)
        {
            if (!next.Contains(target))
            {
                next.Add(target);
            }

            if (!target.prev.Contains(this))
            {
                target.prev.Add(target);
            }
        }
    }
}