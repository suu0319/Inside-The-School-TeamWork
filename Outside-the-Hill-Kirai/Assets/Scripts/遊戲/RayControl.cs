using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RayControl : MonoBehaviour
{
    PhotonView view;
    //預設準心 木頭人準心
    Ray ray;
    //預設準心距離
    //準心UI
    public RaycastHit hit,ghosthit;
    //ButtonE UI
    public GameObject canvas,ui,raw,ButtonE,woodghost,hatelightghost1,hatelightghost2;
    private float raylength = 1.5f;
    private bool ghostappear = false;
    public static bool woodghoststop,hatelightghostTrack;
    void Start()
    {
        //抓取ButtonE UI
        canvas = GameObject.Find("Canvas");
        ui = canvas.transform.GetChild(1).gameObject;
        raw = ui.transform.GetChild(0).gameObject;
        ButtonE = raw.transform.GetChild(1).gameObject;
        view = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        RayInteraction();
    }

    //射線互動判斷
    void RayInteraction()
    {
        //預設準心
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            
        if(Physics.Raycast(ray,out hit, raylength,1 << 11))
        {
            Tagtext();        
            Debug.DrawRay(Input.mousePosition,hit.point,Color.yellow);
            //Debug.Log(hit.transform.name);
        }
        else
        {
            ButtonE.SetActive(false);
        }

        //玩家1
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1 && Time.timeScale == 1f)
        {
            //木頭人出現
            if(Trigger.Enter2ndFloor == true && ghostappear == false)
            {
                woodghost = GameObject.Find("木頭人");
                
                if(woodghost != null)
                {
                    ghostappear = true;
                }
            }
            if(ghostappear == true)
            {
                //木頭人判定
                if (IsInView(woodghost.transform.position) && HP.currentHP != 0)   
                {
                    woodghoststop = true;
                }
                else
                {
                    woodghoststop = false;
                }
            }
        }
        //玩家2
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2 && Time.timeScale == 1f)
        {
            //厭光怪出現
            if(Trigger.Enter2ndFloor == true && ghostappear == false)
            {
                hatelightghost1 = GameObject.Find("厭光怪1");
                hatelightghost2 = GameObject.Find("厭光怪2");
                
                if(hatelightghost1 != null || hatelightghost2 != null)
                {
                    ghostappear = true;
                }
            }
            if(ghostappear == true)
            {
                //厭光怪判定
                if (IsInView(hatelightghost1.transform.position) || IsInView(hatelightghost2.transform.position))   
                {
                    hatelightghostTrack = true;
                }
                else
                {
                    hatelightghostTrack = false;
                }
            }
        }
    }

    //物件互動判斷
    void Tagtext()
    {
        //物件觸發方法
        switch(hit.collider.gameObject.tag)
        {
            //取得物品
            case "InteractionGet": case "FireExtinguish":
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("GetObject",gameObject,SendMessageOptions.DontRequireReceiver);
            }
            break;

            //滅火
            case "Smoke":
            {
                if(PuzzleObjController.FireExtinguish == true)
                {
                    ButtonE.SetActive(true);
                    hit.transform.SendMessage("GetObject",gameObject,SendMessageOptions.DontRequireReceiver);
                }
            }
            break;

            //開關門
            case "Door": case "ToiletDoor": case "StudentDoor": case "GeneralDoor": case "BiologyDoor": case "LibraryDoor":
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("DoorOpenClose",gameObject,SendMessageOptions.DontRequireReceiver);
            }
            break;

            //置物櫃開關門
            case "BiologyPuzzleDoor":
            {
                if(PuzzleObjController.Key_BiologyPuzzle == true)
                {
                    ButtonE.SetActive(true);
                    hit.transform.SendMessage("DoorOpenClose",gameObject,SendMessageOptions.DontRequireReceiver);
                }
            }
            break;

            //鐵捲門開關
            case "IronDoorSwitch":
            {
                if(PuzzleObjController.IronDoorPuzzle == true && PuzzleObjController.IronDoorSwitch == false && outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                {
                    ButtonE.SetActive(true);
                    hit.transform.SendMessage("Switch",gameObject,SendMessageOptions.DontRequireReceiver);
                }
            }
            break;

            //蒸飯箱開關
            case "SteamBoxSwitch":
            {
                if(PuzzleObjController.SteamBoxPuzzle == true && PuzzleObjController.SteamBoxSwitch == false && outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                {
                    ButtonE.SetActive(true);
                    hit.transform.SendMessage("Switch",gameObject,SendMessageOptions.DontRequireReceiver);
                }
            }
            break;

            //2DUI
            case "Interaction2DUI":
            {
                if(hit.collider.gameObject.name == "Piano")
                {
                    if(PuzzleObjController.PianoFinish == true)
                    {
                        ButtonE.SetActive(false);
                    }
                    else if((PuzzleObjController.SheetMusic902 == true || PuzzleObjController.SheetMusic802 == true) && PuzzleObjController.PianoFinish == false)
                    {
                        ButtonE.SetActive(true);
                        hit.transform.SendMessage("Uiappear",gameObject,SendMessageOptions.DontRequireReceiver);
                    }
                }
                else if(hit.collider.gameObject.name == "P1書籍謎題" || hit.collider.gameObject.name == "P2書籍謎題")
                {
                    if(PuzzleObjController.BookPuzzle == true)
                    {
                        ButtonE.SetActive(false);
                    }
                    else
                    {
                        ButtonE.SetActive(true);
                        hit.transform.SendMessage("Uiappear",gameObject,SendMessageOptions.DontRequireReceiver);
                    }
                }
                else if(hit.collider.gameObject.name == "裝置藝術")
                {
                    if(PuzzleObjController.ArtPuzzle == true)
                    {
                        ButtonE.SetActive(false);
                    }
                    else
                    {
                        ButtonE.SetActive(true);
                        hit.transform.SendMessage("Uiappear",gameObject,SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    ButtonE.SetActive(true);
                    hit.transform.SendMessage("Uiappear",gameObject,SendMessageOptions.DontRequireReceiver);
                }
            }   
            break;

            //文字觸發
            case "InteractionText":
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("Textappear",gameObject,SendMessageOptions.DontRequireReceiver);
            }   
            break;

            default:
            {
                ButtonE.SetActive(false);
            }
            break;
        }
    }

    //判斷物體是否在相機前面
    bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);     

        if(dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}