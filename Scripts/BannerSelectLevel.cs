using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerSelectLevel : MonoBehaviour
{
#if UNITY_ANDROID
    // Use this for initialization
    private BannerView bannerView;
    void Awake()
    {

        string appUnitId = "ca-app-pub-9593360452062088~9540305330";
        MobileAds.Initialize(appUnitId);

    }
    private void AdsTest()
    {
        string adTest = "ca-app-pub-3940256099942544/6300978111";

        bannerView = new BannerView(adTest, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder()
            .AddExtra("max_ad_content_rating", "G")
            .Build();
        //AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    private void AdsReal()
    {
        string adUnitId = "ca-app-pub-9593360452062088/9853158348"; //banner id

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("D871D88F9FBC771E5628764E872AE6B2") //Recuerda quitar en la version FINAL
            .AddExtra("max_ad_content_rating", "G")
            .Build();
        //AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    void Start()
    {


        //AdsTest();
        AdsReal();

    }

   

    void OnDisable()
    {

        bannerView.Destroy();
    }
#endif

}
