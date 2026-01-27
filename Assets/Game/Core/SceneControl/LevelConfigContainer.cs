namespace Game.Core.SceneControl
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Config/Level Config Registry")]
    public class LevelConfigContainer : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelConfigs = new();

    }

}
