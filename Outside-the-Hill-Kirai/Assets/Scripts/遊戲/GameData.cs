using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using outsidetheschoolPUN;

namespace GameData
{
    public class GameData : MonoBehaviour
    {
        [SerializeField]
        PlayerData data;
        //存檔文字
        public GameObject SaveTextObj;
        //場景物件
        public GameObject[] Player1Obj;
        public GameObject[] Player2Obj;
        public GameObject[] Player12Obj;
        public Text SaveText;
        public Animation TextAnimation;
        public AnimationClip[] TextAnimationClips;
        //當前場景編號
        private int scenenum;
        //協程CD
        private bool SaveAnimationBool = false;
        //玩家存檔點觸發
        public static bool Save1,Save2 = false;

        //Singleton
        private static GameData _singleton = null;
        
        public static GameData singleton
        {
            get
            {
                if(_singleton == null)
                {
                    _singleton = new GameData();
                }
                
                return _singleton;
            }
        }

        void Awake()
        {
            if(_singleton != null)
            {
                return;
            }
            
            _singleton = this;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            scenenum = SceneManager.GetActiveScene().buildIndex;
            SaveTextObj = GameObject.Find("存檔點文字");
            SaveText = SaveTextObj.GetComponent<Text>();
            TextAnimation = SaveTextObj.GetComponent<Animation>();
            
            if(PuzzleObjController.End == true)
            {
                SceneManager.LoadScene(0);
            }
        }

        //初始資料
        void OriginData()
        {
            //尚未讀檔(開始新遊戲)
            if(OriginMode.savedata == false)
            {
                //尚未存檔
                Save1 = false;
                Save2 = false;
            
                //玩家共同
                Guide.GuideClose = false;
                PlayerText.GuideClose = false;
                Trigger.Enter2ndFloor = false;
                
                //書籍謎題
                PuzzleObjController.BookPuzzle = false;
                //電腦密碼
                PuzzleObjController.PasswordCorrect = false;
                //藝術裝置謎題
                PuzzleObjController.ArtPuzzle = false;
                //巫毒娃娃謎題
                PuzzleObjController.DollsPuzzle = false;
                //破關準備
                Ending.endready = false;
                //破關
                PuzzleObjController.End = false;

                if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                {
                    //物件
                    PuzzleObjController.Key_Student = false;
                    PuzzleObjController.Key_BiologyPuzzle = false;
                    PuzzleObjController.Key_Biology = false;
                    PuzzleObjController.IronDoorPuzzle = false;
                    PuzzleObjController.SheetMusic902 = false;
                    PuzzleObjController.PianoFinish = false;
                    PuzzleObjController.SteamBoxSwitch = false;
                    PuzzleObjController.StudentCardP1 = false;

                    //文字
                    PlayerText.StudentText = false;
                    PlayerText.IronDoorText = false;
                }
                else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                {     
                    //物件     
                    PuzzleObjController.FireExtinguish = false;
                    PuzzleObjController.SmokeBool = false;       
                    PuzzleObjController.Key_General = false;
                    PuzzleObjController.IronDoorSwitch = false;
                    PuzzleObjController.EightPuzzle = false;
                    PuzzleObjController.ZeroPuzzle = false;
                    PuzzleObjController.FivePuzzle = false;
                    PuzzleObjController.SteamBoxPuzzle = false;
                    PuzzleObjController.SheetMusic802 = false;
                    PuzzleObjController.PianoFinish = false;
                    PuzzleObjController.StudentCardP2 = false;
                
                    //文字
                    Trigger.Smokebool = false;   
                    PlayerText.GeneralText = false;
                    PlayerText.IronDoorOpenText = false;
                    PlayerText.EightPuzzleText = false;
                    PlayerText.ToiletText = false;
                    PlayerText.SteamBoxText = false;
                }
            }
            //讀檔(繼續遊戲)
            else
            {
                LoadData();
            }
        }

