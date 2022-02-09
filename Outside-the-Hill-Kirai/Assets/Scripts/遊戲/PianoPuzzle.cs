using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzle : MonoBehaviour
{
    public int i = 0;

    private AudioSource audio;
    public AudioClip[] audioclips;
    public Animator animator;
    public GameObject studentcard,ui2d,uiclose;
    public GameObject[] sheets;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1 && PuzzleObjController.PianoFinish == false)
        {
            if(i == 6)
            {
                Time.timeScale = 1f;
                ButtonEInteractionObj.puzzleuiappear = false;      
                Player.PlayerActive = true;
                MouseLook.mouselookbool = true;
                ui2d.SetActive(false);
                uiclose.SetActive(false);

                PuzzleObjController.PianoFinish = true;
                animator.SetBool("Open",true);
                studentcard.SetActive(true);
                audio.clip = audioclips[7];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                i = 0;

                audio.clip = audioclips[0];
                audio.Play();

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                 }
            }  
            else if(Input.GetKeyDown(KeyCode.W))
            {
                i = 0;

                audio.clip = audioclips[1];
                audio.Play();

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                }
            } 
            else if(Input.GetKeyDown(KeyCode.E))
            {
                if(i == 3)
                {
                    i += 1;
                    sheets[3].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }

                audio.clip = audioclips[2];
                audio.Play();
            }  
            else if(Input.GetKeyDown(KeyCode.R))
            {
                i = 0;

                audio.clip = audioclips[3];
                audio.Play();

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                }
            } 
            else if(Input.GetKeyDown(KeyCode.T))
            {
                i = 0;

                audio.clip = audioclips[4];
                audio.Play();

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                }
            } 
            else if(Input.GetKeyDown(KeyCode.Y))
            {
                if(i == 0)
                {
                    i += 1;
                    sheets[0].SetActive(true);
                }
                else if(i == 5)
                {
                    i += 1;
                    sheets[5].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[5];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.U))
            {
                if(i == 1)
                {
                    i += 1;
                    sheets[1].SetActive(true);
                }
                else if(i == 2)
                {
                    i += 1;
                    sheets[2].SetActive(true);
                }
                else if(i == 4)
                {
                    i += 1;
                    sheets[4].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[6];
                audio.Play();
            }
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2 && PuzzleObjController.PianoFinish == false)
        {
            if(i == 6)
            {
                Time.timeScale = 1f;
                ButtonEInteractionObj.puzzleuiappear = false;      
                Player.PlayerActive = true;
                MouseLook.mouselookbool = true;
                ui2d.SetActive(false);
                uiclose.SetActive(false);

                PuzzleObjController.PianoFinish = true;
                animator.SetBool("Open",true);
                studentcard.SetActive(true);
                audio.clip = audioclips[7];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                if(i == 1)
                {
                    i += 1;
                    sheets[1].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[0];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                if(i == 5)
                {
                    i += 1;
                    sheets[5].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[1];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                if(i == 3)
                {
                    i += 1;
                    sheets[3].SetActive(true);
                }
                else if(i == 4)
                {
                    i += 1;
                    sheets[4].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[2];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                if(i == 0)
                {
                    i += 1;
                    sheets[0].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[3];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.T))
            {
                if(i == 2)
                {
                    i += 1;
                    sheets[2].SetActive(true);
                }
                else
                {
                    i = 0;

                    for(int y = 0 ; y < 5 ; y++)
                    {
                        if(sheets[y].activeInHierarchy == true)
                        {
                            sheets[y].SetActive(false);
                        }
                    }
                }
                
                audio.clip = audioclips[4];
                audio.Play();
            }
            else if(Input.GetKeyDown(KeyCode.Y))
            {
                i = 0;

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                }

                audio.clip = audioclips[5];
                audio.Play();
            } 
            else if(Input.GetKeyDown(KeyCode.U))
            {
                i = 0;

                for(int y = 0 ; y < 5 ; y++)
                {
                    if(sheets[y].activeInHierarchy == true)
                    {
                        sheets[y].SetActive(false);
                    }
                }

                audio.clip = audioclips[6];
                audio.Play();
            } 
        }
    }
}
