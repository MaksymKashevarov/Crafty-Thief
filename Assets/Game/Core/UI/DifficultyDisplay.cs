namespace Game.Core.UI 
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class DifficultyDisplay : MonoBehaviour, IUIElement, IButton, ISceneConnected
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private DifficultyDisplayButton _button;
        [SerializeField] private Transform _rTransform;
        [SerializeField] private Transform _lTransform;
        [SerializeField] private string _currentLevel;
        [SerializeField] private LevelLoadButton _loaderButton;
        private IButton _currentLoaderButton;
        private List<IButton> _buttons = new();
        private SceneData _sceneData;
        [SerializeField] private List<DifficultyLevel> _levels = new List<DifficultyLevel>();

        private IUIElement _parent;

        public void Activate()
        {
            return;
        }

        public void CollectChildElements()
        {
            return;
        }

        public List<IUIElement> GetChildElements()
        {
            return null;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public ISceneConnected GetSceneConnection()
        {
            return this;
        }

        public TextMeshProUGUI GetTextComponent()
        {
            return _text;            
        }

        public IUIElement GetUIElement()
        {
            return this;
        }

        public void OnClick()
        {
            return;
        }


        public void SetAsChild(IUIElement element)
        {
            return;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetList(List<SceneData> list)
        {
            return;            
        }

        public void SetParent(IUIElement element)
        {
            _parent = element;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void Terminate()
        {
            Container.Unregister(this);
            _controller.DestroyElement(this);
        }
        private void Awake()
        {
            Container.Register<DifficultyDisplay>(this);

        }
        private void Start()
        {
            DevLog.elementLog.Log("Difficulty Display Started", this);
            if (_sceneData == null)
            {
                return;
            }
            _levels = _sceneData.GetDifficulity();
            if ( _levels.Count == 0)
            {
                return;
            }
            SetText(_levels[0].ToString());
            _currentLevel = _text.text;
            RequestButtons();
            ReloadLoaderButton();

        }

        private void ReloadLoaderButton()
        {
            if (_currentLoaderButton != null)
            {
                _currentLoaderButton.GetUIElement().Terminate();
            }

            _currentLoaderButton = _controller.BuildButton(_loaderButton, _parent.GetObject().transform, _parent);
            _parent.SetAsChild(_currentLoaderButton.GetUIElement());
            ISceneConnected sceneConnected = _currentLoaderButton.GetUIElement().GetSceneConnection();
            sceneConnected.AssignScene(_sceneData);
        }

        private void RequestBuildButton(ButtonSide side, Transform tParent)
        {
            IButton currentButton = _controller.BuildButton(_button, tParent, this);
            DifficultyDisplayButton button = Container.dDRegistry.ResolveDifficultyButton();
            Container.dDRegistry.ClearRegirstry();
            button.SetSide(side);
            _buttons.Add(currentButton);
        }

        private void RequestButton(ButtonSide side)
        {
            if (_button == null)
            {
                Debug.LogAssertion("Missing Button");
                return;
            }

            if (side == ButtonSide.right)
            {
                RequestBuildButton(side, _rTransform);
            }
            if (side == ButtonSide.left)
            {
                RequestBuildButton(side, _lTransform);
            }
        }

        public void SwitchDifficulty(DifficultyDisplayButton button)
        {
            ButtonSide side = button.GetSide();
                for (int i = 0; i < _levels.Count; i++)
                {
                    if (_levels[i].ToString() == _text.text)
                    {
                        if (side == ButtonSide.right)
                        {
                            DifficultyLevel level = _levels[i] + 1;
                            SetText(level.ToString());
                        }
                        if (side == ButtonSide.left)
                        {
                            DifficultyLevel level = _levels[i] - 1;
                            SetText(level.ToString());
                        }
                        foreach (IButton ibutton in _buttons)
                        {                            
                            ibutton.GetUIElement().Terminate();
                        }
                    _buttons.Clear();
                    RequestButtons();                    
                    _currentLevel = _text.text;
                    ReloadLoaderButton();
                    break;
                    }
                }
        }

        private void RequestButtons()
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                if (_levels[i].ToString() == _text.text)
                {

                    if (i < _levels.Count - 1)
                    {
                        RequestButton(ButtonSide.right);
                    }
                    if (i > 0)
                    {
                        RequestButton(ButtonSide.left);
                    }
                    break;
                }
            }
        }

        public void AssignScene(SceneData scene)
        {
            _sceneData = scene;
        }

        public SceneData GetAssignedScene()
        {
            return null;
        }
    }

}

