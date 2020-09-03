using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdMob : MonoBehaviour
{
    private InterstitialAd interstitial;
    public bool premio;
    private ButtonOptions buttonOptions;

    public void Start()
    {
       
        premio = false;
        buttonOptions = GameObject.Find("ElCubo").GetComponent<ButtonOptions>();
        
        AdsReal();
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    

    private void AdsTest()
    {
#if UNITY_ANDROID
        string adTest = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adTest);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddExtra("max_ad_content_rating", "G")
            .Build();
        // Load the rewarded video ad with the request.
       // rewardBasedVideo.LoadAd(request, adTest); //el rewarded, lo voy a quitar, es muy largo
        this.interstitial.LoadAd(request);
    }

    private void AdsReal()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9593360452062088/2790724843";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

          // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("D871D88F9FBC771E5628764E872AE6B2") //Recuerda quitar en la version FINAL
            .AddExtra("max_ad_content_rating", "G")
            .Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
   

}