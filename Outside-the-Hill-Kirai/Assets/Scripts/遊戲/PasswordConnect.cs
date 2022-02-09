using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PasswordConnect : MonoBehaviour
{
    PhotonView view;
    private bool cd = false;

    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleObjController.PasswordCorrect == true && cd == false)
        {
            cd = true;
            view.RPC("Correct",RpcTarget.All);
        }
    }

    [PunRPC]
    void Correct()
    {
        PuzzleObjController.PasswordCorrect = true;
    }
}
