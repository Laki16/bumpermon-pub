using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

using System;
using System.Text;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GPGS : MonoBehaviour
{
    private string leaderboardId = GPGSIds.leaderboard_global_score_ranking;
    public Text stateText;                  // 상태 메세지
    private Action<bool> signInCallback;    // 로그인 성공 여부 확인을 위한 Callback 함수

    private static GPGS instance;
    public static GPGS Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GPGS>();

                if(instance == null)
                {
                    instance = new GameObject("GPGS").AddComponent<GPGS>();
                }
            }
            return instance;
        }
    }

    public bool isProcessing
    {
        get;
        private set;
    }

    public string loadedData
    {
        get;
        private set;
    }

    private const string m_saveFileName = "game_save_data";

    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }
    
    void Awake()
    {
        // 안드로이드 빌더 초기화
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);

        // 구글 플레이 로그를 확인할려면 활성화
        PlayGamesPlatform.DebugLogEnabled = false;

        // 구글 플레이 활성화
        PlayGamesPlatform.Activate();

        // Callback 함수 정의
        signInCallback = (bool success) =>
        {
            if (success)
            {
                stateText.text = "SignIn Success!";
                stateText.text = PlayGamesPlatform.Instance.localUser.userName;
            }
            else
            {
                stateText.text = "SignIn Fail!";
            }
        };
    }

    private void Start()
    {
        Login();
    }

    // 로그인
    public void Login()
    {
        // 로그아웃 상태면 호출
        if (PlayGamesPlatform.Instance.IsAuthenticated() == false)
            PlayGamesPlatform.Instance.Authenticate(signInCallback);
    }

    // 로그아웃
    public void Logout()
    {
        // 로그인 상태면 호출
        if (PlayGamesPlatform.Instance.IsAuthenticated() == true)
        {
            stateText.text = "Log In";
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    #region LeaderBoard Method
    public void LeaderBoard()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(AuthenticateHandler);
    }

    void AuthenticateHandler(bool isSuccess)
    {
        if (isSuccess)
        {
            float highScore = PlayerPrefs.GetInt("Score", 0);
            Social.ReportScore((long)highScore, leaderboardId, (bool success) =>
            {
                if (success)
                {
                    PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
                }
                else
                {
                    Debug.Log("Save Failed.");
                }
            });
        }
        else
        {
            Debug.Log("Login Failed.");
        }
    }
    #endregion LeaderBoard Method

    #region Achievement Method
    public void CompleteFirstRun()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(GPGSIds.achievement_1, 100.0, (bool success)=>{
            if (!success) { Debug.Log("Report Fail!"); }
            else
            {
                PlayerPrefs.SetInt("FirstRun",1);
                ShowAchivementUI();
            }
        });
    }

    public void CompleteFirstCombo()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(GPGSIds.achievement_2, 100.0, (bool success) => {
            if (!success) { Debug.Log("Report Fail!"); }
            else
            {
                PlayerPrefs.SetInt("FirstCombo", 1);
                ShowAchivementUI();
            }
        });
    }

    public void ShowAchivementUI()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ShowAchievementsUI();
    }
    #endregion Achievement Method

    #region Cloud Saving
    private void ProcessCloudData(byte[] cloudData)
    {
        if(cloudData == null)
        {
            Debug.Log("No Data saved to the cloud");
            return;
        }

        string progress = BytesToString(cloudData);
        loadedData = progress;
    }

    public void LoadFromCloud(Action<string> afterLoadAction)
    {
        if(isAuthenticated && !isProcessing)
        {
            StartCoroutine(LoadFromCloudRoutin(afterLoadAction));
        }
        else
        {
            Login();
        }
    }

    private IEnumerator LoadFromCloudRoutin(Action<string> loadAction)
    {
        isProcessing = true;
        Debug.Log("Loading game progress from the cloud.");

        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
            m_saveFileName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            OnFileOpenToLoad);

        while (isProcessing)
        {
            yield return null;
        }

        loadAction.Invoke(loadedData);
    }

    public void SaveToCloud(string dataToSave)
    {
        if (isAuthenticated)
        {
            loadedData = dataToSave;
            isProcessing = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                m_saveFileName,
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                OnFileOpenToSave);
        }
        else
        {
            Login();
        }
    }

    private void OnFileOpenToSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            byte[] data = StringToBytes(loadedData);

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

            SavedGameMetadataUpdate updatedMetadata = builder.Build();

            ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(metaData,
                updatedMetadata, data, OnGameSave);
        }
        else
        {
            Debug.LogWarning("Error opening saved game" + status);
        }
    }

    private void OnFileOpenToLoad(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(metaData, OnGameLoad);
        }
        else
        {
            Debug.LogWarning("Error opening saved Game" + status);
        }
    }

    private void OnGameLoad(SavedGameRequestStatus status, byte[] bytes)
    {
        if(status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error saving" + status);
        }
        else
        {
            ProcessCloudData(bytes);
        }

        isProcessing = false;
    }

    private void OnGameSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if(status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error saving" + status);
        }
        isProcessing = false;
    }

    private byte[] StringToBytes(string stringToConvert)
    {
        return Encoding.UTF8.GetBytes(stringToConvert);
    }

    private string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
    #endregion Cloud Saving

}