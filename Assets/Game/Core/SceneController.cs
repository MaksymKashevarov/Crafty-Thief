namespace Game.Core
{
    using Game.Core.ServiceLocating;
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;
    using System.Threading.Tasks;

    public class SceneController : MonoBehaviour
    {
        [SerializeField] private SceneDatabase _database;
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

        public Task SwitchScene(SceneData level)
        {
            return LoadScene(level);
        }

        private async Task LoadScene(SceneData level)
        {
            if (level == null)
            {
                Debug.LogAssertion("Level data is null");
                return;
            }
            if (_database == null)
            {
                DevLog.LogAssetion("Scene database is null");
                return;
            }

            var loadingScene = _database.GetLoadingScene();
            if (loadingScene == null)
            {
                DevLog.LogAssetion("Loading scene is null!");
                return;
            }

            await AssembleScene(loadingScene);
            await Task.Yield();                
            //await AssembleScene(level);
        }

        private async Task AssembleScene(SceneData level)
        {
            switch (level.GetSceneType())
            {
                case SceneType.Loading:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        PostSceneLoaded();
                        return;
                    }

                case SceneType.MainMenu:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        PostSceneLoaded();
                        return;
                    }

                case SceneType.Gameplay:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        PostSceneLoaded();
                        return;
                    }

                default:
                    DevLog.LogAssetion("Scene type not recognized!");
                    return;
            }
        }

        private async Task LoadAddressableScene(SceneData level, LoadSceneMode mode)
        {
            AssetReference sceneReference = level.GetScene();
            if (sceneReference == null)
            {
                DevLog.LogAssetion("Scene reference is null!");
                return;
            }

            var load = Addressables.LoadSceneAsync(sceneReference, mode);
            await load.Task;
        }

        private void PostSceneLoaded()
        {
            if (_driver == null) return;

            ReloadSceneContent();
            //_driver.RequestSScreenBuild();
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

