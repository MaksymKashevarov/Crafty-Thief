namespace Game.Core.SceneControl
{

    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [CreateAssetMenu(menuName = "Config/Scene Data")]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private AssetReference _scene;
        [SerializeField] private string _sceneName;

        public AssetReference GetScene()
        {
            return _scene;
        }

        public string GetSceneName()
        {
            return _sceneName;
        }

    }


}
