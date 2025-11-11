namespace Game.Utility
{
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Attributes;

    public static class NpcDataSetter
    {

        public static int GenerateAttributeData(int inputNumber)
        {
            inputNumber = Random.Range(0, 101);
            return inputNumber;
        }

        public static int GetSleepDuration(List<AttributeEntry> attributes)
        {
            float sleepNeed = GetAttr(attributes, "SleepNeed");
            float laziness = GetAttr(attributes, "Laziness");

            float score = sleepNeed + 0.5f * laziness;

            float z = (score - 50f) / 12f;
            float sig = 1f / (1f + Mathf.Exp(-z));

            float hours = 5f + 6.5f * sig;

            int minutes = Mathf.RoundToInt(hours * 60f);
            minutes = Mathf.RoundToInt(minutes / 15f) * 15;

            return minutes < 0 ? 0 : minutes;
        }

        private static float GetAttr(List<AttributeEntry> attrs, string name)
        {
            foreach (var a in attrs)
                if (a.Name == name) return a.Value;
            return 0f;
        }
    }
}


