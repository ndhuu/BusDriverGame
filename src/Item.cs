﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour {

    private Animator anim;
    public AudioClip ItemCollect;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            source.PlayOneShot(ItemCollect);
            anim.SetTrigger("Collected");
            Destroy(gameObject, 3f);
        }
    }
}
