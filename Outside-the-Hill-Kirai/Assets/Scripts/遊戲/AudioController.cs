using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audios;

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            for(int i = 0 ; i < audios.Length ; i++)
            {
                audios[i].enabled = false;
            }
        }
    }
}
