

using System;
using UnityEngine;
public static class DevLog
{
    public static readonly ElementLog elementLog = new ElementLog();

    public static void Log(string message, object obj)
    {
        Debug.Log(
            $"<color=#2196F3>[{FixType(obj).Name}]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }

    private static Type FixType(object obj)
    {
        Type type = obj.GetType();
        if (type == null)
        {
            Debug.LogAssertion("Object type is null!");
            return null;
        }
        return type;
    }

    public static void LogAssetion(string message, object obj)
    {
        Debug.LogAssertion(
            $"<color=#F44336>[{FixType(obj).Name}]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }

    public static void LogWarning(string message, object obj)
    {
        Debug.LogWarning(
            $"<color=#FF5722>[{FixType(obj).Name}]</color> " +
            $"<color=#FFC107>{message}</color>"
            );
    }

}
