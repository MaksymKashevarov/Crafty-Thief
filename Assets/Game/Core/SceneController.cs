namespace Game.Core
{
    using Game.Core.ServiceLocating;
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
            ReloadSceneContent();
            _driver.RequestSScreenBuild(); //REFACTOR
        }

        public void SwitchScene(SceneData level) //REFACTOR
        {
            LoadScene(level);
        }

        private async void LoadScene(SceneData level)
        {
            if (level == null)
            {
                Debug.LogAssertion("Level data is null");
                return;
            }

            SceneData _loadingScene = _database.GetLoadingScene();
            if (_loadingScene == null)
            {
                DevLog.LogAssetion("Loading scene is null!");
                return;
            }

            SceneType sceneType = level.GetSceneType();
            if (sceneType == SceneType.Loading)
            {
                var loader = Addressables.LoadSceneAsync(_loadingScene.GetScene(), LoadSceneMode.Single);
                await loader.Task;
                DevLog.Log(_loadingScene.GetSceneName() + " loaded!");
                return;
            }

            AssetReference sceneReference = level.GetScene();
            var load = Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Single);
            Debug.Log("Waiting!");
            await load.Task;
            Debug.Log("Completed!");
            if (_driver == null)
            {
                return;
            }
            ReloadSceneContent(); //REFACTOR
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

