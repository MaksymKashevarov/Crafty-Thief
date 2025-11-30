namespace Game.Core
{
    using Game.Core.DI;
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;

    public class SceneController : MonoBehaviour
    {
        [SerializeField] private List<string> _scenes = new();
        [SerializeField] private string _sourceScene;
        private SceneDatabase _database;
        private GlobalDriver _driver;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async void LoadMenu()
        {
            SceneData menuScene = _database.GetSourceScene();
            AssetReference sceneReference = menuScene.GetScene();
            if (menuScene == null)
            {
                return;
            }
            var load = Addressables.LoadSceneAsync(sceneReference);
            Debug.Log("Waiting!");
            await load.Task;
            Debug.Log("Completed!");
            if (_driver == null)
            {
                return;
            }
            _driver.RequestSScreenBuild();
            ReloadSceneContent();
        }

        public void SetDataBase(SceneDatabase database)
        {
            _database = database;
        }

        public void SetGlobalDriver(GlobalDriver driver)
        {
            _driver = driver;
        }

        public void ReloadSceneContent()
        {
            if (_driver == null)
            {
                Debug.LogAssertion("Missing Driver!");
                return;
            }
            _driver.BuildEventSystem();
        }

    }
}

