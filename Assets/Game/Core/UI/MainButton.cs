namespace Game.Core.UI
{
    using TMPro;
    using UnityEngine;

    public class MainButton : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIController _controller;

        public void OnClick()
        {
            Debug.Log("Check!");
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }
    }
}

