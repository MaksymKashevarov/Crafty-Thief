namespace Game.Core
{
    using Game.Core.DI;
    using Game.Core.Interactable;
    using Game.Core.Player;
    using Game.Core.UI;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Rendering;

    public class GlobalDriver : MonoBehaviour
    {
        private float _logTimer;
        [SerializeField] private List<Item> _itemsToSteal;
        [SerializeField] private int _stealListCount;
        [SerializeField] private PlayerCore _playerPrefab;
        [SerializeField] private UIController _canvasPrefab;
        [SerializeField] private SceneController _sceneController;
        private PlayerInterface _activePlayerInterface;
        private List<string> _activeStealingList = new();


        void Update()
        {
            CountTime();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            BuildSceneController();
        }

        public void LoadLevel(SpawnPoint spawnPoint)
        {            
            GenerateStealingList();
            BuildCharacter(spawnPoint);
            MarkItemsToSteal();
        }

        private void Start()
        {

        }

        private void ApplyActiveStealList()
        {
            if (_activePlayerInterface != null)
            {
                _activePlayerInterface.RequestListBuild(_activeStealingList);
                Debug.Log("Requesting list build");
                _activeStealingList.Clear();
            }
            else
            {
                Debug.LogError("Interface missing");
            }
        }

        public void BuildSceneController()
        {
            if (_sceneController == null)
            {
                Debug.LogError("Error! Scene Controller is invalid!");
                return;
            }
            Debug.Log("Loading Scene Controller!");
            Instantiate(_sceneController);
            _sceneController.SetGlobalDriver(this);
        }

        public void CheckListCompletion()
        {
            List<string> activeList = _activePlayerInterface.GetActiveList();
            if (activeList.Count == 0)
            {
                Debug.LogWarning("Nothing to collect");
            }
            else
            {
                Debug.Log($"Items left: {activeList.Count}");
            }
        }

        public void SetActivePlayerInterface(PlayerInterface playerInterface)
        {
            if (_activePlayerInterface != null)
            {
                _activePlayerInterface = null;
                _activePlayerInterface = playerInterface;
            }
            else
            {
                _activePlayerInterface = playerInterface;
            }
            Debug.Log("INTERFACE REINSTALLED!");
            ApplyActiveStealList();
        }

        public void MarkItemsToSteal()
        {
            if (_activePlayerInterface != null)
            {
                List<IInteractable> registryList = SpawnRegistry.GetItemList();
                List<string> activeStealList = _activePlayerInterface.GetActiveList();

                if (registryList != null && activeStealList != null)
                {
                    foreach (IInteractable registry in registryList)
                    {
                        Item currentItem = registry.GetItem();
                        if (currentItem != null && activeStealList.Contains(currentItem.GetItemName()))
                        {
                            registry.SetValuable(true);                            
                        }
                    }
                }
            }

        }

        private void BuildCharacter(SpawnPoint spawnPoint)
        {
            if (_playerPrefab == null)
            {
                Debug.LogError("PLAYER PREFAB MISSING!");
                return;
            }

            if (_canvasPrefab == null)
            {
                Debug.LogError("CANVAS PREFAB MISSING!");
                return;
            }

            Transform spawnPointPos = spawnPoint.GetTransform();
            PlayerCore playerCore = Instantiate(_playerPrefab, spawnPointPos.position, Quaternion.identity);
            UIController playerController = Instantiate(_canvasPrefab);

            if (playerCore == null)
            {
                Debug.LogError("PLAYER CORE MISSING ON PREFAB!");
                return;
            }

            if (playerController == null)
            {
                Debug.LogError("UI CONTROLLER MISSING ON CANVAS PREFAB!");
                return;
            }

            playerCore.Initialize(playerController, this);
        }

        private void GenerateStealingList()
        {
            List<Item> list = new List<Item>(_itemsToSteal);

            list = list
            .OrderBy(i => Random.value)
            .Take(_stealListCount)
            .ToList();

            foreach (Item item in list)
            {
                string itemName = item.GetItemName();
                _activeStealingList.Add(itemName);
                Debug.Log($"Item set to list: {itemName}");
            }
        }

        public void CountTime()
        {
            GameTime.Update(Time.deltaTime);

            _logTimer += Time.deltaTime;
            if (_logTimer >= 5f)
            {
                _logTimer = 0f;

                Debug.Log($"Current time (HHMM): {GameTime.CurrentTime}");

                Debug.Log($"Current time (formatted): {GameTime.GetFormattedTime()}");
            }
        }
    }

}

