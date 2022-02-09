using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DeviceSelect : MonoBehaviour
{
    //按鍵
    public GameObject firstbutton,settingbutton,deadfirstbutton,replayfirstbutton,setting,teachui,rawui,replayui;
    private int scenenum;
    //游標協程CD
    private bool menudeadcursor = false;
    //鍵盤使用
    public static bool keyboardused = false;

    void Start() 
    {
        scenenum = SceneManager.GetActiveScene().buildIndex;
        
        //主選單
        if(scenenum == 0)
        {
            HideCursor();
            StartCoroutine(CursorCD());
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardCursor();
    }

    //游標鍵盤判定(Update)
    void KeyboardCursor()
    {
        if(HP.currentHP == 0 && menudeadcursor == false)
        {
            HideCursor();
            StartCoroutine(CursorCD());
        }
        
        if(scenenum != 0 || menudeadcursor == true)
        {
            if(HP.currentHP == 0)
            {
                StartCoroutine(CursorCD());
            }
            else if(ButtonEInteractionObj.puzzleuiappear == false)
            {
                DeviceManager();
            }
        }

        if(scenenum == 2)
        {
            if(teachui.activeInHierarchy == true)
            {
                HideCursor();
                StartCoroutine(CursorCD());
            }
        }
    }
    
    //游標鍵盤判定
    void DeviceManager()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetMouseButtonDown(0)) 
        {
            keyboardused = false;
            EventSystem.current.SetSelectedGameObject(null);

            if(scenenum == 0)
            {
                AppearCursor();
            }
            else if(Player.PlayerActive == false && (teachui.activeInHierarchy == false || rawui.activeInHierarchy == false))
            {
                AppearCursor();
            }
        }
        else if(Input.anyKeyDown && keyboardused == false) 
        {
            keyboardused = true;
            
            if(setting.activeInHierarchy == false)
            {
                if(HP.currentHP == 0)
                {
                    if(replayui.activeInHierarchy == true)
                    {
                        EventSystem.current.SetSelectedGameObject(replayfirstbutton);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(deadfirstbutton);
                    }
                }
                else
                {   
                    EventSystem.current.SetSelectedGameObject(firstbutton);
                }
                
                HideCursor();
            }
            else if(setting.activeInHierarchy == true)
            {
                EventSystem.current.SetSelectedGameObject(settingbutton);
                HideCursor();
            }
        }
    }

    //隱藏游標
    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //出現游標
    void AppearCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //游標出現協程
    IEnumerator CursorCD()
    {
        yield return new WaitForSeconds(12f);
        menudeadcursor = true;
        
        //玩家死亡
        if(HP.currentHP == 0)
        {
            DeviceManager();
        }
    }
}