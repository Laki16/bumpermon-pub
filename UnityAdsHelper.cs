using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
 
public class UnityAdsHelper : MonoBehaviour
{
    private const string android_game_id = "2703328";
    private const string ios_game_id = "xxxxxxx";

    private const string video_id = "video";
    private const string rewarded_video_id = "rewardedVideo";
    private const string rewarded_video_15secs_id = "rewardedvideo_15secs";

    private int index;
    public CrateController crateController;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }

    public void ShowAd() //continue btn
    {
        index = 0;
        if (Advertisement.IsReady(video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(video_id, options);
        }
    }

    public void ShowRewardedAd(int _index) //rewarded ads
    {
        index = _index;
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_id, options);
        }
    }

    public void ShowRewardedAd_15secs() //not used
    {
        if (Advertisement.IsReady(rewarded_video_15secs_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_15secs_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");

                    switch (index)
                    {
                        case 0: Time.timeScale = 1; break;
                        case 1:
                            PlayerPrefs.SetString("Time1", DateTime.Now.ToString());
                            crateController.BtnOnCrate(1);
                            break;
                        case 2:
                            PlayerPrefs.SetString("Time2", DateTime.Now.ToString());

                            int gem = PlayerPrefs.GetInt("Gem");
                            gem += 10;
                            PlayerPrefs.SetInt("Gem", gem);
                            PlayerPrefs.Save();
                            CloudVariables.SystemValues[1] = gem;
                            PlayGamesScript.Instance.SaveData();
                            break;
                        case 3:
                            PlayerPrefs.SetString("Time3", DateTime.Now.ToString());
                            crateController.BtnOnCrate(4);
                            break;
                    }

                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");

                    Time.timeScale = 1;

                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");

                    // to do ...
                    // 광고 시청에 실패했을 때 처리

                    break;
                }
        }
    }
}