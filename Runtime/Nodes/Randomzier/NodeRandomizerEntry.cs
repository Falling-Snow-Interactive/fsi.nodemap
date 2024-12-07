using System;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace Fsi.NodeMap.Nodes.Randomzier
{
    [Serializable]
    public class NodeRandomizerEntry<TEnum> : RandomizerEntry<TEnum>, ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name = "";
        
        [SerializeField]
        private int weight;
        public override int Weight => weight;

        [SerializeField]
        private TEnum value;
        public override TEnum Value => value;
        
        public void OnBeforeSerialize()
        {
            name = $"{Value} - {Weight}";
        }

        public void OnAfterDeserialize() { }
    }
}