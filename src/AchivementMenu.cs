using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchivementMenu : MonoBehaviour {
    public GameObject connectedMenu, disconnectedMenu;

    private void Awake()
    {
        PlayGamesPlatform.Activate();
        OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);
    }

    //Google Play Services
    public void OnConnectClick()
    {
        PlayGamesPlatform.Instance.Authenticate((bool success) => {
            if (success)
            {
                OnConnectionResponse(success);
                Debug.Log("Sign In Success" + success);
                UnLockAchivement(GPGSIds.achievement_login);
            }
            else
            {
                Debug.Log("Sign In Success" + success);
            }
        }, true);
    }

    private void OnConnectionResponse(bool authenticated)
    {
        if (authenticated)
        {
            connectedMenu.SetActive(true);
            disconnectedMenu.SetActive(false);
        }
        else
        {
            connectedMenu.SetActive(false);
            disconnectedMenu.SetActive(true);
        }
    }
    
    // Add achivements
    public void OnAchivementClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void UnLockAchivement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, (bool success) =>
        {
            Debug.Log("achivement Unlocked" + success.ToString());
        });
    }

    // Add Leadboard
    public void OnLeaderBoardClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
}
