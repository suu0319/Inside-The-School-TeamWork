using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawnORLeft : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public GameObject PlayerPrefs,Disconnect,stopmenu;
    public GameObject[] Playerobj;
    public CharacterController[] PlayerCollider;
    //玩家座標
    public static float Player1x,Player1y,Player1z;
    public static float Player2x,Player2y,Player2z;
    //是否存檔
    private bool save1,save2 = false;
    private bool spawnremote,disconnect,cd = false;
    
    void Start() 
    {
        view = this.gameObject.GetComponent<PhotonView>();
        
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 1f;

        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            if(GameData.GameData.Save1 == true && OriginMode.savedata == true)
            {
                PlayerPrefs.transform.position = new Vector3(Player1x,Player1y,Player1z);
            }
            else if(GameData.GameData.Save2 == true && OriginMode.savedata == true)
            {
                PlayerPrefs.transform.position = new Vector3(Player1x,Player1y,Player1z);
            }
            else
            {
                PlayerPrefs.transform.position = new Vector3(9.209f,0.252f,-43.461f);
            }
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            if(GameData.GameData.Save1 == true && OriginMode.savedata == true)
            {
                PlayerPrefs.transform.position = new Vector3(Player2x,Player2y,Player2z);
            }
            else if(GameData.GameData.Save2 == true && OriginMode.savedata == true)
            {
                PlayerPrefs.transform.position = new Vector3(Player2x,Player2y,Player2z);
            }
            else
            {
                PlayerPrefs.transform.position = new Vector3(49.529f,0.252f,-43.376f);
            }
        }
        
        PhotonNetwork.Instantiate(PlayerPrefs.name,PlayerPrefs.transform.position,Quaternion.identity);
    }
    void Update()
    {
        PlayerStatement();
    }

    //玩家離開房間
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        view.RPC("disconnectfuntion",RpcTarget.All);
    }

    //玩家狀態
    void PlayerStatement()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            view.RPC("disconnectfuntion",RpcTarget.All);
        }
        
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            view.RPC("Player1SpawnFind",RpcTarget.All);
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            view.RPC("Player2SpawnFind",RpcTarget.All);
        }

        if(PlayerCollider[0] != null && PlayerCollider[1] != null)
        {
            Physics.IgnoreCollision(PlayerCollider[0],PlayerCollider[1]);
        }
    }

    //尋找玩家1
    [PunRPC]
    void Player1SpawnFind()
    {
        if(Playerobj[0] == null)
        {
            Playerobj[0] = GameObject.Find(PlayerPrefs.name + "(Clone)");
            Playerobj[0].name = "Player1";
        }
           
        PlayerCollider[0] = Playerobj[0].GetComponent<CharacterController>();
    }

    //尋找玩家2
    [PunRPC]
    void Player2SpawnFind()
    {
        if(Playerobj[1] == null)
        {
            Playerobj[1] = GameObject.Find(PlayerPrefs.name + "(Clone)");
            Playerobj[1].name = "Player2";
        }
           
        PlayerCollider[1] = Playerobj[1].GetComponent<CharacterController>();
    }

    //斷線UI
    [PunRPC]
    void disconnectfuntion()
    {
        Time.timeScale = 1f;
        Disconnect.SetActive(true);
        PhotonNetwork.LeaveRoom();
        
        if(disconnect == false)
        {
            disconnect = true;
            Player.PlayerActive = false;  
            StartCoroutine(DisconnectUI());
        }
    }

    //斷線異步加載主選單
    IEnumerator DisconnectUI()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(0);
    }
}