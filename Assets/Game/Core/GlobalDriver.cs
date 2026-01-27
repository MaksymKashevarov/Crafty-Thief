namespace Game.Core
{
    using Game.Core.ServiceLocating;
    using Game.Core.Interactable;
    using Game.Core.Player;
    using Game.Core.SceneControl;
    using Game.Core.UI;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Rendering;

    public class GlobalDriver : MonoBehaviour
    {
        private float _logTimer;
        [SerializeField] private List<Item> _itemsToSteal;
        [SerializeField] private int _stealListCount;
        [SerializeField] private PlayerCore _playerPrefab;
        [SerializeField] private PlayerCore _ghostPlayerPrefab;
        [SerializeField] private UIController _canvasPrefab;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SceneDatabase _sceneDatabase;
        private EventSystem _activeEventSystem;
        private PlayerInterface _activePlayerInterface;
        private List<string> _activeStealingList = new();
        private PlayerCore _activePlayer;


        void Update()
        {
            CountTime();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("Awake");
        }


        public void BuildEventSystem()
        {
            if (_activeEventSystem != null)
            {
                DevLog.Log("Event System Valid", this);
                return;
            }
            _activeEventSystem = Instantiate(_eventSystem);
        }

        private void BuildSceneController()
        {
            if (_sceneController == null)
            {
                Debug.LogError("Error! Scene Controller is invalid!");
                return;
            }
            if (_sceneDatabase == null)
            {
                return;
            }

            Debug.Log("Loading Scene Controller!");
            SceneController activeSC = Instantiate(_sceneController);
            activeSC.SetGlobalDriver(this);
            activeSC.SetDataBase(_sceneDatabase);
            activeSC.LoadMenu();
            _sceneController = activeSC;
        }

        public void RequestSwitchScene(SceneData level)
        {
            _sceneController.SwitchScene(level);
        }

        private void Start()
        {
            Debug.Log("Start");
            if (_eventSystem == null)
            {
                return;
            }
            if(_sceneDatabase == null)
            {
                Debug.LogAssertion("Data base is Invalid!");
                return;
            }
            _sceneDatabase.AssembleDataBase();
            BuildSceneController();
        }

        public void RequestMenuScreenBuild() //REFACTOR
        {  
            BuildCharacter(null, _ghostPlayerPrefab, false);
            if (_activePlayerInterface == null)
            {
                Debug.LogAssertion("PlayerInterface is Missing!");
                return;
            }
            _activePlayerInterface.RequestMenuBuild();
        }

        private void ApplyActiveStealList() //REFACTOR
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

        public void MarkItemsToSteal() //REFACTOR
        {
            if (_activePlayerInterface != null)
            {
                List<IInteractable> registryList = Registry.GetItemList();
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

        public void RequestCharacterBuild(bool Playable, PlayerCore player = null, Transform spawnPoint = null)
        {
            if (player == null)
            {
                if (Playable)
                {
                    player = _playerPrefab;
                }
                else
                {
                    player = _ghostPlayerPrefab;
                }
            }
            BuildCharacter(spawnPoint, player, Playable);
        }

        private void BuildCharacter(Transform spawnPoint, PlayerCore player, bool Playable)
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

            if (spawnPoint == null)
            {
                spawnPoint = gameObject.transform;
            }

            PlayerCore playerInstance = Instantiate(player, spawnPoint.position, Quaternion.identity);
            UIController playerController = Instantiate(_canvasPrefab);

            if (playerInstance == null)
            {
                Debug.LogError("PLAYER CORE MISSING ON PREFAB!");
                return;
            }

            if (playerController == null)
            {
                DevLog.Log("UI CONTROLLER MISSING ON PREFAB!", this);
                return;
            }
            if (_sceneDatabase == null)
            {
                return;
            }
            playerController.SetDatabase(_sceneDatabase);

            playerInstance.Initialize(playerController, this);
            playerInstance.SetPlayable(Playable);
        }

        private void GenerateStealingList() //REFACTOR
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

