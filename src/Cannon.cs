using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    private Animator cannonAnim;
    public Animator cannonButton;
    public Animator noticeText;
    public GameObject fire;
    private bool isOn = false;

    private void Start()
    {
        cannonAnim = GetComponent<Animator>();
    }

    public void PutOnCannon()
    {
        cannonAnim.SetTrigger("On");
        cannonButton.SetTrigger("Appear");
        noticeText.SetTrigger("Appear");
        isOn = true;
    }

    public void Shoot()
    {
        if (isOn) {
            GameObject go;
            go = Instantiate(fire) as GameObject;
            go.transform.position = transform.position + new Vector3(0,0,3f);
            go.transform.rotation = transform.rotation;
        }
       
    }

    public void PutOffCannon()
    {
        cannonAnim.SetTrigger("Off");
        cannonButton.SetTrigger("Disappear");
        isOn = false;
    }
}
