using System;
using System.Collections.Generic;
using Fsi.NodeMap.Nodes.Randomzier;
using UnityEngine;

namespace Fsi.NodeMap
{
    public abstract class MapProperties<TEnum> : ScriptableObject
        where TEnum : Enum
    {
        [SerializeField]
        private List<int> startPoints = new List<int>();
        
        public List<int> StartPoints => startPoints;
        
        [SerializeField]
        private Vector2Int dimensions;
        
        public Vector2Int Dimensions => dimensions;
        
        [SerializeField]
        private Vector2Int pathStep = new Vector2Int(2, 1);
        
        public Vector2Int PathStep => pathStep;
        
        [SerializeField]
        private List<NodeRandomizerEntry<TEnum>> encounters = new();
        
        public List<NodeRandomizerEntry<TEnum>> Encounters => encounters;

        [SerializeField]
        private TEnum rootType;
        public TEnum RootType => rootType;

        [SerializeField]
        private TEnum endType;
        public TEnum EndType => endType;
    }
}