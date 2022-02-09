using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] audioclips;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
