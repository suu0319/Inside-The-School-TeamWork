using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPuzzleControl : MonoBehaviour
{
    public GameObject rawui,cam,playercam1,playercam2,uiclose,bookpuzzleP1,bookpuzzleP2,puzzle,textP1,textP2;
    private bool cd = false;

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

        if(puzzle.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
            OriginMode.God = true;
            Player.PlayerActive = false;
            MouseLook.mouselookbool = false;
            ButtonEInteractionObj.puzzleuiappear = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            uiclose.SetActive(true);
            rawui.SetActive(false);

            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                playercam1.SetActive(false);
            }
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                playercam2.SetActive(false);
            }
        }

        if((Input.GetKeyDown("escape") && puzzle.activeInHierarchy == true) || (this.gameObject.name == "謎題攝影機控制" && PuzzleObjController.BookPuzzle == true && cd == false) || (this.gameObject.name == "謎題攝影機控制2" && PuzzleObjController.ArtPuzzle == true && cd == false))
        {
            OriginMode.God = false;
            Time.timeScale = 1f;
            ButtonEInteractionObj.puzzleuiappear = false;      
            uiclose.SetActive(false);
            rawui.SetActive(true);

            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                if(this.gameObject.name == "謎題攝影機控制")
                {
                    textP1.SetActive(false);
                    bookpuzzleP1.SetActive(true);
                }
                else if(this.gameObject.name == "謎題攝影機控制3")
                {
                    GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = true;
                    textP1.SetActive(false);
                }

                playercam1.SetActive(true);
            }
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {   
                if(this.gameObject.name == "謎題攝影機控制")
                {
                    textP2.SetActive(false);

                    if(PuzzleObjController.BookPuzzle == true)
                    {
                        cd = true;
                        bookpuzzleP2.SetActive(true);
                    }
                }
                else if(this.gameObject.name == "謎題攝影機控制2")
                {
                    textP2.SetActive(false);

                    if(PuzzleObjController.ArtPuzzle == true)
                    {
                        cd = true;
                        bookpuzzleP2.SetActive(true);
                    }
                }
                
                playercam2.SetActive(true);
            }

            StartCoroutine(PlayerActive());
            puzzle.SetActive(false);
        }
    }

    IEnumerator PlayerActive()
    {
        yield return new WaitForEndOfFrame();
        Player.PlayerActive = true;
        MouseLook.mouselookbool = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
