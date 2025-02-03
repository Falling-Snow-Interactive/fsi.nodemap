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
        public override int Weight
        {
            get => weight;
            set => weight = value;
        }

        [SerializeField]
        private TEnum value;
        public override TEnum Value
        {
            get => value;
            set => this.value = value;
        }

        public void OnBeforeSerialize()
        {
            name = $"{Value} - {Weight}";
        }

        public void OnAfterDeserialize() { }
    }
}