namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem.XR;
    using TMPro;
    using System.Linq;

    public class MapSelectionScreen : MonoBehaviour, IUIElement
    {
        [SerializeField] private CategoryButton _categoryButton;
        [SerializeField] private Transform _categoryParent;
        private List<IUIElement> _childElements = new();
        [SerializeField] private UIController _controller;
        private GameObject _instance;
        private List<IButton> _buttons = new();
        public void Activate()
        {
            CollectChildElements();
        }

        public void CollectChildElements()
        {
            if (_childElements.Count > 0)
            {
                _childElements.Clear();
            }

            List<IUIElement> elements = Registry.mSRegistry.GetRegistryList();
            if (elements == null)
            {
                return;
            }
            if (elements.Count == 0)
            {
                return;
            }
            foreach (IUIElement element in elements)
            {
                Debug.Log($"{element} Added from Registry");
                _childElements.Add(element);
            }
        }

        private void Awake()
        {
            Container.tContainer.RegisterAsTElement(this);
        }
        private void Start()
        {
            Debug.Log("DISPLAYING");
            DisplayMapSelection();
            Debug.Log("END");

        }
        private void BuildSelection(SceneDatabase dataBase, Dictionary<string, List<SceneData>> sceneData)
        {
            Debug.Log("Creating base map selection...");
                        
            for (int i = 0; i < sceneData.Count; i++)
            {
                _controller.BuildActiveElement(_categoryButton, _categoryParent, this);
                IButton button = Container.tContainer.ResolveTButton();
                Debug.Log($"[{this.name}] Adding [{button}]");
                _buttons.Add(button);
            }
        }

        private void ApplyCategoryText(SceneDatabase dataBase, Dictionary<string, List<SceneData>> sceneData)
        {
            if (dataBase == null)
            {
                Debug.LogAssertion($"[{this.name}] DataBase Missing");
                return;
            }
            if (sceneData == null)
            {
                Debug.LogAssertion($"[{this.name}] Dictionary Missing");
                return;
            }
            if(sceneData.Count == 0) 
            {
                Debug.LogAssertion($"[{this.name}] No elements in Dictionary");
                return; 
            }
            if (_buttons.Count == 0)
            {
                Debug.LogAssertion($"[{this.name}] No elements in List");
            }
            List<string> keysList = sceneData.Keys.ToList();
            for (int i = 0; i < _buttons.Count; i++)
            {
                Debug.Log($"Setting text {_buttons[i]} Text: [{keysList[i]}]");
                TextMeshProUGUI textComponent = _buttons[i].GetTextComponent();
                if(textComponent == null)
                {
                    Debug.LogAssertion("Components is Invalid");
                    break;
                }
                Debug.Log($"[{this.name}]");
                _buttons[i].SetText(keysList[i]);
            }
        }

        public void DisplayMapSelection()
        {
            if (_controller == null)
            {
                Debug.LogAssertion("Missing Controller");
                return;
            }
            SceneDatabase dataBase = _controller.GetDatabase();
            Dictionary<string, List<SceneData>> sceneData = dataBase.GetReferenceBundle();
            BuildSelection(dataBase, sceneData);
            //ApplyCategoryText(dataBase, sceneData);

        }

        public List<IUIElement> GetChildElements()
        {
            return _childElements;
        }

        public GameObject GetInstance()
        {
            return _instance;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
            if (_controller == null)
            {
                Debug.LogAssertion("[SET] Missing Controller!");
                return;
            }
            Debug.Log($"Controller set: {_controller}");
        }

        public void SetInstance(GameObject instance)
        {
            _instance = instance;
        }

        public void SetParent(IUIElement element)
        {
            return;
        }

        public void Terminate()
        {
            _childElements.Clear();
            _childElements = null;
            _controller.DestroyElement(this);
        }
    }

}
