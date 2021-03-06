using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] points;
    public GameObject playerp2;
    public Animator animator;
    public AudioSource audio;
    public int destpoints;
    public float distance;
    private bool gonextpoint = false;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
        agent.autoBraking = false;
        
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {   
        hatelightghost();
    }

    void hatelightghost()
    {
        playerp2 = GameObject.Find("Player2");
        distance = Vector3.Distance(playerp2.transform.position,this.gameObject.transform.position);
        
        if(distance < 6f && OriginMode.God == false)
        {
            agent.SetDestination(playerp2.transform.position);
        }
        else
        {
            GotoPoint();
        }

        if(Time.timeScale == 0 || HP.currentHP == 0)
        {
            audio.Stop();
        }
        else if(!audio.isPlaying)
        {
            audio.Play();
        }

        if(HP.currentHP == 0)
        {
            gameObject.transform.GetChild(1).LookAt(playerp2.transform);
            animator.SetBool("Attack",true);
        }
    }

    //尋點
    void GotoPoint()
    {
        if(agent.remainingDistance < 0.5f && gonextpoint == false)
        {
            gonextpoint = true;
            GotoNextPoint();
            StartCoroutine(GotoNextPointCD());
        }   
    }

    //巡下個點
    void GotoNextPoint()
    {
        //AI目標 = 巡邏點陣列的座標
        agent.SetDestination(points[destpoints].position);
        //AI巡邏點+1
        destpoints = (destpoints + 1) % points.Length;
    }

    //巡下個點CD
    IEnumerator GotoNextPointCD()
    {
        yield return new WaitForSeconds(5f);
        gonextpoint = false;
    }
}
