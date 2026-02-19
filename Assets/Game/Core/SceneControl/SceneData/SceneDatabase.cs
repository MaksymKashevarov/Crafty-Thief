namespace Game.Core.SceneControl
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [CreateAssetMenu(menuName = "Config/Scene Database")]
    public class SceneDatabase : ScriptableObject
    {
        [SerializeField] private SceneData _sourceScene;
        [SerializeField] private SceneData _loadingScene;
        [Header("Scene Categories")]
        [SerializeField] private List<SceneData> _devScenes;
        [SerializeField] private List<SceneData> _coreMaps;
        [SerializeField] private List<SceneData> _hotel;

        private Dictionary<string, List<SceneData>> _referenceBundle = new();

        public void AssembleDataBase()
        {
            AssembleReferences();
        }

        private void AssembleReferences()
        {
            Debug.Log("Assembling Scenes!");
            //ASSEMBLE SCENES HERE//
            _referenceBundle["DevScenes"] = _devScenes;
            _referenceBundle["Zone [VISTA]"] = _coreMaps;
            _referenceBundle["Hotel"] = _hotel;

            //[BORDER]//

            foreach (string key in _referenceBundle.Keys)
            {
                if (key == null)
                {
                    Debug.LogAssertion("Key is Invalid!");
                    break;
                }
                Debug.Log($"Bundle contains: {key}");
            }
        }

        public SceneData GetLoadingScene()
        {
            return _loadingScene;
        }

        public List<SceneData> GetDevScenes()
        {
            return _devScenes;
        }

        public SceneData GetSourceScene()
        {
            return _sourceScene;
        }

        public Dictionary<string, List<SceneData>> GetReferenceBundle()
        {
            return _referenceBundle;
        }
    }
}

