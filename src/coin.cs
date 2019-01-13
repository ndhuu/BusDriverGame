using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {
    private Animator anim;
    private bool magnet = false;
    private float magnetEffect = 25.0f;
    private bool collected = false;
    private bool called = false;

    public bool Collected { get { return collected; } }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        magnet = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().HaveMagnet;
        
        if (magnet && Vector3.Distance(playerPosition, transform.position) <= magnetEffect)
        {
            for (int i = 0; i <= 10; i++)
            {
                transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime);
            }
            collected = true;
        }
        if (collected)
        {
            anim.SetTrigger("Collected");
            if (!called)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().CoinScore();
                called = true;
            }
            Destroy(gameObject, 3f);
        }
    }

    private void Collecting()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().CoinScore();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Collected");
            Destroy(gameObject, 3f);

        }
    }
}
