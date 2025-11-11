using UnityEngine;
using System.Collections.Generic;

namespace Game.Attributes
{
    [System.Serializable]
    public class CharacterEntry
    {
        public string Name;
        public float Value;
    }

    [CreateAssetMenu(fileName = "NpcCharacter", menuName = "NPC/Character")]
    public class NpcStats : ScriptableObject
    {
        public List<CharacterEntry> CharacterData = new List<CharacterEntry>();
    }
}