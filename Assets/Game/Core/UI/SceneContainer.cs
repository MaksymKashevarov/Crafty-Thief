namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class SceneContainer : MonoBehaviour, IUIElement, IButton
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private TextMeshProUGUI _mapText;
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

        public TextMeshProUGUI GetTextComponent()
        {
            return _mapText;
        }

        public void OnClick()
        {

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
            _mapText.text = text;
        }

        public void Terminate()
        {
            _controller.DestroyElement(this);
        }
    }

}

