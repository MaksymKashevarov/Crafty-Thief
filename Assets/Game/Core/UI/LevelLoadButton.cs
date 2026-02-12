namespace Game.Core.UI
{
    using System.Collections.Generic;
    using Game.Core.SceneControl;
    using TMPro;
    using UnityEngine;

    public class LevelLoadButton : MonoBehaviour, IUIElement, IButton, ISceneConnected
    {
        private UIController _controller;
        private IUIElement _parent;
        [SerializeField] private SceneData _level;
        [SerializeField] private DifficultyLevel _difficulty;
        [SerializeField] private TextMeshProUGUI _text;


        public void Activate()
        {
            return;
        }

        public void AssignScene(SceneData scene)
        {
            _level = scene;
        }

        public void CollectChildElements()
        {
            return;
        }

        public SceneData GetAssignedScene()
        {
            return _level;
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
            return null;
        }

        public IUIElement GetUIElement()
        {
            return this;
        }

        public void OnClick()
        {
            DevLog.elementLog.Log($"Loading level: {_level.GetSceneName()}", this);
            _controller.RequestSceneSwitch(this);
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
            List<IUIElement> elemets = _parent.GetChildElements();
            if (elemets == null)
            {
                DevLog.elementLog.Log("Parent has no child elements", this);
                return;
            }
            if (elemets.Contains(this))
            {
                elemets.Remove(this);
            }
            DevLog.elementLog.Log("Now list contains: ", this);
            foreach (IUIElement child in elemets)
            {               
                DevLog.elementLog.Log($"{child.GetType().Name}", this);
            }
            _controller.DestroyElement(this);
        }

        public void AssignDifficulty(DifficultyLevel difficulty)
        {
            _difficulty = difficulty;
        }
    }

}

