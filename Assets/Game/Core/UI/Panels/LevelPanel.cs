namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public enum ButtonSide
    {
        left,
        right
    }

    public class LevelPanel : MonoBehaviour, IUIElement, ISceneConnected
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private DifficultyDisplay _difficultyDisplay;
        private IUIElement _parent;
        private List<IUIElement> _children = new List<IUIElement>();


        private void Start()
        {
            InitializeContent();
        }

        private void InitializeContent()
        {
            LoadDifficulty();
        }
        
        private void LoadDifficulty()
        {
            IButton display = _controller.BuildButton(_difficultyDisplay, this.transform, this);
            IUIElement displayElement = display.GetUIElement();
            if (displayElement == null)
            {
                Debug.LogAssertion("Missing UI Element");
                displayElement.Terminate();
                return;
            }
            ISceneConnected connection = displayElement.GetSceneConnection();           
            SetAsChild(displayElement);
            if (connection == null)
            {
                Debug.LogAssertion("Missing Connection");
                displayElement.Terminate();
                return;
            }
            connection.AssignScene(_sceneData);          
        }


        public void Activate()
        {
            return;
        }

        public void AssignScene(SceneData scene)
        {
            _sceneData = scene;
        }

        public void CollectChildElements()
        {
            return;
        }

        public SceneData GetAssignedScene()
        {
            return null;
        }

        public List<IUIElement> GetChildElements()
        {
            return _children;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public ISceneConnected GetSceneConnection()
        {
            return this;
        }

        public void SetAsChild(IUIElement element)
        {
            DevLog.elementLog.Log($"Adding the {element.GetType().Name}", this);
            if (_children.Contains(element))
            {
                Debug.LogWarning("Already In List");
                return;
            }
            _children.Add(element);
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetParent(IUIElement element)
        {
            Debug.Log("RECIEVED");
            _parent = element;
        }

        public void Terminate()
        {
            _controller.DestroyElementAsParent(this);
        }

        public void AssignDifficulty(DifficultyLevel difficulty)
        {
            return;
        }
    }

}
