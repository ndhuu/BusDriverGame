using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class Score : MonoBehaviour {
    public float score = 0.0f;
    public Text scoreText;
    public Text coinText;
    private int diffLevel = 1;
    private int maxDiffLevel = 20;
    private int scoreToNextLevel = 10;
    public int coinScore = 0, tempCoinScore = 0, tempCannonCoinScore = 0;//tempCoinScore apply for robot button, tempCannonCoinScore apply for cannon item
    private const float COIN_SCORE_AMOUNT = 2.5f;
    public Text bestHighLocalScore;

    //Robot Button
    public Animator robotButton;

    //Death Menu
    public Animator deathMenuAnim;
    public Text deathScoreText, deathCoinText;
    public AudioClip deathSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //Advertisements
        Advertisement.Initialize("2643555");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<playerController>().IsDead)
            return;
        //do cannon item
        if (tempCannonCoinScore >=50)
        {
            tempCannonCoinScore = 0;
            if (!GetComponent<playerController>().RobotForm && !GetComponent<playerController>().HaveCannon)
            {
                GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>().makeCannonItem();
            }
        }
        //do robot  buttton
        if (tempCoinScore >= 100)
        {
            if (!GetComponent<playerController>().HaveCannon && !GetComponent<playerController>().RobotForm)
            {
                robotButton.SetTrigger("Lighten Up");
                GetComponent<playerController>().ActivateRobotButton();
            }
            tempCoinScore = 0;
        }
        if (score >= scoreToNextLevel)
            LevelUp();
        score += (Time.deltaTime * diffLevel); 
        scoreText.text = ((int)(score + coinScore * COIN_SCORE_AMOUNT)).ToString();
        coinText.text = ((int)coinScore).ToString();
        bestHighLocalScore.text = PlayerPrefs.GetInt("Hiscore").ToString();
    }

    public void DeactivateRobotButton()
    {
        robotButton.SetTrigger("Normalize");
    }

    public void CoinScore ()
    {
        coinScore++;
        tempCoinScore++;
        tempCannonCoinScore++;
        if (coinScore == 500)
        {
            UnLockAchivement(GPGSIds.achievement_collect_500_coins);
        }
    }

    public void ScoreForDragon()
    {
        score += 10;
    }

    private void LevelUp()
    {
        if (diffLevel == maxDiffLevel)
            return;
        scoreToNextLevel *= 2;
        diffLevel++;

        GetComponent<playerController>().SetSpeed(diffLevel * 2);
    }

    public void onPlayButton()
    {
        StoreCoins();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game State");
    }

    public void onBackButton()
    {
        StoreCoins();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void OnDeath()
    {
        source.PlayOneShot(deathSound, 1f);
        deathScoreText.text = ((int)(score + coinScore * COIN_SCORE_AMOUNT)).ToString();
        deathCoinText.text = ((int)coinScore).ToString();
        deathMenuAnim.SetTrigger("Dead");
        ReportScore((int)(score + coinScore * COIN_SCORE_AMOUNT));

        //Check high score
        if ((score + coinScore* COIN_SCORE_AMOUNT) > PlayerPrefs.GetInt("Hiscore"))
        {
            PlayerPrefs.SetInt("Hiscore", (int)(score + coinScore * COIN_SCORE_AMOUNT));
        }
    }

    //Achievement
    public void UnLockAchivement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, (bool success) =>
        {
            Debug.Log("achivement Unlocked" + success.ToString());
        });
    }

    public void ReportScore(int score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_highscore, (bool success) =>
        {
            Debug.Log("report Leaderboard" + success.ToString());
        });
    }

    public void RequestRevive()
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = Revive;
        Advertisement.Show("rewardedVideo", so);
    }

    public void Revive(ShowResult sr)
    {
        if (sr == ShowResult.Finished)
        {
            GetComponent<playerController>().Revive();
            deathMenuAnim.SetTrigger("Alive");
            diffLevel = 1;
        }
        else
        {
            onBackButton();
        }
    }

    public void StoreCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinScore);
    }
}
