namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using Game.Core.Player;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIController : MonoBehaviour
    {
        [SerializeField] private Transform _canvasParent;
        [SerializeField] private GameObject _stealList;
        [SerializeField] private GameObject textbox;
        private Transform panelParent;
        private GameObject _currentInterface;
        private PlayerInterface _currentPlayerInterface;

        public void ShowStealList(List<string> stealList)
        {
            if (_stealList != null)
            {
                GameObject activePanel = Instantiate(_stealList, _canvasParent);
                panelParent = activePanel.transform;
                foreach (string itemName in stealList)
                {
                    GameObject currentTextBox = Instantiate(textbox, panelParent);
                    TextMeshProUGUI text = currentTextBox.GetComponent<TextMeshProUGUI>();
                    if (text != null)
                    {
                        text.text = itemName;
                    }

                }
            }
        }

        public void SetCurrentInterace(GameObject element)
        {
            _currentInterface = element;
        }

        public GameObject GetCurrentInterface()
        {
            if (_currentInterface == null)
            {
                Debug.LogWarning("No current element present!");
                return null;
            }
            else
            {
                return _currentInterface;
            }
        }

        public void SetPlayerInterface(PlayerInterface playerInterface)
        {
           _currentPlayerInterface = playerInterface;
        }


        public void DestroyElement(GameObject element)
        {
            if (element != null)
            {
                Debug.Log($"Element {element} destroyed");
                Destroy(element);
            }
            else
            {
                Debug.LogWarning("Element is missing");
            }
        }

        public void ShowElement(GameObject element, Transform parent, IUIElement source)
        {
            if (element == null) return;

            _currentPlayerInterface.ClearElements(source);

            GameObject instance = Instantiate(element, parent);
            IUIElement instanceElement = instance.GetComponent<IUIElement>();

            if (instanceElement != null)
            {
                instanceElement.SetController(this);
                source.SetActiveElement(instance);
                instanceElement.SetRootElement(source);
            }
            else
            {
                Debug.LogWarning("Component cannot be found!");
            }
        }

        public void TerminateCurrentInterface()
        {
            if (_currentInterface != null)
            {
                DestroyElement(_currentInterface);
                _currentInterface = null;
                Debug.Log("Success");
            }
            else
            {
                Debug.LogError("No interface present!");
            }
        }

        public void OpenInventoryInterface()
        {
            
        }
    }

}

