namespace Game.Core
{
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
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Transform _spawnPoint;
        private PlayerInterface _activePlayerInterface;
        private List<string> _activeStealingList = new();


        void Update()
        {
            CountTime();
        }

        private void Start()
        {
            GenerateStealingList();
            BuildCharacter();
        }

        private void ApplyActiveStealList()
        {
            if (_activePlayerInterface != null)
            {
                _activePlayerInterface.RequestListBuild(_activeStealingList);
                Debug.Log("Requesting list build");
            }
            else
            {
                Debug.LogError("Interface missing");
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

        private void BuildCharacter()
        {
            if (_player != null)
            {
                GameObject readyPlayer = Instantiate(_player, _spawnPoint.position, _spawnPoint.rotation);
                PlayerCore playerCore = readyPlayer.GetComponent<PlayerCore>();
                if (playerCore != null && _canvas != null)
                {
                    GameObject playerCanvas = Instantiate(_canvas);
                    UIController playerController = playerCanvas.GetComponent<UIController>();
                    if (playerController != null)
                    {
                        playerCore.SetController(playerController);
                        playerCore.SetDriver(this);
                    }
                }
            }
            else
            {
                Debug.LogError("PLAYER MISSING!");
            }
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

