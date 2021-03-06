﻿using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

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

    static ILeaderboard m_Leaderboard;
    public int highScoreInt = 1000;

    public string leaderboardName = "Test";
    public string leaderboardID = "com.DefaultCompany.AdManager.test";

    public string achievement1Name = "com.DefaultCompany.AdManager.achv1";

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
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("Authenticated, checking achievements");

            // MAKE REQUEST TO GET LOADED ACHIEVEMENTS AND REGISTER A CALLBACK FOR PROCESSING THEM
            Social.LoadAchievements(ProcessLoadedAchievements); // ProcessLoadedAchievements FUNCTION CAN BE FOUND BELOW

            Social.LoadScores(leaderboardName, scores => {
                if (scores.Length > 0)
                {
                    // SHOW THE SCORES RECEIVED
                    Debug.Log("Received " + scores.Length + " scores");
                    string myScores = "Leaderboard: \n";
                    foreach (IScore score in scores)
                        myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
                    Debug.Log(myScores);
                }
                else
                    Debug.Log("No scores have been loaded.");
            });
        }
        else
            Debug.Log("Failed to authenticate with Game Center.");
    }

    void ProcessLoadedAchievements(IAchievement[] achievements)
    {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");

        // You can also call into the functions like this
        Social.ReportProgress("Achievement01", 100.0, result => {
            if (result)
                Debug.Log("Successfully reported achievement progress");
            else
                Debug.Log("Failed to report achievement");
        });
        //Social.ShowAchievementsUI();
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
            {
                rewardScreen.SetActive(true);
                Social.ShowAchievementsUI();
            }
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
