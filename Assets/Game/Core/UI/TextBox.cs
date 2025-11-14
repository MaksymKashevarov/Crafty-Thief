namespace Game.Core.UI
{
    using TMPro;
    using UnityEngine;

    public class TextBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        public void SetText(string input)
        {
            _textMesh.text = input;
        }

        public string GetText()
        {
            return _textMesh.text;
        }
    }

}
