namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using Game.Core.Interactable;
    using Game.Core.Player;
    using Game.Core.SceneControl;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using static UnityEditor.Rendering.FilterWindow;
    using Game.Core.Factory;

    public class UIController : MonoBehaviour
    {
        [SerializeField] private Transform _canvasParent;
        [SerializeField] private GameObject _stealList;
        [SerializeField] private TextBox textbox;
        [SerializeField] private SourceMenu _menuScreen;
        private SceneDatabase _sceneDatabase;
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

        public void SetDatabase(SceneDatabase input)
        {
            _sceneDatabase = input;
        }
        public SceneDatabase GetDatabase()
        {
            return _sceneDatabase;
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

        public IUIElement BuildActiveElement(IUIElement element, Transform parent = null, IUIElement elementParent = null)
        {
            if (element == null)
            {
                return null;
            }
            if (parent == null)
            {
                parent = _canvasParent;
            }
            IUIElement currentElement = UIFactory.BuildElement(element, parent);
            Debug.Log($"[{this.name}]: {currentElement}");
            if (currentElement == null)
            {
                Debug.LogAssertion("Missing element");
                return null;
            }
            Debug.Log($"Recieved: {currentElement}");
            if (elementParent != null)
            {
                currentElement.SetParent(elementParent);
            }
            currentElement.SetController(this);

            currentElement.Activate();

            List<IUIElement> children = currentElement.GetChildElements();
            if (children == null)
            {
                Debug.LogWarning("No children list present");
                return currentElement;
            }
            if (children.Count == 0)
            {
                return currentElement;
            }
            if (children != null)
            {
                Debug.Log($"[{this.name}]: List detected: {children}");
                foreach (IUIElement child in children)
                {
                    if (child == null)
                    {
                        Debug.LogError("List contains Null!");
                        break;
                    }
                    Debug.Log($"[{this}] Controller set to: {child}");
                    child.SetController(this);
                    Debug.Log($"[{this}] Setting Parent: {currentElement}");
                    child.SetParent(currentElement);
                    Debug.Log($"[{this}] Activating {child}");
                    child.Activate();
                }
                
            }
            return currentElement;
        }

        public IButton BuildButton(IUIElement element, Transform parent = null, IUIElement elementParent = null)
        {
            if (element == null)
            {
                return null;
            }
            if (parent == null)
            {
                parent = _canvasParent;
            }
            IButton button = UIFactory.BuildButton(element, parent);
            if (button == null)
            {
                Debug.LogAssertion("Missing element");
                return null;
            }
            if (elementParent != null)
            {
                button.SetParent(elementParent);
            }
            button.SetController(this);
            return button;
        }

        public void DestroyElement(IUIElement element)
        {
            if (element == null)
            {
                Debug.LogAssertion("Missing Element");
                return;
            }
            Debug.Log($"Destroying: {element}");
            Destroy(element.GetObject());
        }

        public void DestroyElementAsParent(IUIElement element)
        {
            if (element == null)
            {
                Debug.LogAssertion("Missing Parent");
                return;
            }
            Debug.Log("Deleting");

            List<IUIElement> children = element.GetChildElements();
            if (children == null)
            {
                Debug.LogWarning("Missing Children");
                DestroyElement(element);
                return;
            }
            if (children.Count > 0)
            {
                Debug.Log("List detected!");
                foreach (IUIElement child in children)
                {
                    if (child == null)
                    {
                        Debug.LogError($"[{this}] List contains Null!");
                        break;
                    }
                    Debug.Log($"[{this}] Deleting: {child}");
                    child.Terminate();

                }
            }            
            DestroyElement(element);
            Debug.Log("DONE");

        }

    }

}

