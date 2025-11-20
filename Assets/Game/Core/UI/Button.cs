namespace Game.Core.UI
{
    using TMPro;
    using UnityEngine;

    public class Button : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIController _controller;

        public void OnClick()
        {

        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }
    }
}

