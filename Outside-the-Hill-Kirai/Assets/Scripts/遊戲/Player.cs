using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    PhotonView view;
    //postprocessing
    public Volume postprocessing;
    //暈眩效果
    ChromaticAberration chromatic;
    Vignette vignette;
    DepthOfField depth;
    //動畫控制器
    public Animator anim;
    //角色音效                  
    public AudioClip[] sfx;
    //角色音效撥放器
    private AudioSource voice;
    //角色座標
    public Transform groundCheck;
    //groundMask為掉落要碰到的圖層名稱
    public LayerMask groundMask;
    //轉向殺手的座標
    Vector3 ghostlook;
    //加速度
    Vector3 velocity;
    //角色控制器
    public CharacterController controller;
    //物件
    public GameObject playercamera,postprocessingobj,canvas,spotlight,allui,stopmenu,backmenuUI,ReplayUI,RawUI,buttonEUI;      
    public GameObject[] ghost;      
    //場景編號
    private int sceneindex;         
    //浮點數
    public float speed,gravity,JumpHeight,distance,groundDistance;
    //角色是否在地板上 (bool)
    public bool isGrounded; 
    //布林變數
    private bool spotlightuse,dead,tired,ReAP,running,coroutine = false;
    public static bool spotlightopen;
    //靜態布林變數
    public static bool PlayerActive = false;                

    void Awake()
    {
        view = this.gameObject.GetComponent<PhotonView>();
        
        if(!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<AudioListener>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //抓取Canvas
        canvas = GameObject.Find("Canvas");
        //抓取MainCamera
        playercamera = this.gameObject.transform.GetChild(0).gameObject;
        //動畫控制器
        anim = this.gameObject.GetComponent<Animator>();
        //抓取角色控制器
        controller = this.gameObject.GetComponent<CharacterController>();
        //抓取地板判定
        groundCheck = this.gameObject.transform.GetChild(2);
        //取得角色音效撥放器
        voice = this.gameObject.GetComponent<AudioSource>();
        //音效循環 = false
        voice.loop = false;
        //獲取場景編號
        sceneindex = SceneManager.GetActiveScene().buildIndex;
        //尋找UI
        allui = canvas.transform.GetChild(1).gameObject;
        //Raw UI
        RawUI = allui.transform.GetChild(0).gameObject;
        //ButtonE UI
        buttonEUI = RawUI .transform.GetChild(1).gameObject;
        //尋找暫停選單
        stopmenu = canvas.transform.GetChild(3).gameObject;
        //尋找Postprocessing
        postprocessingobj = GameObject.Find("PostProcessing Volume");
        postprocessing = postprocessingobj.GetComponent<Volume>();
        //尋找手電筒
        spotlight = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(view.IsMine)
        {
            CharacterControl(); 
        }   
    } 
   
    void CharacterControl()
    {
        //手電筒控制
        spotlightopen = spotlightuse;
            
        //AP控制
        AP.currentAP = Mathf.Clamp(AP.currentAP,0,100);

        //鬼魂
        ghost = GameObject.FindGameObjectsWithTag("Ghost");
    
        //作弊模式1
        if(Input.GetKeyDown(KeyCode.P))
        {
            speed = 10f;
            HP.currentHP = 9999;
            AP.currentAP = 9999;
            OriginMode.God = true;
        }
        //作弊模式2
        if(Input.GetKeyDown(KeyCode.I))
        {
            PuzzleObjController.PasswordCorrect = true;
            PuzzleObjController.DollsPuzzle = true;
        }
        //瞬移
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(OPMove());
        }
        
        //角色活躍 = true
        if(PlayerActive == true)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            //transform.right = X軸 , transform.forward = Z軸
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            //疲勞濾鏡效果
            if(postprocessing.profile.TryGet(out vignette) && postprocessing.profile.TryGet(out chromatic) && postprocessing.profile.TryGet(out depth))
            {
                //跳躍
                if(Input.GetButtonDown("Jump") && isGrounded && speed != 1.5f)
                {
                    //Mathf.Sqrf = 取平方根 (根據物理的結果)
                    velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
                    AP.currentAP = AP.currentAP - 10;
                    
                    if(vignette.intensity.value < 0.5f)
                    {
                        vignette.intensity.value += 0.05f;
                    }
                    
                    if(depth.focalLength.value < 200)
                    {
                        depth.focalLength.value += 20;
                    }
                    
                    chromatic.intensity.value += 0.1f;
                }
                
                //跑步
                if(Input.GetKey(KeyCode.LeftShift) && AP.currentAP > 0 && tired == false && coroutine == false)
                {   
                    coroutine = true;
                    running  = true;
                    ReAP = false;
                    speed = 6f;
                    StartCoroutine(Runtime());
                }
                
                if((Input.GetKeyUp(KeyCode.LeftShift) || AP.currentAP <= 0))
                {
                    running = false;
                    ReAP = true;
                    
                    if(AP.currentAP <= 0)
                    {
                        tired = true;
                        speed = 1.5f;
                    }
                    else
                    {
                        speed = 3.5f;
                    }
                }
                
                if(((running == false && ReAP == true) || isGrounded) && coroutine == false)
                {
                    coroutine = true;
                    StartCoroutine(ReAPtime());
                }
            }
          
            //開關手電筒
            if(Input.GetKeyDown(KeyCode.F))
            {                  
                voice.clip = sfx[1];
                voice.loop = false;
                voice.Play();         
                view.RPC("spotlightRPC",RpcTarget.All);
            }

            //暫停選單
            if(Input.GetKeyDown(KeyCode.Tab) && Ending.endready == false)
            {
                GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = true;
                stopmenu.SetActive(true);
                allui.SetActive(false);
                PlayerActive = false;
                MouseLook.mouselookbool = false;
                voice.Stop();
                Time.timeScale = 0f;
                DeviceSelect.keyboardused = false;
            }
                
            //疲勞狀態
            if(voice.isPlaying && AP.currentAP >= 40)
            {
                tired = false;
                voice.clip = sfx[1];
                voice.loop = false;
            }
            else if(!voice.isPlaying && AP.currentAP < 40)
            {
                voice.clip = sfx[0];
                voice.loop = true;
                voice.Play();
            }
      
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);    
        }
        
        //死亡
        for(int i = 0 ; i < ghost.Length ; i++)
        {
            //作弊模式3
            if(Input.GetKeyDown(KeyCode.O))
            {
                ghost[i].SetActive(false);
            }
            
            //與鬼魂的距離
            distance = Vector3.Distance(ghost[i].transform.position,this.transform.position);
    
            if(distance < 2.5f && OriginMode.God == false)
            {
                switch(ghost[i].name)
                {
                    //木頭人
                    case "木頭人":
                    {
                        if(RayControl.woodghoststop == false)
                        {
                            ghostlook = ghost[i].transform.position - transform.GetChild(0).position;
                            PlayerDead();
                        }
                    }
                    break;
                    
                    //厭光怪
                    case "厭光怪1": case "厭光怪2":
                    {
                        if(RayControl.hatelightghostTrack == true && spotlightopen == true)
                        {
                            ghostlook = ghost[i].transform.position - transform.GetChild(0).position;
                            PlayerDead();
                        }
                    } 
                    break;

                    //靈界鬼魂
                    case "靈界鬼魂1": case "靈界鬼魂2": case "殭屍1": case "殭屍2":
                    {
                        ghostlook = ghost[i].transform.position - transform.GetChild(0).position;
                        PlayerDead();
                    } 
                    break;
                }
            }
        }
    }

    //GameOver
    void PlayerDead()
    {
        view.RPC("GameOver",RpcTarget.All);
        //殺手角度
        Quaternion rotation = Quaternion.LookRotation(new Vector3(ghostlook.x,ghostlook.y + 1.5f,ghostlook.z));
        //緩慢轉向殺手         
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation , 150f * Time.deltaTime);
    }

    //開關手電筒
    [PunRPC]
    void spotlightRPC()
    {
        spotlightuse = !spotlightuse;
        spotlight.GetComponent<Light>().enabled = spotlightuse;   
    }

    //同步GAMEOVER
    [PunRPC]
    void GameOver()
    {
        Time.timeScale = 1f;
        HP.currentHP = 0;
        PlayerActive = false;
    }

    //跑步
    IEnumerator Runtime()
    {
        AP.currentAP = AP.currentAP - 1;
        
        if(vignette.intensity.value < 0.5f)
        {
            vignette.intensity.value += 0.005f;
        }
        if(depth.focalLength.value < 200)
        {
            vignette.intensity.value += 0.005f;
        }
        
        vignette.intensity.value += 0.005f;
        
        yield return new WaitForSeconds(0.1f);
        coroutine = false;
    }

    //回復AP
    IEnumerator ReAPtime()
    {
        if(AP.currentAP <= 0)
        {
            yield return new WaitForSeconds(3f);
        }
        
        AP.currentAP = AP.currentAP + 1;
        vignette.intensity.value -= 0.005f;
        chromatic.intensity.value -= 0.01f;
        depth.focalLength.value -= 2;
        yield return new WaitForSeconds(0.1f);
        coroutine = false;
    }

    //瞬移至圖書館
    IEnumerator OPMove()
    {
        yield return new WaitForEndOfFrame();
        this.gameObject.transform.position = new Vector3(50.4f,6f,-4.9f);
    }
}