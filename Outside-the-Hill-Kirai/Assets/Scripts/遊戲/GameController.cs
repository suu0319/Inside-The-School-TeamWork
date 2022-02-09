using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using outsidetheschoolPUN;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    PhotonView view;
    public GameObject player,allui,stopmenu,menu,setting,Loading,deadui,lobbyui,waittingplayerui,replayui;
    public GameObject[] deaduiclose;
    //前導文字、等待文字、Loading數字
    public Text storytext,waittingttext,loadingtextcontent;
    //設定選單狀況保留
    public Slider audioslider;
    public Toggle fullscreen;
    public Dropdown quality,resolutions;

    private int scenenum;
    //死亡、協程CD
    private bool dead,cd,cd2,cd3 = false;
    //玩家重玩準備
    private bool p1replay,p2replay = false;
    
    void Start()
    {
        //限制FPS
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        
        view = this.gameObject.GetComponent<PhotonView>();
        player = GameObject.FindGameObjectWithTag("Player");
        scenenum = SceneManager.GetActiveScene().buildIndex; 
        
        if(scenenum == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }      
    }
        
    void Update()
    {
        if(scenenum == 0)
        {
            if(Input.GetKeyDown("escape") && lobbyui.activeInHierarchy == true)
            {
                BackMenu();
            }
        }

        //判斷玩家是否死亡
        Dead();
    }

    //玩家死亡
    void Dead()
    {
        if(HP.currentHP == 0 && dead == false)
        {
            dead = true;
            deadui.SetActive(true);
        }
    }

    //連線大廳(開始新遊戲)
    public void Lobby()
    {
        GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = false;
        AppearCursor();
        OriginMode.savedata = false;
        PuzzleObjController.End = false;
        menu.SetActive(false);
        lobbyui.SetActive(true);
    }
    
    //連線大廳(繼續遊戲)
    public void ContinueLobby()
    {
        GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = false;
        AppearCursor();
        OriginMode.savedata = true;
        PuzzleObjController.End = false;
        menu.SetActive(false);
        lobbyui.SetActive(true);
    }

    //設定選單
    public void Setting()
    {
        menu.SetActive(false);
        setting.SetActive(true);
        DeviceSelect.keyboardused = false;
    }

    //設定選單返回主選單or暫定選單
    public void BackMenu()
    {
        GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = true;
        menu.SetActive(true);
        setting.SetActive(false);
        OriginMode.savedata = false;
        
        if(scenenum == 0)
        {
            lobbyui.SetActive(false);
        }
    }

    //死亡畫面返回遊戲主選單
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadSceneAsync(0);
    }

    //退出遊戲房間
    public void QuitGameRoom()
    {
        //退出連線房間
        PhotonNetwork.LeaveRoom();
        //連線大廳
        lobbyui.SetActive(true);
        //等待玩家
        waittingplayerui.SetActive(false);
        waittingplayerui.GetComponent<WaittingForPlayerText>().waittingttext.text = "尋找玩家中";
        waittingplayerui.GetComponent<WaittingForPlayerText>().textanimbool = false;
        DeviceSelect.keyboardused = false;
        //玩家12控制
        outsidetheschoolPUN.ServerConnect.PlayerP1 = false;
        outsidetheschoolPUN.ServerConnect.PlayerP2 = false;
        outsidetheschoolPUN.ServerConnect.PlayerID = 0;
    }

    //選單回復到遊戲中
    public void Continue()
    {
        Player.PlayerActive = true;
        allui.SetActive(true);
        stopmenu.SetActive(false);
        MouseLook.mouselookbool = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    //重新遊玩
    public void Replay()
    {      
        OriginMode.savedata = true;
        replayui.SetActive(true);

        for(int i = 0 ; i < 4; i++)
        {
            deaduiclose[i].SetActive(false);
        }
         
        //玩家1
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            view.RPC("P1replay",RpcTarget.All);
        } 
        //玩家2
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            view.RPC("P2replay",RpcTarget.All);
        }
    }

    //退出遊戲
    public void Quit()
    {
        Application.Quit();
    }

    //出現游標
    void AppearCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //玩家1準備
    [PunRPC]
    void P1replay()
    {
        p1replay = true;
        
        if(cd == false)
        {
            cd = true;
            StartCoroutine(ReplayCD());
        }
    }

    //玩家2準備
    [PunRPC]
    void P2replay()
    {
        p2replay = true;
        
        if(cd2 == false)
        {
            cd2 = true;
            StartCoroutine(ReplayCD());
        }
    }

    //重新遊玩
    IEnumerator ReplayCD()
    {
        yield return new WaitForSeconds(1f);
        
        if(p1replay == true && p2replay == true && replayui.activeInHierarchy == true && cd3 == false)
        {
            cd3 = true;
            StartCoroutine(LoadLevelAsync());
        }
    }

    //異步加載遊戲場景1
    IEnumerator LoadLevelAsync()
    {
        PhotonNetwork.LoadLevel("Scene1");

        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            loadingtextcontent.text = (int)(PhotonNetwork.LevelLoadingProgress * 100) + "%";
            yield return new WaitForEndOfFrame();
        }     
    }
}