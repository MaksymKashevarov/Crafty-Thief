namespace Game.Utility 
{
    using UnityEngine;
    using System.Collections.Generic;

    public enum SlotType
    {
        Sleep,
        Work,
        Free
    }

    [System.Serializable]
    public class ScheduleEntry
    {
        public SlotType Type;
        public int duration;
        public Transform Place;
    }

    [CreateAssetMenu(fileName = "NpcSchedule", menuName = "NPC/Schedule")]
    public class NpcSchedule : ScriptableObject
    {
        public List<ScheduleEntry> Entries = new List<ScheduleEntry>();
    }
}
