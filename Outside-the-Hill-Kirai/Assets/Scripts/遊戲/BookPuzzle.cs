using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPuzzle : MonoBehaviour
{
    private AudioSource audio;
    public GameObject ghost1,ghost2,zombie1,zombie2,hatelight1,hatelight2,textP1,textP2;
    public GameObject[] book;
    public GameObject[] bookchange;
    public Vector3 bookchanged0;
    public Vector3[] bookposition;
    public Transform[] booktransform;
    public Transform[] booktransformchange;

    public int i = 0;

    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();

        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            ghost1.SetActive(true);
            ghost2.SetActive(true);
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            hatelight1.SetActive(false);
            hatelight2.SetActive(false);
            zombie1.SetActive(true);
            zombie2.SetActive(true);
        }
    }

    void Update()
    {
        OriginMode.God = true;
        
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            if(this.gameObject.activeInHierarchy == true)
            {
                textP1.SetActive(true);
            }
        }
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            if(this.gameObject.activeInHierarchy == true && PuzzleObjController.BookPuzzle == false)
            {
                textP2.SetActive(true);
            }
            if((booktransform[0].transform.localPosition == bookposition[0]) && (booktransform[1].transform.localPosition == bookposition[1]) && (booktransform[2].transform.localPosition == bookposition[2]) && (booktransform[3].transform.localPosition == bookposition[3]) && (booktransform[4].transform.localPosition == bookposition[4]) && PuzzleObjController.BookPuzzle == false)
            {
                PuzzleObjController.BookPuzzle = true;
                Player.PlayerActive = true;
                MouseLook.mouselookbool = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void book0()
    {
        ChangePosition(0);
    }
    public void book1()
    {
        ChangePosition(1);
    }
    public void book2()
    {
        ChangePosition(2);
    }
    public void book3()
    {
        ChangePosition(3);
    }
    public void book4()
    {
        ChangePosition(4);
    }

    void ChangePosition(int y)
    {
        if(i == 0)
        {
            i += 1;
            booktransformchange[0] = booktransform[y];
            bookchanged0 = booktransformchange[0].transform.position;
            bookchange[0] = book[y];
        }
        else if(i == 1)
        {
            i = 0;
            audio.Play();
            booktransformchange[1] = booktransform[y];
            bookchange[1] = book[y];
            bookchange[0].transform.position = booktransformchange[1].transform.position;
            bookchange[1].transform.position = bookchanged0;
        }
    }
}
