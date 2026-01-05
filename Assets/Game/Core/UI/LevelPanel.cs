namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelPanel : MonoBehaviour, IUIElement
    {
        [SerializeField] private UIController _controller;
        private IUIElement _parent;
        public void Activate()
        {
            
        }

        public void CollectChildElements()
        {
            
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
            return null;
        }

        public void SetAsChild(IUIElement element)
        {
            
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
            Container.Unregister(this);
            _controller.DestroyElement(this);

        }
        private void Awake()
        {
            Container.Register(this);
        }
    }

}
