using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static event Action OnWakeUp;

    public void WakeUp()
    {
        OnWakeUp?.Invoke();
    }
}