        //場景資料
        void SceneData()
        {
            if(OriginMode.savedata == true && (Save1 == true || Save2 == true))
            {
                //玩家物件
                Player12Obj[0].SetActive(false);
                Player12Obj[1].SetActive(false);

                //書籍謎題
                PuzzleObjController.BookPuzzle = false;
                //電腦密碼
                PuzzleObjController.PasswordCorrect = false;
                //藝術裝置謎題
                PuzzleObjController.ArtPuzzle = false;
                //巫毒娃娃謎題
                PuzzleObjController.DollsPuzzle = false;
                //破關準備
                Ending.endready = false;
                //破關
                PuzzleObjController.End = false;
                
                if(Save2 == true)
                {
                    Player12Obj[2].SetActive(false);
                }
                
                //玩家1
                if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
                {
                    if(PuzzleObjController.Key_Student == true && Player1Obj[0])
                    {
                        Player1Obj[0].SetActive(false);
                    }
                    
                    if(PuzzleObjController.Key_Biology == true && Player1Obj[1])
                    {
                        Player1Obj[1].SetActive(false);
                    }
                    if(PuzzleObjController.SheetMusic902 == true && Player1Obj[2])
                    {
                        Player1Obj[2].SetActive(false);
                    }
                    if(PuzzleObjController.StudentCardP1 == true && Player1Obj[3])
                    {
                        Player1Obj[3].SetActive(false);
                    }
                }
                //玩家2
                else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
                {
                    if(PuzzleObjController.SmokeBool == true && Player2Obj[0])
                    {
                        Player2Obj[0].SetActive(false);
                    }
                    
                    if(PuzzleObjController.Key_General == true && Player2Obj[1])
                    {
                        Player2Obj[1].SetActive(false);
                    }
                    if(PuzzleObjController.SheetMusic802 == true && Player2Obj[2])
                    {
                        Player2Obj[2].SetActive(false);
                    }
                    if(PuzzleObjController.StudentCardP2 == true && Player2Obj[3])
                    {
                        Player2Obj[3].SetActive(false);
                    }
                    if(Save2 == true)
                    {
                        Player2Obj[4].SetActive(true);
                        Player2Obj[5].SetActive(true);
                    }
                }
            }
        }

        //存檔
        void SaveData()
        {
            Debug.Log("存檔完成");

            //存檔文字動畫
            if(SaveAnimationBool == false)
            {
                SaveAnimationBool = true;
                TextAnimation.clip = TextAnimationClips[1];
                TextAnimation.Play();
                StartCoroutine(SaveTextAnimation());
            }
            
            //玩家共同
            PlayerPrefs.SetInt("GuideClose",booltoint(PlayerText.GuideClose));
            PlayerPrefs.SetInt("Enter2ndFloor",booltoint(Trigger.Enter2ndFloor));

            //玩家1
            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                //座標
                PlayerPrefs.SetFloat("Player1x",GameObject.Find("Player1").transform.position.x);
                PlayerPrefs.SetFloat("Player1y",GameObject.Find("Player1").transform.position.y);
                PlayerPrefs.SetFloat("Player1z",GameObject.Find("Player1").transform.position.z);
                
                //物件
                PlayerPrefs.SetInt("Key_Student",booltoint(PuzzleObjController.Key_Student));
                PlayerPrefs.SetInt("Key_BiologyPuzzle",booltoint(PuzzleObjController.Key_BiologyPuzzle));
                PlayerPrefs.SetInt("Key_Biology",booltoint(PuzzleObjController.Key_Biology));
                PlayerPrefs.SetInt("IronDoorPuzzle",booltoint(PuzzleObjController.IronDoorPuzzle));
                PlayerPrefs.SetInt("SheetMusic902",booltoint(PuzzleObjController.SheetMusic902));
                PlayerPrefs.SetInt("PianoFinish",booltoint(PuzzleObjController.PianoFinish));
                PlayerPrefs.SetInt("SteamBoxSwitch",booltoint(PuzzleObjController.SteamBoxSwitch));
                PlayerPrefs.SetInt("StudentCardP1",booltoint(PuzzleObjController.StudentCardP1));

                //文字
                PlayerPrefs.SetInt("StudentText",booltoint(PlayerText.StudentText));
                PlayerPrefs.SetInt("IronDoorText",booltoint(PlayerText.IronDoorText));
            }
            //玩家2
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                //座標
                PlayerPrefs.SetFloat("Player2x",GameObject.Find("Player2").transform.position.x);
                PlayerPrefs.SetFloat("Player2y",GameObject.Find("Player2").transform.position.y);
                PlayerPrefs.SetFloat("Player2z",GameObject.Find("Player2").transform.position.z);
                
