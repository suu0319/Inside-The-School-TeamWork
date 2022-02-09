using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ButtonEInteractionDoorSwitch : MonoBehaviour
{
    PhotonView view;
    public GameObject irondoors,smoke,gallerysmoke;
    //取得Animation
    public Animator animator;
    public Animation anim;
    public AnimationClip[] animclips;
    public AudioSource audiosource;
    public AudioClip[] audioclips;
    //遊戲文字
    public Text text;
    //協程CD
    private bool cd,cd2 = false;
    //取得開關門bool
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();

        switch(this.gameObject.tag)
        {
            case "Door": case "StudentDoor": case "GeneralDoor": case "BiologyDoor": case "IronDoorSwitch": case "LibraryDoor":
            {
                animator = this.gameObject.GetComponentInParent<Animator>();
                audiosource = this.gameObject.GetComponentInParent<AudioSource>();
            }
            break;
            
            case "ToiletDoor": case "BiologyPuzzleDoor": case "SteamBoxSwitch":
            {
                animator = this.gameObject.GetComponent<Animator>();
                audiosource = this.gameObject.GetComponent<AudioSource>();
            }
            break;
        }
    }
   
    void DoorOpenClose()
    {
        //取得動畫Bool-Open
        open = animator.GetBool("Open");
        
        switch(this.gameObject.tag)
        {
            //學務處門判斷
            case "StudentDoor":
            {
                if(Input.GetKeyDown(KeyCode.E) && cd == false && PuzzleObjController.Key_Student == true)
                {
                    //開門
                    if(open == false)
                    {
                        DoorOpenSfx();

                        //尋找生物教室保管表文字
                        if(PlayerText.StudentText == false)
                        {
                            PlayerText.StudentText = true;
                        }
                    }
                    //關門
                    else if(open == true)
                    {
                        DoorCloseSfx();
                    }
                }
                //門鎖住
                else if(Input.GetKeyDown(KeyCode.E) && PuzzleObjController.Key_Student == false)
                {
                    DoorLocked();
                }
            }
            break;

            //教務處門判斷
            case "GeneralDoor":
            {
                if(Input.GetKeyDown(KeyCode.E) && cd == false && PuzzleObjController.Key_General == true)
                {
                    //開門
                    if(open == false)
                    {
                        DoorOpenSfx();
                    }
                    //關門
                    else if(open == true)
                    {
                        DoorCloseSfx();
                    }
                }
                //門鎖住
                else if(Input.GetKeyDown(KeyCode.E) && PuzzleObjController.Key_General == false)
                {
                    DoorLocked();
                }
            }
            break;

            //生物教室門判斷
            case "BiologyDoor":
            {
                if(Input.GetKeyDown(KeyCode.E) && cd == false && PuzzleObjController.Key_Biology == true)
                {
                    //開門
                    if(open == false)
                    {
                        DoorOpenSfx();
                    }
                    //關門
                    else if(open == true)
                    {
                        DoorCloseSfx();
                    }
                }
                //門鎖住
                else if(Input.GetKeyDown(KeyCode.E) && PuzzleObjController.Key_Biology == false)
                {
                    DoorLocked();
                }
            }
            break;
            //圖書館門判斷
            case "LibraryDoor":
            {
                if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                {
                    if(Input.GetKeyDown(KeyCode.E) && cd == false && PuzzleObjController.StudentCardP1 == true)
                    {
                        //開門
                        if(open == false)
                        {
                            DoorOpenSfx();
                        }
                        //關門
                        else if(open == true)
                        {
                            DoorCloseSfx();
                        }
                    }
                    //門鎖住
                    else if(Input.GetKeyDown(KeyCode.E) && PuzzleObjController.StudentCardP1 == false)
                    {
                        DoorLocked();
                    }
                }
                else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                {
                    if(Input.GetKeyDown(KeyCode.E) && cd == false && PuzzleObjController.StudentCardP2 == true)
                    {
                        //開門
                        if(open == false)
                        {
                            DoorOpenSfx();
                        }
                        //關門
                        else if(open == true)
                        {
                            DoorCloseSfx();
                        }
                    }
                    //門鎖住
                    else if(Input.GetKeyDown(KeyCode.E) && PuzzleObjController.StudentCardP2 == false)
                    {
                        DoorLocked();
                    }
                }
            }
            break;

            //一般門判斷
            default:
            {
                if(Input.GetKeyDown(KeyCode.E) && cd == false)
                {
                    //開門
                    if(open == false)
                    {
                        DoorOpenSfx();
                    }
                    //關門
                    else if(open == true)
                    {
                        DoorCloseSfx();
                    }
                }
            }
            break;
        }
    }
    
    void Switch()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(this.gameObject.tag)
            {
                //鐵捲門開關
                case "IronDoorSwitch":
                {
                    if(PuzzleObjController.IronDoorSwitch == false && open == false && cd == false)
                    {
                        //鐵捲門開關開啟
                        PuzzleObjController.IronDoorSwitch = true;
                
                        //同步玩家鐵捲門打開
                        view.RPC("IrondoorOpen",RpcTarget.All);

                        //開啟鐵捲門文字
                        if(PlayerText.IronDoorOpenText == false)
                        {
                            PlayerText.IronDoorOpenText = true;
                        }
                    }
                }
                break;

                //蒸飯室引爆
                case "SteamBoxSwitch":
                {
                    if(PuzzleObjController.SteamBoxSwitch == false)
                    {
                        //蒸飯箱引爆成功
                        PuzzleObjController.SteamBoxSwitch = true;
                        smoke.SetActive(true);
                        StartCoroutine("smokeCD");
                        audiosource.Play();

                        //長廊黑影消失
                        view.RPC("GallerySmoke",RpcTarget.All);
                    }
                }
                break;
            }
        }   
    }

    //開門音效
    void DoorOpenSfx()
    {
        view.RPC("DoorOpen",RpcTarget.All);
        audiosource.clip = audioclips[1];
        audiosource.Play();
    }
    
    //關門音效
    void DoorCloseSfx()
    {
        view.RPC("DoorClose",RpcTarget.All);
        audiosource.clip = audioclips[0];
        audiosource.Play();
    }

    //門鎖住文字
    void DoorLocked()
    {
        if(cd2 == false)
        {
            cd2 = true;
            text.text = "門鎖住了。";
            anim.clip = animclips[1];
            anim.Play();
            StartCoroutine(textCD());
        }
    }

    //開門動畫
    [PunRPC]
    void DoorOpen()
    {
        cd = true;
        animator.SetBool("Open",true);
        StartCoroutine(timeCD());
    }

    //關門動畫
    [PunRPC]
    void DoorClose()
    {
        cd = true;
        animator.SetBool("Open",false);
        StartCoroutine(timeCD());
    }

    //鐵捲門開門
    [PunRPC]
    void IrondoorOpen()
    {
        irondoors.SetActive(false);
        DoorOpen();
        audiosource.Play();
    }

    //長廊黑影消失
    [PunRPC]
    void GallerySmoke()
    {
        gallerysmoke.SetActive(false);
    }

    //動畫CD
    IEnumerator timeCD()
    {
        yield return new WaitForSeconds(1f);
        cd = false;
    }

    //文自動畫CD
    IEnumerator textCD()
    {
        yield return new WaitForSeconds(2.5f);
        cd2 = false;
        anim.clip = animclips[0];
        anim.Play();  
    }

    //蒸飯箱失火CD
    IEnumerator smokeCD()
    {
        yield return new WaitForSeconds(20f);
        smoke.SetActive(false);
    }
}