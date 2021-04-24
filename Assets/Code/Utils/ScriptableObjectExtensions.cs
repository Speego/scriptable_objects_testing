using UnityEngine;

public static class ScriptableObjectExtensions
{
    public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
    {
        if (scriptableObject == null)
        {
            Debug.LogErrorFormat("ScriptableObject was null. Returning default {0} object.", typeof(T));
            return (T)ScriptableObject.CreateInstance(typeof(T));
        }

        T instance = Object.Instantiate(scriptableObject);
        return instance;
    }
}
