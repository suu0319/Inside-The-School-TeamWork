using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadingText : MonoBehaviourPunCallbacks
{
    PhotonView view;
    //協程CD、玩家跳過
    private bool cd,cd2,P1skip,P2skip = false;
    public GameObject text,skiptext,playername;
    public Text skiptextcontent;
    public Animation textanim,skipanim;
    public AnimationClip[] animclip;
    AsyncOperation async;
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        skipanim = skiptext.GetComponent<Animation>();
        skiptextcontent = skiptext.GetComponent<Text>();
        view = this.gameObject.GetComponent<PhotonView>();
    }

    void Update() 
    {
        LoadingReady();
    }

    void LoadingReady()
    {
        switch(OriginMode.savedata)
        {
            //開始新遊戲
            case false:
            {
                if(cd == false)
                {
                    cd = true;
                    StartCoroutine(Skiploading());
                }
                
                if(Input.anyKeyDown && skiptext.activeInHierarchy == true)
                {
                    //玩家1
                    if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                    {
                        view.RPC("P1SkipLoading",RpcTarget.All);
                        skiptextcontent.text = "等待玩家跳過";
                    }
                    //玩家2
                    else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                    {
                        view.RPC("P2SkipLoading",RpcTarget.All);
                        skiptextcontent.text = "等待玩家跳過";
                    }
                }
            }
            break;
            
            //繼續遊戲
            case true:
            {
                if(cd == false)
                {
                    view.RPC("LoadGame",RpcTarget.All);
                }
            }
            break;
        }
    }

    //玩家1跳過
    [PunRPC]
    void P1SkipLoading()
    {
        P1skip = true;
        if(P1skip == true && P2skip == true && cd2 == false)
        {
            cd2 = true;
            StartCoroutine(LoadLevelAsync());
        }
    }

    //玩家2跳過
    [PunRPC]
    void P2SkipLoading()
    {
        P2skip = true;
        if(P1skip == true && P2skip == true && cd2 == false)
        {
            cd2 = true;
            StartCoroutine(LoadLevelAsync());
        }
    }

    //載入場景
    [PunRPC]
    void LoadGame()
    {
        cd = true;
        text.SetActive(false);
        skiptext.SetActive(true);
        playername.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    
    //跳過文字出現
    IEnumerator Skiploading()
    {
        yield return new WaitForSeconds(8f);
        skiptext.SetActive(true);
    }

    //異步加載遊戲場景1
    IEnumerator LoadLevelAsync()
    {
        PhotonNetwork.LoadLevel("Scene1");

        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            skiptextcontent.text = (int)(PhotonNetwork.LevelLoadingProgress * 100) + "%";
            yield return new WaitForEndOfFrame();
        }     
    }
}