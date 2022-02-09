using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class StoryBeginText : MonoBehaviour
{
    public Text text;
    public string storytext;
    public char[] storychar;
    private bool textbool = false;
    private int scenenum;
    // Start is called before the first frame update
    void Start()
    {
        scenenum = SceneManager.GetActiveScene().buildIndex;  
        
        storytext = text.text;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(textbool == false)
        {
            textbool = true;
            StartCoroutine(Texttimer());
        }
    }

    IEnumerator Texttimer()
    {
        yield return new WaitForSeconds(2f);
        
        foreach(char storychar in storytext.ToCharArray())
        {
            text.text += storychar;

            if(scenenum == 1)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}