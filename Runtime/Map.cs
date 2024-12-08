using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.NodeMap
{
    public abstract class Map<TEnum, TNode> 
        where TEnum : Enum
        where TNode : Node<TEnum, TNode>, new()
    {
        public TNode Root { get; }
        public List<TNode> Nodes { get; }
        public TNode End { get; }

        private readonly MapProperties<TEnum> properties;
        
        protected Map(MapProperties<TEnum> properties, uint seed)
        {
            Random.InitState((int)seed);
            
            this.properties = properties;
            
            Root = new TNode
                   {
                       position = new Vector2Int(0, -1),
                       type = properties.RootType
                   };
            Nodes = new List<TNode>();
            End = new TNode
                   {
                       position = new Vector2Int(0, properties.Size.y),
                       type = properties.EndType
                   };
            
            // TODO - Generate mutiple paths - KD
            foreach (int start in this.properties.StartPoints)
            {
                TNode current = Root;
                int x = start;
                while (current != End)
                {
                    Vector2Int pos = current.position;
                    pos.x = x;
                    pos.y += 1;

                    if (!TryGetNode(pos, out TNode next))
                    {
                        next = new TNode
                               {
                                   position = pos,
                                   // TODO - Set node type - KD
                               };
                        Nodes.Add(next);
                    }

                    current.next.Add(next);
                    current = next;

                    var dirs = new List<int> { 0 };

                    if (x > 0)
                    {
                        dirs.Add(-1);
                    }

                    if (x < properties.Size.x - 1)
                    {
                        dirs.Add(1);
                    }

                    // TODO - Random needs to be deterministic eventually - KD
                    int dir = dirs[Random.Range(0, dirs.Count)];
                    x += dir;
                }
            }
        }

        public TNode GetRootNode()
        {
            return Root;
        }

        public TNode GetEndNode()
        {
            return End;
        }

        public bool TryGetNode(Vector2Int position, out TNode node)
        {
            if (position.y < 0)
            {
                node = Root;
                return true;
            }
            
            if (position.y >= properties.Size.y)
            {
                node = End;
                return true;
            }
            
            foreach (var entry in Nodes)
            {
                if (entry.position == position)
                {
                    node = entry;
                    return true;
                }
            }
            
            node = null;
            return false;
        }
    }
}
