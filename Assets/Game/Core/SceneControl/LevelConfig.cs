namespace Game.Core.SceneControl
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Config/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Level Core Settings")]
        [SerializeField] private DifficultyLevel _difficulty;

    }

}
