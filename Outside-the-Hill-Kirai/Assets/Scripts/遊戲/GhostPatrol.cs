using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class GhostPatrol : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] points;
    public GameObject playerp1;
    public AudioSource audio;
    public float distance;
    private int destpoints;
    //協程CD
    private bool gonextpoint = false;
  
    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        audio = this.gameObject.GetComponent<AudioSource>();
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        GhostTrack();
    }

    //怪物追蹤判定
    void GhostTrack()
    {
        playerp1 = GameObject.Find("Player1");
        distance = Vector3.Distance(playerp1.transform.position,this.gameObject.transform.position);
        
        //追蹤玩家
        if(distance < 6f && OriginMode.God == false)
        {
            agent.SetDestination(playerp1.transform.position);
        }
        //巡點
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

        //玩家死亡
        if(HP.currentHP == 0)
        {
            gameObject.transform.GetChild(1).LookAt(playerp1.transform);
        }
    }

    //巡點
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