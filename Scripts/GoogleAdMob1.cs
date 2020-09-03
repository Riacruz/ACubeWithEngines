using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdMob1 : MonoBehaviour
{
   // private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;
    public bool premio;
    private ButtonOptions buttonOptions;

    public void Start()
    {
       
        premio = false;
        buttonOptions = GameObject.Find("ElCubo").GetComponent<ButtonOptions>();
        /*
        rewardBasedVideo = null;
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
         rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
         rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
         */

        //RequestRewardBasedVideo();
        AdsReal();
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    /*
    public void ShowAdRewarded()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            rewardBasedVideo.Show();
        }
    }

   
    void OnDestroy()
    {
        rewardBasedVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted -= HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
    }

    void OnDisable()
    {
        rewardBasedVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted -= HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
    }
    */

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

    private void RequestRewardBasedVideo()
    {
        //AdsTest();
        AdsReal();

    }
    /*
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        RequestRewardBasedVideo();
        print(Time.time + "ERROR " + premio);
    }
    
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
       
    }
    

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        buttonOptions.Premio();
        RequestRewardBasedVideo();
        print(Time.time + "CERRADO " + premio);
    }
    

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardBasedVideoRewarded event received for "
                        + (amount*0.03d).ToString() + " " + type);
        
        premio = true;
        print(Time.time + "PREMIO "+premio);
        buttonOptions.Premio();
    }

    
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    */

    void Update()
    {
        /*
        if(premio)
        {
            buttonOptions.Premio();
            print(Time.time + "PREMIO UPDATE, que es true");
            premio = false;
        }
        */
    }
}