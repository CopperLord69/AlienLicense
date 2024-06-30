using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public static event Action OnWin;

    [SerializeField]
    private Trigger _trigger;

    private void Awake()
    {
        _trigger.OnTriggerEntered += Win;
    }


    private void OnDestroy()
    {
        _trigger.OnTriggerEntered -= Win;    
    }

    private bool Win(Collider collider)
    {
        if (collider.TryGetComponent(out Character character))
        {
            OnWin?.Invoke();
            return true;
        }
        return false;
    }
}
