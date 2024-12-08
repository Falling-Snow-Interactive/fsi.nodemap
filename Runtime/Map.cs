using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.NodeMap
{
    [Serializable]
    public abstract class Map<TEnum, TNode> 
        where TEnum : Enum
        where TNode : Node<TEnum, TNode>, new()
    {
        public TNode root;
        public List<TNode> nodes;
        public TNode end;

        private MapProperties<TEnum> properties;

        [SerializeField]
        private uint seed;

        protected Map(MapProperties<TEnum> properties, uint seed)
        {
            Random.InitState((int)seed);
            
            this.properties = properties;
            this.seed = seed;
            
            root = new TNode
                   {
                       position = new Vector2Int(0, -1),
                       type = properties.RootType
                   };
            nodes = new List<TNode>();
            end = new TNode
                   {
                       position = new Vector2Int(0, properties.Size.y),
                       type = properties.EndType
                   };
            
            // TODO - Generate mutiple paths - KD
            foreach (int start in this.properties.StartPoints)
            {
                TNode current = root;
                int x = start;
                while (current != end)
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
                        nodes.Add(next);
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
            return root;
        }

        public TNode GetEndNode()
        {
            return end;
        }

        public bool TryGetNode(Vector2Int position, out TNode node)
        {
            if (position.y < 0)
            {
                node = root;
                return true;
            }
            
            if (position.y >= properties.Size.y)
            {
                node = end;
                return true;
            }
            
            foreach (var entry in nodes)
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
