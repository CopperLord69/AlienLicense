using UnityEngine;

public class DestroyAdsButton : MonoBehaviour
{
    void Start()
    {
        if(AdsInitializer.Instance.AnnoyingDestroyed)
        {
            gameObject.SetActive(false);
        }
    }

}
