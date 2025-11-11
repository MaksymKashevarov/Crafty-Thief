namespace Game.Attributes
{
    using UnityEngine;
    using System.Collections.Generic;

    [System.Serializable]
    public class AttributeEntry
    {
        public string Name;
        public int Value;
    }

    [CreateAssetMenu(fileName = "NpcAttributes", menuName = "NPC/Attributes")]
    public class NpcAttributes : ScriptableObject
    {
        public List<AttributeEntry> Attributes = new List<AttributeEntry>();
    }
}
    
