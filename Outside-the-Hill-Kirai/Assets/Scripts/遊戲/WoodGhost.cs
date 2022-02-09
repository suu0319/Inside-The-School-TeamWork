using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class WoodGhost : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public AudioSource audio;
    public GameObject playerp1;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
        playerp1 = GameObject.Find("Player1");
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        woodghost();
    }

    void woodghost()
    {
        if(RayControl.woodghoststop == false)
        {   
            agent.SetDestination(playerp1.transform.position);
            agent.isStopped = false;
            animator.enabled = true;
            
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            agent.isStopped = true;
            animator.enabled = false;
            
            if(audio.isPlaying)
            {
                audio.Stop();
            }
        }
            
        if(Time.timeScale == 0)
        {
            audio.Stop();
        }

        if(HP.currentHP == 0)
        {
            gameObject.transform.GetChild(1).LookAt(playerp1.transform);
            animator.SetBool("Attack",true);
            
            if(audio.isPlaying)
            {
                audio.Stop();
            }
        }
    }
}