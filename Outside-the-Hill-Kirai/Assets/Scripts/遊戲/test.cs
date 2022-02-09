using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cursor.visible);
    }
}