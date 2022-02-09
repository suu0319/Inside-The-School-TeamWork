using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using outsidetheschoolPUN;

public class OriginMode : MonoBehaviour
{
    public int scenenum;
    private bool loaddata = false;
    public bool Save1,Save2 = false;
    public static bool savedata,woodghoststop,hatelightghost,God = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scenenum = SceneManager.GetActiveScene().buildIndex; 

        //取得存檔
        Save1 = GameData.GameData.Save1 = GameData.GameData.inttobool(PlayerPrefs.GetInt("Save1"));
        //正長時間速度
        Time.timeScale = 1f;     
        //隱藏滑鼠        
        Cursor.visible = false;
        //把滑鼠鎖定到螢幕中間
        Cursor.lockState = CursorLockMode.Locked;
        //原始血量 
        HP.currentHP = 100;      
        //原始體力   
        AP.currentAP = 100;          
        //角色是否活躍(動畫 音效等)  
        Player.PlayerActive = true;  
        //視角是否可以轉動
        MouseLook.mouselookbool = true;  
        //選取第一個Button
        DeviceSelect.keyboardused = false; 
        //玩家ID
        ServerConnect.PlayerP1 = false;
        ServerConnect.PlayerP2 = false;
        //木頭人判定
        woodghoststop = false;
        //厭光怪判定
        hatelightghost = false;
        //手電筒開關判定
        Player.spotlightopen = false;
        //UI出現判定
        ButtonEInteractionObj.puzzleuiappear = false;
        //無敵狀態
        God = false;

        //尚未讀檔
        if(scenenum == 0)
        {
            savedata = false;
        }
        else if(scenenum == 2)
        {
            GameObject.Find("GameData").SendMessage("OriginData");
        }
    }
}