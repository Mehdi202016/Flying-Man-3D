using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] string gameId= "4748099";
    [SerializeField] string InterAd = "Interstitial_Android";
    [SerializeField] string BannerAd = "Banner_Android";
    [SerializeField] string RewardAd = "Rewarded_Android";
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] bool TestMode=true;

    void Start()
    {
        Advertisement.Initialize(gameId,TestMode);
        Advertisement.Banner.SetPosition(_bannerPosition);
        StartCoroutine(ShowBanner());
    }

    //BANNER ADS
    IEnumerator ShowBanner()
    {
        while (!Advertisement.IsReady(BannerAd))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(BannerAd);
    }

    //INTERSTITIAL ADS
    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(InterAd))
        {
            Advertisement.Show(InterAd);
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    //REWARD ADS 

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(RewardAd))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(RewardAd, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
