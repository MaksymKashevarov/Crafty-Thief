

using UnityEngine;
public static class DevLog
{
    public static readonly ElementLog elementLog = new ElementLog();

    public static void Log(string message)
    {
        Debug.Log(
            $"<color=#2196F3>[DevLog]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }

    public static void LogAssetion(string message)
    {
        Debug.LogAssertion(
            $"<color=#F44336>[DevLog]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }

}
