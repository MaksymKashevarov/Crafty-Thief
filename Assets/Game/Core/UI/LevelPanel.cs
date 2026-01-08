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
        [SerializeField] private List<DifficultyLevel> _levels = new List<DifficultyLevel>();

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
            SetAsChild(displayElement);
            DifficultyDisplay currentDisplay = Container.Resolve<DifficultyDisplay>();
            if (currentDisplay == null)
            {
                Debug.LogAssertion("MIssing Current Display");
                return;
            }
            _levels = _sceneData.GetDifficulity();
            display.SetText(_levels[0].ToString());
            ShowDDButtons(display, currentDisplay);
            
            
        }

        private void ShowDDButtons(IButton display, DifficultyDisplay currentDisplay)
        {
            TextMeshProUGUI text = display.GetTextComponent();
            string stringText = text.text;
            DifficultyLevel difficultyLevel;
            foreach (DifficultyLevel level in _levels)
            {
                if (level.ToString() == stringText)
                {
                    difficultyLevel = level;
                    for (int i = 0; i < _levels.Count; i++)
                    {
                        if (_levels[i] == difficultyLevel)
                        {
                            if (i - 1 >= 0)
                            {
                                currentDisplay.ShowButtons(true, false);
                            }
                            else
                            {
                                Debug.LogWarning("Destroying button");
                            }
                            if (i + 1 < _levels.Count)
                            {
                                currentDisplay.ShowButtons(false, true);
                            }
                            else
                            {
                                Debug.LogWarning("Destroying button");
                            }
                            break;
                        }

                    }
                    break;

                }
            }

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

        public void SetAsChild(IUIElement element)
        {
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


    }

}
