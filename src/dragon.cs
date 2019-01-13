using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : MonoBehaviour {
    private Animator anim;
    public GameObject fire;
    private BoxCollider boxCol;
    private bool takenOff = false;
    private float count = 0f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Fire":
                anim.SetTrigger("Take Off");
                boxCol.center = new Vector3(0,7,-1);
                takenOff = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().ScoreForDragon();
                break;
            case "Player":
                other.gameObject.GetComponent<playerController>().Crash();
                break;   
        }
    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!takenOff)
        {
            anim.SetTrigger("Flame Attack");
            if (count >= 3f)
            {
                GameObject go;
                go = Instantiate(fire) as GameObject;
                go.transform.position = transform.position - new Vector3((float)(Random.Range(-400, 410)) / 100, 0, 5);
                go.transform.rotation = transform.rotation;
                count = 0;
            }

            count += Time.deltaTime;
        }
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().HaveCannon)
        {
            Destroy(gameObject);
        }
    }
}
