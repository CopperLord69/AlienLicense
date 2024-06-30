using UnityEngine;
using UnityEngine.Events;

public class DisappearZone : MonoBehaviour
{
    public UnityEvent OnDisappear;

    [SerializeField]
    private Trigger _trigger;

    private void Awake()
    {
        _trigger.OnTriggerEntered += TryDisappear;

    }

    private void OnDestroy()
    {
        _trigger.OnTriggerEntered -= TryDisappear;
    }

    private bool TryDisappear(Collider collider)
    {
        if (collider.TryGetComponent(out DisappearableItem item))
        {
            item.Disappear();
            gameObject.SetActive(false);
            OnDisappear?.Invoke();
            return true;
        }
        return false;
    }
}
