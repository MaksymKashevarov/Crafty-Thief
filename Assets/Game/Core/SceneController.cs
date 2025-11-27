namespace Game.Core
{
    using Game.Core.DI;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneController : MonoBehaviour
    {
        [SerializeField] private List<string> _scenes = new();
        [SerializeField] private string _sourceScene;
        private SceneDatabase _database;
        private GlobalDriver _driver;

        private void Start()
        {
            if (_driver == null)
            {
                Debug.LogWarning("Driver Is Missing!");
                return;
            }
            SpawnPoint spawnPoint = Container.Resolve<SpawnPoint>();
            if (spawnPoint == null) 
            {
                Scene current = SceneManager.GetActiveScene();
                string sceneName = current.name;

                if (sceneName != _sourceScene)
                {
                    ResetScene();
                }
                if(_driver == null)
                {
                    Debug.LogError("Driver is missing!");
                    return;
                }
                Transform driverT = _driver.transform;
                _driver.LoadLevel(driverT, false);
                _driver. RequestSScreenBuild();
                return;

            }
            Transform activeSpawnPoint = spawnPoint.GetTransform();
            _driver.LoadLevel(activeSpawnPoint, true);

        }

        public void SetDataBase(SceneDatabase database)
        {
            _database = database;
        }

        public void SetGlobalDriver(GlobalDriver driver)
        {
            _driver = driver;
        }

        public void ResetScene()
        {
            Scene current = SceneManager.GetActiveScene();
            string sceneName = current.name;
            Debug.Log($"Name: {sceneName}");
            if (sceneName == _sourceScene)
            {
                Debug.LogWarning("Scene is already source");
                return;
            }

            SceneManager.LoadScene(_sourceScene);
        }

        //               Change paramether for proper method call (Depends)
        public void RequestSceneLoad(string input)
        {
            foreach (var scene in _scenes)
            {
                if (scene == input)
                {
                    SceneManager.LoadScene(scene);
                    break;
                }
            }
        }
    }
}

