using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Curtain : MonoBehaviour
{
    public Animator animator;
    public AudioSource audio;

    void Start()
    {
        animator.enabled = false;
        audio.enabled = false;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            animator.enabled = true;
            audio.enabled = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            animator.enabled = false;
            audio.enabled = false;
        }
    }
}
