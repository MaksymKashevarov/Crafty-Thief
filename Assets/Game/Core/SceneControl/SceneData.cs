namespace Game.Core.SceneControl
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    public enum DifficultyLevel
    {
        Low,
        Medium,
        High
    }

    public enum SceneType
    {
        MainMenu,
        Gameplay,
        Loading
    }

    [CreateAssetMenu(menuName = "Config/Scene Data")]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private AssetReference _scene;
        [SerializeField] private string _sceneName;
        [SerializeField] private List<DifficultyLevel> _levelList = new();
        [SerializeField] private SceneType _sceneType;

        public AssetReference GetScene()
        {
            return _scene;
        }

        public string GetSceneName()
        {
            return _sceneName;
        }

        public List<DifficultyLevel> GetDifficulity()
        {
            if (_levelList.Count == 0)
            {
                return null;
            }
            return _levelList;
        }

        public SceneType GetSceneType()
        {
            return _sceneType;
        }

    }
}
