using UnityEngine;

public class WakeUpZone : MonoBehaviour
{
    [SerializeField]
    private Trigger _trigger;

    private void Awake()
    {
        _trigger.OnTriggerEntered += TryWakeUp;
    }

    private void OnDestroy()
    {
        _trigger.OnTriggerEntered -= TryWakeUp;
    }

    private bool TryWakeUp(Collider collider)
    {
        if (collider.TryGetComponent(out Character character))
        {
            character.WakeUp();
            return true;
        }
        return false;
    }
}