                //物件
                PlayerPrefs.SetInt("FireExtinguish",booltoint(PuzzleObjController.FireExtinguish));
                PlayerPrefs.SetInt("SmokeBool",booltoint(PuzzleObjController.SmokeBool));
                PlayerPrefs.SetInt("Key_General",booltoint(PuzzleObjController.Key_General));
                PlayerPrefs.SetInt("IronDoorSwitch",booltoint(PuzzleObjController.IronDoorSwitch));
                PlayerPrefs.SetInt("EightPuzzle",booltoint(PuzzleObjController.EightPuzzle));
                PlayerPrefs.SetInt("ZeroPuzzle",booltoint(PuzzleObjController.ZeroPuzzle));
                PlayerPrefs.SetInt("FivePuzzle",booltoint(PuzzleObjController.FivePuzzle));
                PlayerPrefs.SetInt("SteamBoxPuzzle",booltoint(PuzzleObjController.SteamBoxPuzzle));
                PlayerPrefs.SetInt("SheetMusic802",booltoint(PuzzleObjController.SheetMusic802));
                PlayerPrefs.SetInt("PianoFinish",booltoint(PuzzleObjController.PianoFinish));
                PlayerPrefs.SetInt("StudentCardP2",booltoint(PuzzleObjController.StudentCardP2));

                //文字
                PlayerPrefs.SetInt("SmokeboolTrigger",booltoint(Trigger.Smokebool));
                PlayerPrefs.SetInt("GeneralText",booltoint(PlayerText.GeneralText));
                PlayerPrefs.SetInt("IronDoorOpenText",booltoint(PlayerText.IronDoorOpenText));
                PlayerPrefs.SetInt("EightPuzzleText",booltoint(PlayerText.EightPuzzleText));
                PlayerPrefs.SetInt("ToiletText",booltoint(PlayerText.ToiletText));
                PlayerPrefs.SetInt("SteamBoxText",booltoint(PlayerText.SteamBoxText));
            }
            
