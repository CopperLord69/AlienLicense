using UnityEngine;

public class DisappearableItem : MonoBehaviour
{
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}
