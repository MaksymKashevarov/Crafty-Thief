namespace Game.Core
{
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Attributes;
    using Game.Utility;
    using UnityEngine.TextCore.Text;

    public class NpcCore : MonoBehaviour
    {
        [SerializeField] private NpcAttributes _attributes;
        [SerializeField] private NpcStats _stats;
        private Dictionary<string, float> attributes = new();
        private Dictionary<string, float> stats = new();
        private NpcBrain _brain;

        private void BuildCharacter()
        {
            foreach (var entry in _stats.CharacterData)
            {
                stats[entry.Name] = entry.Value;
            }
        }

        private void BuildAttributes()
        {
            foreach (var attribute in _attributes.Attributes)
            {
                float generatedValue = NpcDataSetter.GenerateAttributeData(attribute.Value);
                attributes[attribute.Name] = generatedValue;
            }
        }

        private void Start()
        {           
            BuildAttributes();
            BuildCharacter();
            foreach (var attribute in attributes)
            {
                Debug.Log($"{attribute.Key} = {attribute.Value}");
            }

            foreach (var stat in stats)
            {
                Debug.Log($"{stat.Key} = {stat.Value}");
            }
            _brain = new NpcBrain(attributes);
        }
    }

}

