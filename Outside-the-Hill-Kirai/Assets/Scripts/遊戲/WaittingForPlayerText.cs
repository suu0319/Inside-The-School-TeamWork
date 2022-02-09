using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaittingForPlayerText : MonoBehaviour
{
    public Text waittingttext;
    public bool textanimbool = false;
    
    // Start is called before the first frame update
    void Start()
    {
        waittingttext = this.gameObject.GetComponentInChildren<Text>();
        waittingttext.text = "等待玩家中";
    }

    // Update is called once per frame
    void Update()
    {
        if(textanimbool == false)
        {
            textanimbool = true;
            StartCoroutine(WaittingAnimation());
        }
    }

    //檢查點動畫
    IEnumerator WaittingAnimation()
    {
        for(int i = 0 ; i < 3 ; i++)
        {
            waittingttext.text += ".";
            yield return new WaitForSeconds(1f);
            
            if(waittingttext.text == "等待玩家中...")
            {
                waittingttext.text = "等待玩家中";
                textanimbool = false;
            }
        }
    }
}