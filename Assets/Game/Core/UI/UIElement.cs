namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIElement : MonoBehaviour, IUIElement
    {
        [SerializeField] private Image _slotImage;
        [SerializeField] private Sprite _slotDefaultImage;
        [SerializeField] private bool _isClickable;
        [SerializeField] private UIController _controller;
        [SerializeField] private GameObject _buttonPanel;
        private GameObject _activeElement;
        [SerializeField] private Item _itemInSlot;

        public GameObject GetActiveElement()
        {
            return _activeElement;
        }

        public Image GetSlotImage()
        {
            return _slotImage;
        }

        public Item getSlotItem()
        {
            return _itemInSlot;
        }

        public void SetSlotItem(Item item)
        {
            _itemInSlot = item;
        }

        public void OnClicked()
        {
            if(_activeElement != null)
            {
                Debug.Log("Already Opened");
                return;
            }
            _controller.ShowElement(_buttonPanel, transform, this);
        }


        public void SetActiveElement(GameObject element)
        {
            _activeElement = element;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetRootElement(IUIElement root)
        {
            Debug.LogWarning("This element does not implement SetRootElement!");
            return;
        }
    }

}

