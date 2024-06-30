using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnEntered;
    public UnityEvent OnExited;
    public event Func<Collider, bool> OnTriggerEntered;
    public event Action<Collider> OnTriggerExited;

    [SerializeField]
    private bool _destroyAfterEnter;
    [SerializeField]
    private bool _waitForResult;
    [SerializeField]
    private LayerMask _layers;
    private List<GameObject> _colliders = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (_layers != (_layers | (1 << other.gameObject.layer)))
        {
            return;
        }
        var root = other.transform.root.gameObject;
        if (!_colliders.Contains(root))
        {
            _colliders.Add(root);
        }
        OnEntered?.Invoke();
        var result = OnTriggerEntered?.Invoke(other);
        if (_destroyAfterEnter)
        {
            if (_waitForResult)
            {
                if (result.HasValue && result.Value)
                {
                    Destroy(this);
                }
        }
            else
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_layers != (_layers | (1 << other.gameObject.layer)))
        {
            return;
        }
        _colliders.Remove(other.transform.root.gameObject);
        OnExited?.Invoke();
        OnTriggerExited?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_layers != (_layers | (1 << other.gameObject.layer)))
        {
            return;
        }
        var root = other.transform.root.gameObject;
        if (!_colliders.Contains(root))
        {
            _colliders.Add(root);
            OnEntered?.Invoke();
            var result = OnTriggerEntered?.Invoke(other);
            if (_destroyAfterEnter && result.HasValue && result.Value)
            {
                Destroy(this);
            }
        }
    }
}
