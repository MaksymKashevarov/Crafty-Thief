using Game.Core.SceneControl;
using System.Collections.Generic;
using TMPro;

namespace Game.Core.UI
{
    public interface IButton
    {
        void SetText(string text);
        TextMeshProUGUI GetTextComponent();
        void SetParent(IUIElement element);
        void SetController(UIController controller);
        void SetList(List<SceneData> list);
        void OnClick();
        void Terminate();
    }

}

