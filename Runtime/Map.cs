using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes;
using Fsi.NodeMap.Nodes.Randomzier;
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
        
        public uint Seed { get; }

        private MapProperties<TEnum> Properties { get; }
        
        protected Map(MapProperties<TEnum> properties, uint seed)
        {
            Random.InitState((int)seed);
            Seed = seed;
            
            Properties = properties;
            
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
            
            
            var nodeRandomizer = new NodeRandomizer<TEnum>();
            foreach (NodeRandomizerEntry<TEnum> nodeEntry in properties.NodeTypes)
            {
                nodeRandomizer.Add(nodeEntry);
            }
            foreach (int start in Properties.StartPoints)
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
                                   type = nodeRandomizer.Randomize(),
                               };
                        Nodes.Add(next);
                    }

                    current.AddNext(next);
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

        public bool TryGetNode(Vector2Int position, out TNode node)
        {
            if (position.y < 0)
            {
                node = Root;
                return true;
            }
            
            if (position.y >= Properties.Size.y)
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

        public override string ToString()
        {
            var s = $"Map: {Seed}";
            return s;
        }
    }
}
