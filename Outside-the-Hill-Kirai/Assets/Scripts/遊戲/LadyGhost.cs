using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class LadyGhost : MonoBehaviour
{
    private NavMeshAgent agent;
    //private Animator animator;
    public Transform[] points;
    public GameObject playerp2;
    public float distance;
    private int destpoints;
    //協程CD
    private bool gonextpoint = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        //animator = this.gameObject.GetComponent<Animator>();
        playerp2 = GameObject.Find("Player2");
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
        distance = Vector3.Distance(playerp2.transform.position,this.gameObject.transform.position);
        
        //追蹤玩家
        if(distance < 6f)
        {
            agent.SetDestination(playerp2.transform.position);
            //animator.SetBool("Attack",true);
        }
        //巡點
        else
        {
            GotoPoint();
        }

        //玩家死亡
        if(HP.currentHP == 0)
        {
            gameObject.transform.GetChild(1).LookAt(playerp2.transform);
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
        //animator.SetBool("Attack",false);
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
