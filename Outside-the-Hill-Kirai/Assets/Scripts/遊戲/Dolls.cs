using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolls : MonoBehaviour
{
    public GameObject[] dolls;
    private bool cd = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleObjController.BookPuzzle == true && PuzzleObjController.ArtPuzzle == true && PuzzleObjController.PasswordCorrect == true && cd == false)
        {
            cd = true;
            
            for(int i = 0 ; i < 3 ; i++)
            {
                dolls[i].SetActive(true);
            }
        }

        if(dolls[0] == null && dolls[1] == null && dolls[2] == null && PuzzleObjController.DollsPuzzle == false)
        {
            PuzzleObjController.DollsPuzzle = true;
        }
    }
}
