using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PuzzleObjController : MonoBehaviour
{
    PhotonView view;
    public GameObject woodghost;
    public GameObject[] PlayerNumber;
    public GameObject[] PlayerP2;
    private bool eightzerofive,woodghostbool = false;
    public static bool Key_Student,Key_Biology,Key_General,FireExtinguish,SmokeBool,Key_BiologyPuzzle,IronDoorPuzzle,IronDoorSwitch,EightPuzzle,ZeroPuzzle,FivePuzzle,SheetMusic902,SheetMusic802,PianoFinish,SteamBoxPuzzle,SteamBoxSwitch,StudentCardP1,StudentCardP2,BookPuzzle,PasswordCorrect,ArtPuzzle,DollsPuzzle,End = false;
    
    void Awake() 
    {
        view = this.gameObject.GetComponent<PhotonView>();

        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            PlayerNumber[0].SetActive(true);
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            PlayerNumber[1].SetActive(true);
        }
    }

    void Update()
    {
        if(PuzzleObjController.EightPuzzle == true && PuzzleObjController.ZeroPuzzle == true && PuzzleObjController.FivePuzzle == true && eightzerofive == false) 
        {
            eightzerofive = true;
            view.RPC("Trash",RpcTarget.All);
        }
        if(PuzzleObjController.SteamBoxSwitch == true && woodghostbool == false)
        {
            woodghostbool = true;
            woodghost.SetActive(false);
        }
    }

    [PunRPC]
    void Trash()
    {
        PlayerP2[0].SetActive(false);
        PlayerP2[1].SetActive(true);
    }
}