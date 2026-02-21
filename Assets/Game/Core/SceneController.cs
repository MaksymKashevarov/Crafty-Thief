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
        [SerializeField] private LevelConfigContainer _levelConfigContainer;
        private GlobalDriver _driver;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async void LoadMenu()
        {
            SceneData menuScene = _database.GetSourceScene();
            AssetReference sceneReference = menuScene.GetScene();
            await SwitchScene(menuScene);
            Debug.Log("Completed!");
            if (_driver == null)
            {
                return;
            }
             //REFACTOR
        }

        public void SetLevelConfigs(LevelConfigContainer container)
        {
            _levelConfigContainer = container;
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
                DevLog.LogAssetion("Scene database is null", this);
                return;
            }

            var loadingScene = _database.GetLoadingScene();
            if (loadingScene == null)
            {
                DevLog.LogAssetion("Loading scene is null!", this);
                return;
            }

            await AssembleScene(loadingScene);
            await Task.Yield();                
            await AssembleScene(level);
        }

        private async Task AssembleScene(SceneData level)
        {
            switch (level.GetSceneType())
            {
                case SceneType.Loading:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        DevLog.Log("Loading scene loaded", this);
                        PostSceneLoaded(SceneType.Loading);
                        return;
                    }

                case SceneType.MainMenu:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        DevLog.Log("Menu scene loaded", this);
                        PostSceneLoaded(SceneType.MainMenu);
                        return;
                    }

                case SceneType.Gameplay:
                    {
                        await LoadAddressableScene(level, LoadSceneMode.Single);
                        DevLog.Log("Gameplay scene loaded", this);
                        await ConfigureDifficulty(level);
                        PostSceneLoaded(SceneType.Gameplay);
                        return;
                    }

                default:
                    DevLog.LogAssetion("Scene type not recognized!", this);
                    return;
            }
        }

        private Task ConfigureDifficulty(SceneData level)
        {           
            return Task.CompletedTask;
        }


        private async Task LoadAddressableScene(SceneData level, LoadSceneMode mode)
        {
            AssetReference sceneReference = level.GetScene();
            if (sceneReference == null)
            {
                DevLog.LogAssetion("Scene reference is null!", this);
                return;
            }

            var load = Addressables.LoadSceneAsync(sceneReference, mode);
            await load.Task;
        }

        private async void PostSceneLoaded(SceneType type)
        {
            switch (type)
            {
                case SceneType.Loading:
                    await ReloadSceneContent();
                    DevLog.Log("Reloading scene content for loading scene", this);
                    _driver.RequestCharacterBuild(false, null, null);
                    break;
                case SceneType.MainMenu:
                    _driver.RequestMenuScreenBuild();
                    DevLog.Log("Requesting menu build", this);
                    await ReloadSceneContent();
                    break;
                case SceneType.Gameplay:
                    await ReloadSceneContent();
                    await _driver.GetSpawnDirector().GenInitialize();
                    SpawnPoint spawnPoint = Container.Resolve<SpawnPoint>();
                    _driver.RequestCharacterBuild(true, null, spawnPoint.transform);
                    DevLog.Log("Reloading scene content for gameplay scene", this);
                    break;
                default:
                    break;
            }
            
        }

        public void SetDataBase(SceneDatabase database)
        {
            _database = database;
        }

        public void SetGlobalDriver(GlobalDriver driver)
        {
            _driver = driver;
        }

        public Task ReloadSceneContent()
        {
            if (_driver == null)
            {
                Debug.LogAssertion("Missing Driver!");
                return Task.CompletedTask;
            }
            
            _driver.BuildEventSystem();
            return Task.CompletedTask;
        }

    }
}

