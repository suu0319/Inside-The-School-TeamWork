using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThreePuzzleController : MonoBehaviour
{
    PhotonView view;
    public GameObject[] puzzle;
    private bool cd,cd2 = false;

    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            if(puzzle[0].activeInHierarchy == true && cd == false)
            {
                cd = true;
                puzzle[1].SetActive(true);
                puzzle[2].SetActive(true);
                puzzle[3].SetActive(true);
            }
        }
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            if(PuzzleObjController.BookPuzzle == true && cd2 == false)
            {
                cd2 = true;
                puzzle[4].SetActive(true);
                puzzle[5].SetActive(true);
                puzzle[6].SetActive(true);
            }
            if(PuzzleObjController.DollsPuzzle == true)
            {
                view.RPC("End",RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void End()
    {
        puzzle[7].SetActive(true);
    }
}
