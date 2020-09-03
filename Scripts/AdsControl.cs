using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdsControl : MonoBehaviour
{
    //private RewardedAdsScript adsRewarded;
    public Image setupButton;

    void Awake()
    {
       // adsRewarded = GameObject.Find("Ads").GetComponent<RewardedAdsScript>();
        setupButton = GameObject.Find("setupButton").GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //adsRewarded = GameObject.Find("Ads").GetComponent<RewardedAdsScript>();
        setupButton = GameObject.Find("setupButton").GetComponent<Image>();
       // adsRewarded.HideBanner();
    }

    
    void Update()
    {
        /*
        if(adsRewarded.isBannerShow() )
        {
            adsRewarded.HideBanner();
        }
        if(Input.GetKeyDown (KeyCode.P))
        {
            ShowAdsRewarded();
        }
        */
    }
    public void AtEndAds()
    {
        Time.timeScale = 1;
        setupButton.raycastTarget = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Propulsor>().Go();
    }
    public void ShowAdsRewarded()
    {
        setupButton.raycastTarget = false;
       // adsRewarded.ShowAdsRewarded(); ;
    }

    public void ShowAdsNoRewarded()
    {
        setupButton.raycastTarget = false;
        //adsRewarded.ShowAdsNoRewarded (); ;
    }
}
