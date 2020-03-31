using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    bool testMode = true;
    string gameId = "3483570";
    string myPlacementReward = "rewardedVideo";
    string myPlacementVideo = "video";
    string myPlacementBanner = "banner";
    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    GameObject rewardScreen;
    int count = 0;

    // Initialize the Ads listener and service:
    void Start()
    {
        rewardScreen = GameObject.FindGameObjectWithTag("Finish");
        rewardScreen.SetActive(false);
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Load(myPlacementBanner);
        Advertisement.Banner.Show();
    }

    public void CountInteraction()
    {
        int nbActions = 3;

        count++;
        if(count >= nbActions)
        {
            count = 0;
            PlayVideo();
        }
    }

    private void PlayVideo()
    {
        Advertisement.Banner.Hide();
        if (Advertisement.IsReady())
            Advertisement.Show(myPlacementVideo);
    }

    public void RewardOnClick()
    {
        Advertisement.Banner.Hide();
        if(Advertisement.IsReady())
            Advertisement.Show(myPlacementReward);
    }

    public void CloseReward()
    {
        rewardScreen.SetActive(false);
        Advertisement.Banner.Show();
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Advertisement.Banner.Show();
            if (placementId == myPlacementReward)
                rewardScreen.SetActive(true);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Advertisement.Banner.Show();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementReward)
        {
            
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
