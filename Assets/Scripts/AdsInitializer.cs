using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoSingleton<AdsInitializer>, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public enum Ad
    {
        Rewarded,
        Annoying
    }

    public static event Action<Ad> OnAdCompleted = delegate { };
    public string myAdStatus = "";
    public bool adStarted;
    public bool adCompleted;
    private Ad _current;
    public bool AnnoyingDestroyed { get; private set; } = false;

    private void Start()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize("5622041", true, this);
        }
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    public void DestroyAnnoying()
    {
        AnnoyingDestroyed = true;
        Save();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }


    public void ShowRewarded()
    {
        Advertisement.Load("Rewarded_Android", this);
        _current = Ad.Rewarded;
    }

    public void ShowAnnoying()
    {
        if (AnnoyingDestroyed)
        {
            return;
        }
        Advertisement.Load("Interstitial_Android", this);
        _current = Ad.Annoying;
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        if (!adStarted)
        {
            Advertisement.Show(adUnitId, this);
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");

    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        adStarted = true;
        Debug.Log("Ad Started: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad Clicked: " + adUnitId);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        adCompleted = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        Debug.Log("Ad Completed: " + adUnitId);
        if (adCompleted)
        {
            adStarted = false;
            OnAdCompleted?.Invoke(_current);
        }
    }

    private void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "config", AnnoyingDestroyed.ToString());
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "config"))
        {
            var result = File.ReadAllText(Application.persistentDataPath + "config");
            AnnoyingDestroyed = bool.Parse(result);
        }
        else
        {
            AnnoyingDestroyed = false;
        }
    }
}
