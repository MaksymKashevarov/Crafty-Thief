namespace Game.Core.UI
{
    using Game.Core.DI;
    using Game.Core.Interactable;
    using Game.Core.Player;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using static UnityEditor.Rendering.FilterWindow;

    public class UIController : MonoBehaviour
    {
        [SerializeField] private Transform _canvasParent;
        [SerializeField] private GameObject _stealList;
        [SerializeField] private TextBox textbox;
        [SerializeField] private SourceMenu _menuScreen;
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
            BuildActiveElement(_menuScreen);

        }

        public void CallbackActivation(IUIElement element)
        {
            element.Activate();
            List<IUIElement> children = element.GetChildElements();
            if (children.Count == 0)
            {
                return;
            }
            if (children != null)
            {
                Debug.Log("List detected!");
                foreach (IUIElement child in children)
                {
                    if (child == null)
                    {
                        Debug.LogError("List contains Null!");
                        break;
                    }
                    Debug.Log($"Controller set to: {child}");
                    child.SetController(this);
                    Debug.Log($"Setting Parent: {element}");
                    child.SetParent(element);
                    Debug.Log($"Activating {child}");
                    child.Activate();
                }
            }

        }


        public void BuildActiveElement(IUIElement element, Transform parent = null)
        {
            if (element == null)
            {
                return;
            }
            if (parent == null)
            {
                parent = _canvasParent;
            }

            GameObject prefab = element.GetObject();
            GameObject instance;
            instance = Instantiate(prefab, parent);
            element.SetInstance(instance);
            Debug.Log("setting controller!");
            element.SetController(this);
        }

        public void DestroyElement(IUIElement element)
        {
            if (element == null)
            {
                return;
            }
            Debug.Log($"Destroying: {element}");
            Destroy(element.GetInstance());
        }

        public void DestroyElementAsParent(IUIElement element)
        {
            Debug.Log("Deleting");

            List<IUIElement> children = element.GetChildElements();
            if (children.Count == 0)
            {
                return;
            }

            if (children != null)
            {
                Debug.Log("List detected!");
                foreach (IUIElement child in children)
                {
                    if (child == null)
                    {
                        Debug.LogError("List contains Null!");
                        break;
                    }
                    Debug.Log($"Unregistering: {child}");
                    Registry.menuRegisrtry.Unregister(child);
                    Debug.Log($"Deleting: {child}");
                    child.Terminate();
                
                }
            }
            element.Terminate();
        }

    }

}

