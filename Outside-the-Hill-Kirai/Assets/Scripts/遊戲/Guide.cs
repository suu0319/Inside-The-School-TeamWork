using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public GameObject RawUI,guideui;
    //協程CD
    private bool cd = false;
    public static bool GuideClose = false;
    // Start is called before the first frame update
    void Start()
    {
        guideui = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GuideUI();
    }

    //判斷新手教學UI
    void GuideUI()
    {
        if(guideui.activeSelf == true)
        {
            Player.PlayerActive = false;
            MouseLook.mouselookbool = false;

            if(Input.anyKeyDown)
            {
                StartCoroutine(timeCD());
                guideui.SetActive(false);
                RawUI.SetActive(true);
                GuideClose = true;
            }
        }
        else if(guideui.activeSelf == false && cd == false)
        {
            cd = true;
            RawUI.SetActive(true);
        }
    }
    
    //玩家狀態激活(關閉新手教學UI)
    IEnumerator timeCD()
    {
        yield return null;
        Player.PlayerActive = true;
        MouseLook.mouselookbool = true;
    }
}