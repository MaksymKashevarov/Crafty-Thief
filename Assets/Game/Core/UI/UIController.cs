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
        [SerializeField] private TextBox textbox;
        [SerializeField] private GameObject _menuScreen;
        private Transform panelParent;
        private GameObject _currentInterface;
        private PlayerInterface _currentPlayerInterface;
        [SerializeField] private List<TextBox> _activeTextBoxes = new();

        public void ShowStealList(List<string> stealList, List<string> activeList)
        {
            if (_stealList != null)
            {
                GameObject activePanel = Instantiate(_stealList, _canvasParent);
                panelParent = activePanel.transform;
                foreach (string itemName in stealList)
                {
                    TextBox currentTextBox = Instantiate(textbox, panelParent);
                    if (currentTextBox != null)
                    {
                        currentTextBox.SetText(itemName);
                        activeList.Add(itemName);
                        _activeTextBoxes.Add(currentTextBox);
                    }

                }
            }
        }

        public void RemoveItemInList(string key)
        {
            for (int i = _activeTextBoxes.Count - 1; i >= 0; i--)
            {
                var text = _activeTextBoxes[i];
                if (text.GetText() == key)
                {
                    Destroy(text.gameObject);
                    _activeTextBoxes.RemoveAt(i);
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
            Debug.Log($"Interface Installed{this.name}");
        }

        public void DisplayMenu()
        {
            if (_menuScreen == null)
            {
                Debug.LogAssertion("Menu base is missing!");
                return;
            }
            Instantiate(_menuScreen, _canvasParent);

        }

        public void BuildActiveElement(IUIElement element)
        {
            if (element == null)
            {
                return;
            }
            

        }

        public void DestroyElement(GameObject element)
        {
            if (element == null)
            {
                Debug.LogWarning("Element is missing");
                return;
            }

            Debug.Log($"Element {element} destroyed");
            Destroy(element);
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

