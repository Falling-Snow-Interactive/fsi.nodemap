using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes;
using Fsi.NodeMap.Nodes.Randomzier;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Fsi.NodeMap
{
    public abstract class Map<TEnum, TNode> 
        where TEnum : Enum
        where TNode : Node<TEnum>, new()
    {
        public TNode Root { get; private set; }
        public List<TNode> Nodes { get; private set; }
        public TNode End { get; private set; }

        protected Map(MapProperties<TEnum> properties)
        {
            Root = new TNode
                   {
                       position = new Vector2(properties.Dimensions.x / 2f, -2f),
                       type = properties.RootType
                   };
            Nodes = new List<TNode>();
            End = new TNode
                   {
                       position = new Vector2(properties.Dimensions.x / 2f, properties.Dimensions.y + 1),
                       type = properties.EndType
                   };
            
            NodeRandomizer<TEnum> nodeRandomizer = new NodeRandomizer<TEnum>();
            foreach (var entry in properties.Encounters)
            {
                nodeRandomizer.Add(entry);
            }
            
            for(int y = 0; y < properties.Dimensions.y; y++)
                for (int x = 0; x < properties.Dimensions.x; x++)
                {
                    TNode node = new TNode
                                 {
                                     position = new Vector2(x, y),
                                     type = nodeRandomizer.Randomize()
                                 };
                    Nodes.Add(node);
                }

            foreach(int p in properties.StartPoints)
            {
                int xPos = p;
                int yPos = 0;
                
                Vector2Int pos = new Vector2Int(xPos, yPos);

                if (TryGetNode(pos, out TNode currentNode))
                {
                    Root.Connect(currentNode);

                    while (yPos < properties.Dimensions.y - 1)
                    {
                        int xStep = Random.Range(-properties.PathStep.x, properties.PathStep.x + 1);
                        xPos += xStep;
                        xPos = Mathf.Clamp(xPos, 0, properties.Dimensions.x - 1);

                        yPos += Random.Range(1, properties.PathStep.y + 1);
                        yPos = Mathf.Clamp(yPos, 0, properties.Dimensions.y - 1);

                        pos = new Vector2Int(xPos, yPos);
                        if (TryGetNode(pos, out TNode connectionNode))
                        {
                            currentNode.Connect(connectionNode);

                            currentNode = connectionNode;
                        }
                    }

                    currentNode.Connect(End);
                }
            }
        }

        private bool TryGetNode(Vector2Int position, out TNode node)
        {
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
