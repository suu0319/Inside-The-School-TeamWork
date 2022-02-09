using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerText : MonoBehaviour
{
    PhotonView view;
    public GameObject playertextobj;
    public GameObject[] dolls;
    public Text playertext;
    public Animation anim;
    public AnimationClip[] animclips;
    //文字觸發協程
    private bool keystudenttext,keygeneraltext,keybiologytext,smoketext,fireextinguish,studenttext,irondoortext,generaltext,irondooropentext,enter2andfloor,sheettext,studentcardp1text,studentcardp2text,eightpuzzletext,pianofinishtext,steamboxtext,steamboxswitchtext,dollsbool1,dollsbool2,dollsbool3,passwordcorrect = false;
    //文字觸發前置
    public static bool GuideClose,StudentText,IronDoorText,GeneralText,IronDoorOpenText,EightPuzzleText,ToiletText,SteamBoxText = false;
    // Start is called before the first frame update
    void Start()
    {
        playertext = playertextobj.GetComponent<Text>();
        anim = playertextobj.GetComponent<Animation>();
        view = this.gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Playertext();
    }

    //玩家文字
    void Playertext()
    {
        //玩家1
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            //存檔點1未存檔
            if(GameData.GameData.Save1 == false)
            {
                if(Guide.GuideClose == true && GuideClose == false)
                {
                    GuideClose = true;
                    playertext.text = "先來看看辦公室裡有什麼吧。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.Key_Student == true && keystudenttext == false)
                {
                    keystudenttext = true;
                    playertext.text = "取得學務處鑰匙。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(StudentText == true && studenttext == false)
                {
                    studenttext = true;
                    playertext.text = "樓梯間的鐵捲門拉下了，不知道有沒有辦法打開。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.Key_Biology == true && keybiologytext == false)
                {
                    keybiologytext = true;
                    playertext.text = "取得生物教室鑰匙。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(IronDoorText == true && irondoortext == false)
                {
                    irondoortext = true;
                    playertext.text = "器材室在哪裡呢?去樓梯間看看地圖吧。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
            }
            //存檔點2未存檔
            else if(GameData.GameData.Save2 == false)
            {
                if(Trigger.Enter2ndFloor == true && enter2andfloor == false) 
                {
                    enter2andfloor = true;
                    irondoortext = true;
                    playertext.text = "鋼琴曲…?是從音樂教室傳來的?";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.SheetMusic902 == true && sheettext == false)
                {
                    sheettext = true;
                    playertext.text = "取得樂譜。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.PianoFinish == true && pianofinishtext == false)
                {
                    pianofinishtext = true;
                    playertext.text = "鋼琴似乎掉出了什麼。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.StudentCardP1 == true && studentcardp1text == false)
                {
                    studentcardp1text = true;
                    playertext.text = "...學生證?似乎可以在三樓圖書館進出用到。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.SteamBoxSwitch == true && steamboxswitchtext == false)
                {
                    steamboxswitchtext = true;
                    playertext.text = "...這是什麼情況?";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
            }
            //3樓
            else if(GameData.GameData.Save2 == true)
            {
                if(PuzzleObjController.PasswordCorrect == true && passwordcorrect == false)
                {
                    passwordcorrect = true;
                    playertext.text = "幫助阿凱蒐集完巫毒毒娃娃後，就返回書櫃那裡跟阿凱會合吧!";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
            }
        }
        //玩家2
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            //存檔點1未存檔
            if(GameData.GameData.Save1 == false)
            {
                if(Guide.GuideClose == true && GuideClose == false)
                {
                    GuideClose = true;
                    playertext.text = "先照著阿浩說的來翻翻看辦公室吧。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(Trigger.Smokebool == true && smoketext == false)
                {
                    smoketext = true;
                    playertext.text = "這裡怎麼冒煙了?快快快!有沒有可以滅火的東西?";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.FireExtinguish == true && fireextinguish == false)
                {
                    fireextinguish = true;
                    playertext.text = "取得滅火器。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.Key_General== true && keygeneraltext == false)
                {
                    keygeneraltext = true;
                    playertext.text = "取得教務處鑰匙。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(GeneralText == true && generaltext == false)
                {
                    generaltext = true;
                    playertext.text = "801旁的廁所壞了嗎?真慘。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(IronDoorOpenText == true && irondooropentext == false)
                {
                    irondooropentext = true;
                    playertext.text = "打開這個就能上二樓探索了!!";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());   
                    view.RPC("SaveData",RpcTarget.All);
                }
            }
            //存檔點2未存檔
            else if(GameData.GameData.Save2 == false)
            {
                if(Trigger.Enter2ndFloor == true && enter2andfloor == false) 
                {
                    enter2andfloor = true;
                    irondoortext = true;
                    playertext.text = "鋼琴曲?該不會是鬼魂在彈奏的吧…";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.SheetMusic802 == true && sheettext == false)
                {
                    sheettext = true;
                    playertext.text = "取得樂譜。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(EightPuzzleText == true && eightpuzzletext == false)
                {
                    eightpuzzletext = true;
                    playertext.text = "888...?似乎是某種提示，好像其他間廁所也有類似的惡作劇??";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(EightPuzzleText == true && PuzzleObjController.ZeroPuzzle == true && PuzzleObjController.FivePuzzle == true && ToiletText == false)
                {
                    ToiletText = true;
                    playertext.text = "這三個提示代表什麼呢...?";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(SteamBoxText == true && steamboxtext == false)
                {
                    steamboxtext = true;
                    playertext.text = "蒸飯室在哪裡呢?去樓梯間看看地圖吧。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.PianoFinish == true && pianofinishtext == false)
                {
                    pianofinishtext = true;
                    playertext.text = "鋼琴似乎掉出了什麼。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(PuzzleObjController.StudentCardP2 == true && studentcardp2text == false)
                {
                    studentcardp2text = true;
                    playertext.text = "...學生證?似乎可以在三樓圖書館進出用到。";
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                    view.RPC("SaveData",RpcTarget.All);
                }
            }
            //3樓
            else if(GameData.GameData.Save2 == true)
            {
                if(dolls[0] == null && dollsbool1 == false)
                {
                    dollsbool1 = true;
                    
                    if(dolls[0] == null && dolls[1] == null && dolls[2] == null)
                    {
                        playertext.text = "蒐集完巫毒娃娃了，返回書櫃那裡跟阿浩會合吧!";
                    }
                    else
                    {
                        playertext.text = "取得巫毒娃娃。";
                    }
    
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(dolls[1] == null && dollsbool2 == false)
                {
                    dollsbool2 = true;
                    
                    if(dolls[0] == null && dolls[1] == null && dolls[2] == null)
                    {
                        playertext.text = "蒐集完巫毒娃娃了，返回書櫃那裡跟阿浩會合吧!";
                    }
                    else
                    {
                        playertext.text = "取得巫毒娃娃。";
                    }
                    
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
                else if(dolls[2] == null && dollsbool3 == false)
                {
                    dollsbool3 = true;
                    
                    if(dolls[0] == null && dolls[1] == null && dolls[2] == null)
                    {
                        playertext.text = "蒐集完巫毒娃娃了，返回書櫃那裡跟阿浩會合吧!";
                    }
                    else
                    {
                        playertext.text = "取得巫毒娃娃。";
                    }
                    
                    anim.clip = animclips[1];
                    anim.Play();
                    StartCoroutine(textCD());
                }
            }
        }
    }

    //存檔
    [PunRPC]
    void SaveData()
    {
        if(GameData.GameData.Save1 == true)
        {
            GameData.GameData.Save2 = true;
        }
        else
        {
            GameData.GameData.Save1 = true;
        }
    
        GameObject.Find("GameData").SendMessage("SaveData");
    }

    //文字動畫協程
    IEnumerator textCD()
    {
        yield return new WaitForSeconds(2.5f);
        anim.clip = animclips[0];
        anim.Play();  
        yield return new WaitForSeconds(2.5f);
        playertext.text = "";
    }
}