using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdScript : MonoBehaviour, IUnityAdsListener
{
    public static AdScript instance;

    [SerializeField] string playStoreID, adName;

    void Start()
    {
        instance = this;
        Advertisement.Initialize(playStoreID, false);
        Advertisement.AddListener(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayAD();
        }
    }

    public void PlayAD()
    {
        if (Advertisement.IsReady(adName))
        {
            Advertisement.Show(adName);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        OfflineScoreBoard.SetAdCounter(3);
    }
}