using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour {
    public Text soundText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (AudioListener.volume == 0)
        {
            soundText.text = "Off";
        }
        else
        {
            soundText.text = "On";
        }
	}

    public void TurnOnOffSound()
    {
        Debug.Log("skjbf");
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

}
