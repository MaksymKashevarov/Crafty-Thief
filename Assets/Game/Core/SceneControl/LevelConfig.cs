namespace Game.Core.SceneControl
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Config/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Level Core Settings")]
        [SerializeField] private DifficultyLevel _difficulty;

        [Header("Level Core Config")]
        [SerializeField] private bool _IsDoorLocked;
        [SerializeField] private int _maxDoorLocks;

        public DifficultyLevel GetDifficulty()
        {
            return _difficulty;
        }

        public bool IsDoorLocked()
        {
            return _IsDoorLocked;
        }

        public int GetMaxDoorLocks()
        {
            if (!_IsDoorLocked)
            {
               DevLog.LogWarning("Trying to get max door locks while the door is not locked. Returning 0.", this);
                return 0;
            }
            return _maxDoorLocks;
        }
    }

}
