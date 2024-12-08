using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes.Randomzier;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fsi.NodeMap
{
    public abstract class MapProperties<TEnum> : ScriptableObject
        where TEnum : Enum
    {
        [SerializeField]
        private List<int> startPoints = new();
        public List<int> StartPoints => startPoints;
        
        [SerializeField]
        private Vector2Int size;
        public Vector2Int Size => size;
        
        [SerializeField]
        private Vector2Int pathStep = new Vector2Int(2, 1);
        public Vector2Int PathStep => pathStep;

        [SerializeField]
        private TEnum rootType;
        public TEnum RootType => rootType;

        [SerializeField]
        private TEnum endType;
        public TEnum EndType => endType;
        
        [SerializeField]
        private List<NodeRandomizerEntry<TEnum>> nodeTypes = new();
        public List<NodeRandomizerEntry<TEnum>> NodeTypes => nodeTypes;
    }
}