using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ButtonEInteractionObj : MonoBehaviour
{
    PhotonView view;
    public GameObject ui2d,uiclose,puzzleobj,pianoobj,playercam,rawui;
    private Animation anim;
    private AudioSource audio;
    public AudioClip[] audioclip;
    private bool pianofinish = false;
    //UI是否出現
    public static bool puzzleuiappear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
        audio = this.gameObject.GetComponent<AudioSource>();

        if(this.gameObject.name == "配置圖" || this.gameObject.name == "蒸飯箱使用手冊")
        {
            view = this.gameObject.GetComponent<PhotonView>();
        }
    }

    //撿取物品(刪掉物件)
    void GetObject()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(this.gameObject.name)
            {
                //撿取學務處鑰匙
                case "Key_Student":
                {
                    if(PuzzleObjController.Key_Student == false)
                    {
                        PuzzleObjController.Key_Student = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //撿取生物教室鑰匙
                case "Key_Bilogy":
                {
                    if(PuzzleObjController.Key_Biology == false)
                    {
                        PuzzleObjController.Key_Biology = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //撿取教務處鑰匙
                case "Key_General":
                {
                    if(PuzzleObjController.Key_General == false)
                    {
                        PuzzleObjController.Key_General = true;
                        Destroy(puzzleobj);
                    }
                }
                break;
                
                //撿取滅火器
                case "FireExtinguish":
                {
                    if(PuzzleObjController.FireExtinguish == false)
                    {
                        PuzzleObjController.FireExtinguish = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //滅火
                case "Smoke":
                {
                    if(PuzzleObjController.FireExtinguish == true && PuzzleObjController.SmokeBool == false)
                    {
                        PuzzleObjController.SmokeBool = true;
                        audio.Play();
                        StartCoroutine(ObjSfx());
                    }
                }
                break;

                //樂譜上902
                case "樂譜上902":
                {
                    if(PuzzleObjController.SheetMusic902 == false)
                    {
                        PuzzleObjController.SheetMusic902 = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //樂譜下802
                case "樂譜下802":
                {
                    if(PuzzleObjController.SheetMusic802 == false)
                    {
                        PuzzleObjController.SheetMusic802 = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //P1阿浩學生證
                case "阿浩學生證":
                {
                    if(PuzzleObjController.StudentCardP1 == false)
                    {
                        PuzzleObjController.StudentCardP1 = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //P2阿凱學生證
                case "阿凱學生證":
                {
                    if(PuzzleObjController.StudentCardP2 == false)
                    {
                        PuzzleObjController.StudentCardP2 = true;
                        Destroy(puzzleobj);
                    }
                }
                break;

                //巫毒娃娃
                case "wudu1": case "wudu2": case "wudu3":
                {
                    Destroy(puzzleobj);
                }
                break;
            }
        }
    }

    //顯示2DUI
    void Uiappear()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(this.gameObject.name != "Piano")
            {
                Time.timeScale = 0f;
                Player.PlayerActive = false;
                MouseLook.mouselookbool = false;
                puzzleuiappear = true;
                ui2d.SetActive(true);
                uiclose.SetActive(true);
            }

            //玩家1
            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                switch(this.gameObject.name)
                {
                    //生物教室鑰匙謎題
                    case "生物教室保管表":
                    {
                        PuzzleObjController.Key_BiologyPuzzle = true;
                    }   
                    break;

                    //電器配置圖謎題
                    case "配置圖":
                    {
                        view.RPC("IronDoorSwitch",RpcTarget.All);
                    }
                    break;

                    //鋼琴謎題物件
                    case "Piano":
                    {
                        if(PuzzleObjController.SheetMusic902 == true && PuzzleObjController.PianoFinish == false)
                        {   
                            Time.timeScale = 0f;
                            Player.PlayerActive = false;
                            MouseLook.mouselookbool = false;
                            puzzleuiappear = true;
                            ui2d.SetActive(true);
                            uiclose.SetActive(true);
                            pianoobj.SetActive(true);
                        }
                    }
                    break;
                }
            }
            //玩家2
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                switch(this.gameObject.name)
                {
                    //國文課本(0)謎題
                    case "國文課本(0)":
                    {
                        PuzzleObjController.ZeroPuzzle = true;
                    }
                    break;

                    //廁所冷笑話(5)謎題
                    case "廁所冷笑話(5)":
                    {
                        PuzzleObjController.FivePuzzle = true;
                    }
                    break;

                    //鋼琴謎題物件
                    case "Piano":
                    {
                        if(PuzzleObjController.SheetMusic802 == true && PuzzleObjController.PianoFinish == false)
                        {   
                            Time.timeScale = 0f;
                            Player.PlayerActive = false;
                            MouseLook.mouselookbool = false;
                            puzzleuiappear = true;
                            ui2d.SetActive(true);
                            uiclose.SetActive(true);
                            pianoobj.SetActive(true);
                        }
                    }
                    break;
                }
            } 
        }
        else if(Input.GetKeyDown("escape"))
        {
            Time.timeScale = 1f;
            puzzleuiappear = false;      
            Player.PlayerActive = true;
            MouseLook.mouselookbool = true;
            ui2d.SetActive(false);
            uiclose.SetActive(false);

            //玩家1
            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                switch(this.gameObject.name)
                {
                    //電器配置圖謎題
                    case "配置圖":
                    {
                        if(PlayerText.IronDoorText == false)
                        {
                            PlayerText.IronDoorText = true;
                        }
                    }
                    break;

                    //鋼琴謎題物件
                    case "Piano":
                    {
                        pianoobj.SetActive(false);
                    }
                    break;

                    //電腦謎題
                    case "電腦謎題":
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        puzzleobj.SetActive(false);
                    }
                    break;
                }
            }
            //玩家2
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                switch(this.gameObject.name)
                {
                    //組長字條
                    case "組長報修":
                    {
                        if(PlayerText.GeneralText == false)
                        {
                            PlayerText.GeneralText = true;
                        }
                    }
                    break;

                    //蒸飯箱使用手冊文字
                    case "蒸飯箱使用手冊":
                    {
                        if(PlayerText.SteamBoxText == false)
                        {
                            PlayerText.SteamBoxText = true;
                        }
                        
                        view.RPC("SteamBoxSwitch",RpcTarget.All);
                    }
                    break;

                    //鋼琴謎題物件
                    case "Piano":
                    {
                        pianoobj.SetActive(false);
                    }
                    break;
                }
            }
        }
    }

    //文字觸發
    void Textappear()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //玩家2
            if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                switch(this.gameObject.name)
                {
                    //塗鴉牆
                    case "塗鴉牆(8)":
                    {
                        PuzzleObjController.EightPuzzle = true;

                        if(PlayerText.EightPuzzleText == false)
                        {
                            PlayerText.EightPuzzleText = true;
                        }
                    }
                    break;
                }
            }
        }
    }

    //鐵捲門開關觸發
    [PunRPC]
    void IronDoorSwitch()
    {
        Debug.Log("鐵捲門可以按了");
        PuzzleObjController.IronDoorPuzzle = true;
    }

    //蒸飯室可以引爆
    [PunRPC]
    void SteamBoxSwitch()
    {
        Debug.Log("蒸飯室可以引爆了");
        PuzzleObjController.SteamBoxPuzzle = true;
    }

    IEnumerator ObjSfx()
    {
        yield return new WaitForSeconds(1f);
        Destroy(puzzleobj);
    }
}