            //存檔點1觸發存檔
            PlayerPrefs.SetInt("Save1",booltoint(Save1));
            //存檔點2觸發存檔
            PlayerPrefs.SetInt("Save2",booltoint(Save2));
            //PlayerPrefs To Json
            PlayerPrefs.SetString("savefile",JsonUtility.ToJson(data));
        }

        //讀檔
        void LoadData()
        {   
            Debug.Log("讀檔完成");
            //Json To PlayerPrefs
            data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("savefile"));
        
            //玩家共同
            PlayerText.GuideClose = data.GuideClose = inttobool(PlayerPrefs.GetInt("GuideClose"));
            Trigger.Enter2ndFloor = data.Enter2ndFloor = inttobool(PlayerPrefs.GetInt("Enter2ndFloor"));

            //玩家1
            if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
            {
                //座標
                PlayerSpawnORLeft.Player1x = data.Player1x = PlayerPrefs.GetFloat("Player1x");
                PlayerSpawnORLeft.Player1y = data.Player1y = PlayerPrefs.GetFloat("Player1y");
                PlayerSpawnORLeft.Player1z = data.Player1z =  PlayerPrefs.GetFloat("Player1z");
                
                //物件
                PuzzleObjController.Key_Student = data.Key_Student = inttobool(PlayerPrefs.GetInt("Key_Student"));
                PuzzleObjController.Key_BiologyPuzzle = data.Key_BiologyPuzzle = inttobool(PlayerPrefs.GetInt("Key_BiologyPuzzle"));
                PuzzleObjController.Key_Biology = data.Key_Biology = inttobool(PlayerPrefs.GetInt("Key_Biology"));
                PuzzleObjController.IronDoorPuzzle = data.IronDoorPuzzle = inttobool(PlayerPrefs.GetInt("IronDoorPuzzle"));
                PuzzleObjController.SheetMusic902 = data.SheetMusic902 = inttobool(PlayerPrefs.GetInt("SheetMusic902"));
                PuzzleObjController.PianoFinish = data.PianoFinish = inttobool(PlayerPrefs.GetInt("PianoFinish"));
                PuzzleObjController.SteamBoxSwitch = data.SteamBoxSwitch = inttobool(PlayerPrefs.GetInt("SteamBoxSwitch"));
                PuzzleObjController.StudentCardP1 = data.StudentCardP1 = inttobool(PlayerPrefs.GetInt("StudentCardP1"));

                //文字
                PlayerText.StudentText = data.StudentText = inttobool(PlayerPrefs.GetInt("StudentText"));
                PlayerText.IronDoorText = data.IronDoorText = inttobool(PlayerPrefs.GetInt("IronDoorText"));
            }
            //玩家2
            else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
            {
                //座標
                PlayerSpawnORLeft.Player2x = data.Player2x = PlayerPrefs.GetFloat("Player2x");
                PlayerSpawnORLeft.Player2y = data.Player2y = PlayerPrefs.GetFloat("Player2y");
                PlayerSpawnORLeft.Player2z = data.Player2z = PlayerPrefs.GetFloat("Player2z");
                
                //物件
                PuzzleObjController.FireExtinguish = data.FireExtinguish = inttobool(PlayerPrefs.GetInt("FireExtinguish"));
                PuzzleObjController.SmokeBool = data.SmokeBool = inttobool(PlayerPrefs.GetInt("SmokeBool"));
                PuzzleObjController.Key_General = data.Key_General = inttobool(PlayerPrefs.GetInt("Key_General"));     
                PuzzleObjController.IronDoorSwitch = data.IronDoorSwitch = inttobool(PlayerPrefs.GetInt("IronDoorSwitch")); 
                PuzzleObjController.EightPuzzle = data.EightPuzzle = inttobool(PlayerPrefs.GetInt("EightPuzzle")); 
                PuzzleObjController.ZeroPuzzle = data.ZeroPuzzle = inttobool(PlayerPrefs.GetInt("ZeroPuzzle")); 
                PuzzleObjController.FivePuzzle = data.FivePuzzle = inttobool(PlayerPrefs.GetInt("FivePuzzle")); 
                PuzzleObjController.SteamBoxPuzzle = data.SteamBoxPuzzle = inttobool(PlayerPrefs.GetInt("SteamBoxPuzzle")); 
                PuzzleObjController.SheetMusic802 = data.SheetMusic802 = inttobool(PlayerPrefs.GetInt("SheetMusic802")); 
                PuzzleObjController.PianoFinish = data.PianoFinish = inttobool(PlayerPrefs.GetInt("PianoFinish")); 
                PuzzleObjController.StudentCardP2 = data.StudentCardP2 = inttobool(PlayerPrefs.GetInt("StudentCardP2")); 

                //文字
                Trigger.Smokebool = data.SmokeboolTrigger = inttobool(PlayerPrefs.GetInt("SmokeboolTrigger"));
                PlayerText.GeneralText = data.GeneralText = inttobool(PlayerPrefs.GetInt("GeneralText"));
                PlayerText.IronDoorOpenText = data.IronDoorOpenText = inttobool(PlayerPrefs.GetInt("IronDoorOpenText"));
                PlayerText.EightPuzzleText = data.EightPuzzleText = inttobool(PlayerPrefs.GetInt("EightPuzzleText"));
                PlayerText.ToiletText = data.ToiletText = inttobool(PlayerPrefs.GetInt("ToiletText"));
                PlayerText.SteamBoxText = data.SteamBoxText = inttobool(PlayerPrefs.GetInt("SteamBoxText"));
            }

            //存檔點1觸發讀檔
            Save1 = data.Save1 = inttobool(PlayerPrefs.GetInt("Save1"));
            //存檔點2觸發讀檔
            Save2 = data.Save2 = inttobool(PlayerPrefs.GetInt("Save2"));
            SceneData();
        }

        //bool與int切換
        public static int booltoint(bool i)
        {
            if(i)
                return 1;
            else
                return 0;
        }
        public static bool inttobool(int i)
        {
            if(i != 0)
                return true;
            else
                return false;
        }   

        IEnumerator SaveTextAnimation()
        {
            SaveText.text = "存檔中";
            
            for(int i = 0 ; i < 3 ; i++)
            {
                SaveText.text += ".";
                yield return new WaitForSeconds(1f);
            }
            
            TextAnimation.clip = TextAnimationClips[0];
            TextAnimation.Play();
            yield return new WaitForSeconds(2.5f);
            SaveText.text = "";
            SaveAnimationBool = false;
        }

        //存檔類別
        [System.Serializable]
        public class PlayerData
        {
            //存檔點
            public bool Save1;
            public bool Save2;

            //玩家座標
            public float Player1x;
            public float Player1y;
            public float Player1z;
            public float Player2x;
            public float Player2y;
            public float Player2z;
            
            //謎題物件判斷
            public bool Key_Student;
            public bool Key_General;
            public bool Key_Biology;
            public bool FireExtinguish;
            public bool SmokeBool;
            public bool Key_BiologyPuzzle;
            public bool IronDoorPuzzle;
            public bool IronDoorSwitch;
            
            public bool EightPuzzle;
            public bool ZeroPuzzle;
            public bool FivePuzzle;
            public bool SheetMusic902;
            public bool SheetMusic802;
            public bool PianoFinish;
            public bool SteamBoxPuzzle;
            public bool SteamBoxSwitch;
            public bool StudentCardP1;
            public bool StudentCardP2;

            //文字觸發
            public bool GuideClose;
            public bool StudentText;
            public bool IronDoorText;
            public bool GeneralText;
            public bool IronDoorOpenText;
            public bool SmokeboolTrigger;
            
            public bool Enter2ndFloor;
            public bool EightPuzzleText;
            public bool ToiletText;
            public bool SteamBoxText;
        }
    }
}