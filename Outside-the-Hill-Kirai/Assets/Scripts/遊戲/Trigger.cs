using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Trigger : MonoBehaviour
{
    PhotonView view;
    public GameObject hatelightghost1,hatelightghost2,woodghost,woodghostfake,blood;
    public AudioSource audio;
    public AudioClip[] audioclips;
    public Animator animator;
    //協程CD
    private bool cd,cd2,cd3,cd4,shining = false;
    public static bool Smokebool,Enter2ndFloor = false;

    void OnTriggerEnter(Collider other)
    {
        view = other.gameObject.GetComponent<PhotonView>();
        
        if(view.IsMine)
        {
            if(other.gameObject.tag == "Player")
            {
                switch(this.gameObject.name)
                {
                    //失火濃煙
                    case "Smoke":
                    {
                        if(Smokebool == false)
                        {
                            Smokebool = true;
                        }
                    }
                    break;
                
                    //上二樓
                    case "進入二樓":
                    {
                        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                        {
                            if(Enter2ndFloor == false)
                            {
                                Enter2ndFloor = true;
                                woodghostfake.SetActive(false);
                                woodghost.SetActive(true);
                                audio.clip = audioclips[0];
                                audio.Play();
                                StartCoroutine(PianoSfx());
                            }
                        }
                        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                        {
                            if(Enter2ndFloor == false)
                            {
                                Enter2ndFloor = true;
                                hatelightghost1.SetActive(true);
                                hatelightghost2.SetActive(true);
                                audio.clip = audioclips[1];
                                audio.Play();
                                StartCoroutine(PianoSfx());
                            }
                        }
                    }
                    break;

                    //國文課本廁所JunpScare
                    case "國文課本JumpScare":
                    {
                        if(cd == false)
                        {
                            cd = true;
                            animator.SetBool("Open",true);
                            audio.Play();
                        }
                    }
                    break;

                    //P1聯絡簿血跡
                    case "P1聯絡簿血跡":
                    {
                        if(cd2 == false)
                        {
                            cd2 = true;
                            blood.SetActive(true);
                            animator.SetBool("Open",true);
                            audio.Play();
                        }
                    }
                    break;

                    //P2聯絡簿血跡
                    case "P2聯絡簿血跡":
                    {
                        if(cd3 == false)
                        {
                            cd3 = true;
                            blood.SetActive(true);
                            animator.SetBool("Open",true);
                            audio.Play();
                        }
                    }
                    break;

                    //P1音樂教室黑板血跡
                    case "P1音樂教室黑板血跡":
                    {
                        if(cd4 == false)
                        {
                            cd4 = true;
                            blood.SetActive(true);
                            audio.Play();
                        }
                    }
                    break;
                }
            }
        }
    }

    //泥娃娃鋼琴聲
    IEnumerator PianoSfx()
    {
        for(int i = 0 ; i < 15 ; i++)
        {
            yield return new WaitForSeconds(1f);
            audio.volume -= 0.075f;
        }
    }
}