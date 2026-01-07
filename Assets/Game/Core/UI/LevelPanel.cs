namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelPanel : MonoBehaviour, IUIElement, ISceneConnected
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private SceneData _sceneData;
        private IUIElement _parent;
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
            throw new System.NotImplementedException();
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
            return;
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
            _controller.DestroyElement(this);

        }
    }

}
