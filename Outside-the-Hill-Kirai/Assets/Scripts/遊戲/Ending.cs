using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Ending : MonoBehaviour
{
    PhotonView view;
    public GameObject rawui,playercam1,playercam2,waitting,ending;
    public Text waittingtext;
    public BoxCollider trigger;
    public AudioSource audio;
    public bool passP1,passP2,triggerenterP1,triggerenterP2 = false;
    public static bool endready = false;

    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1 && playercam1 == null)
        {
            playercam1 = GameObject.Find("Player1").transform.GetChild(0).gameObject;
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2 && playercam2 == null)
        {
            playercam2 = GameObject.Find("Player2").transform.GetChild(0).gameObject;
        }

        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            if(PuzzleObjController.PasswordCorrect == true)
            {
                view.RPC("PassP1",RpcTarget.All);
            }
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            if(PuzzleObjController.DollsPuzzle == true)
            {
                view.RPC("PassP2",RpcTarget.All);
            }
        }

        if(passP1 == true && passP2 == true)
        {
            trigger.enabled = true;
        }

        if(triggerenterP1 == true && triggerenterP2 == true)
        {
            audio.Stop();
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.name == "Player1" && outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            endready = true;
            waittingtext.text = "等待阿凱吧!";
            
            view.RPC("TriggerEnterP1",RpcTarget.All);

            if(triggerenterP1 == true && triggerenterP2 == true && PuzzleObjController.End == false)
            {
                Destroy(playercam1);
                Destroy(waitting);
                PuzzleObjController.End = true;
                OriginMode.God = true;
                rawui.SetActive(false);
                ending.SetActive(true);
                StartCoroutine("AnimCD");
            }
            else
            {
                waitting.SetActive(true);
            }
        }
        else if(other.name == "Player2" && outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            endready = true;
            waittingtext.text = "等待阿浩吧!";

            view.RPC("TriggerEnterP2",RpcTarget.All);

            if(triggerenterP1 == true && triggerenterP2 == true && PuzzleObjController.End == false)
            {
                Destroy(playercam2);
                Destroy(waitting);
                PuzzleObjController.End = true;
                OriginMode.God = true;
                rawui.SetActive(false);
                ending.SetActive(true);
                StartCoroutine("AnimCD");
            }
            else
            {
                waitting.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player1" && outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            endready = false;
            view.RPC("TriggerExitP1",RpcTarget.All);
            waitting.SetActive(false);
        }
        else if(other.name == "Player2" && outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            endready = false;
            view.RPC("TriggerExitP2",RpcTarget.All);
            waitting.SetActive(false);
        }
    }

    [PunRPC]
    void PassP1()
    {
        passP1 = true;
    }

    [PunRPC]
    void PassP2()
    {
        passP2 = true;
    }

    [PunRPC]
    void TriggerEnterP1()
    {
        triggerenterP1 = true;
    }

    [PunRPC]
    void TriggerEnterP2()
    {
        triggerenterP2 = true;
    }

    [PunRPC]
    void TriggerExitP1()
    {
        triggerenterP1 = false;
    }

    [PunRPC]
    void TriggerExitP2()
    {
        triggerenterP2 = false;
    }

    IEnumerator AnimCD()
    {
        yield return new WaitForSeconds(13.5f);
        PhotonNetwork.LoadLevel(0);
    }
}
