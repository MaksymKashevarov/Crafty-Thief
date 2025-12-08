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

        public void CallbackActivation(IUIElement element)
        {
            if (element == null)
            {
                Debug.LogAssertion("Element is invalid");
                return;
            }
            element.Activate();
            List<IUIElement> children = element.GetChildElements();
            if (children == null)
            {
                Debug.LogWarning("No children list present");
                return;
            }
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

        public IEnumerator BuildActive(IUIElement element, Transform parent)
        {
            GameObject prefab = element.GetObject();
            GameObject instance;
            Debug.Log("Building");
            instance = Instantiate(prefab, parent);
            yield return new WaitForSeconds(2);
            Debug.Log("Done");
            element.SetController(this);
            Debug.Log("Element Found");
        }

        public void BuildActiveElement(IUIElement element, Transform parent = null, IUIElement elementParent = null)
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
            Instantiate(prefab, parent);
            IUIElement currentElement = Container.tContainer.Resolve();
            if (currentElement == null)
            {
                Debug.LogAssertion("Missing element");
                return;
            }
            Debug.Log($"Recieved: {currentElement}");
            currentElement.SetController(this);
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

