using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtPuzzle : MonoBehaviour
{
    public GameObject art,ui;
    private AudioSource audio;
    public AudioClip[] audioclips;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        OriginMode.God = true;
        
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            if(ui.activeInHierarchy == false)
            {
                ui.SetActive(true);
            }
            else if(i == 5 && PuzzleObjController.ArtPuzzle == false)
            {
                PuzzleObjController.ArtPuzzle = true;
                Player.PlayerActive = true;
                MouseLook.mouselookbool = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void RightRotate()
    {
        art.transform.Rotate(0f,0f,-90f);
        
        if(i == 1 || i == 4)
        {
            audio.clip = audioclips[1];
            audio.Play();
            
            i += 1;
        }
        else
        {
            audio.clip = audioclips[0];
            audio.Play();

            i = 0;
        }
    }
    public void LeftRotate()
    {
        art.transform.Rotate(0f,0f,90f);
        
        if(i == 0 || i == 2 || i == 3)
        {
            audio.clip = audioclips[1];
            audio.Play();

            i += 1;
        }
        else
        {
            audio.clip = audioclips[0];
            audio.Play();

            i = 0;
        }
    }
}
