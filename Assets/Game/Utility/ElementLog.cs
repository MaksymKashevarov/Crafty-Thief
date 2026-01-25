using Game.Core.UI;
using UnityEngine;
public class ElementLog
{
    public void Log(string message, IUIElement element)
    {
        if (element == null)
        {
            Debug.LogWarning("Missing element for logging.");
            return;
        }
        Debug.Log(
            $"<color=#4CAF50>[ElementLog]</color> " +
            $"<color=#4CAF50>[{element.GetType().Name}]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }
}
