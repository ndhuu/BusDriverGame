using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public void onPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game State");
    }

    public void onScoreBoardButton()
    {

    }

    public void onSettingButton()
    {

    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
