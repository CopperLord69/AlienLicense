using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = GetComponent<T>();
    }
}
