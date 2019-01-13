using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {
    string score;
    /*
    public string AppID = "1787191171401774";


    public string Link = "https://play.google.com/store/apps/details?id=com.TheNextTeam.BusOrbital";



    public string Picture = "https://lh3.googleusercontent.com/Zl2qy6lVJFyg4yLTjOXjrhX4NaMlXQ9HC0971PpDbtbjVhFk7TTiM2ZdSBrj9Luvnz4=s180-rw";


    public string Caption = "Check out my brand new score: ";


    public string Description = "Interesting and free game. Play now and share with your friends.";



    public void ShareScoreOnFB()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().scoreText.text.ToString();
        Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture + "&caption=" + Caption + score + "&description=" + Description);

    }
    */

    
    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);

    }

    private void SetInit()
    {
        Debug.Log("FB init done!");
        if (FB.IsLoggedIn)
        {
            Debug.Log("fb loged in");

        }
        else
        {

        }
    }
    private void OnHideUnity(bool isGameShow)
    {
        if (!isGameShow)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /*
    public void shareFB()
    {
        FB.FeedShare (
         linkCaption: "toi dang choi game nay",
         picture: "https://scontent-hkg3-1.xx.fbcdn.net/hphotos-xta1/v/t1.0-9/11002495_681185385326261_5595920565377356380_n.jpg?oh=709c1ebbeb3d74bec276d54ae611f222&oe=560E242F",
         linkName: "check out this game",
         link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")

         );

    }
    */ 

    public void Share()
    {
        FB.ShareLink(
            contentTitle: "I scored " + score + " on Bus Driver In Action. Can you beat my score?",
            contentURL: new System.Uri("https://play.google.com/storhttps://lh3.googleusercontent.com/Zl2qy6lVJFyg4yLTjOXjrhX4NaMlXQ9HC0971PpDbtbjVhFk7TTiM2ZdSBrj9Luvnz4=s180-rwhttp://i.imgur.com/mQLDue5.png"),
            contentDescription: "Interesting and free game. Play now and share with your friends.",
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
            Debug.Log("Share error: " + result.Error);
        else if (!string.IsNullOrEmpty(result.Error))
            Debug.Log(result.PostId);
        else
            Debug.Log("Success");
    }

}
