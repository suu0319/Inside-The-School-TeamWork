using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
    PhotonView view;
    //角色座標
    public Transform playerBody;
    //滑鼠靈敏度
    public float mouseSensitivity;
    //X座標
    float xRotation = 0f;
    private bool shake = false;
    //滑鼠是否可以移動 (鏡頭移動)
    public static bool mouselookbool = true;
    
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if(view.IsMine)
        {
            PlayerCamera();
        }
    }

    void PlayerCamera()
    {
        //鏡頭可以移動
        if(mouselookbool == true)
        {
            //計算鏡頭移動速度
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;  

            xRotation -= mouseY;
            
            //計算鏡頭移動範圍)
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);
            //鏡頭轉向 (Quaternion.Euler是轉xRotation)
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //Vector3.up = Vector3(0, 1, 0)
            playerBody.Rotate(Vector3.up * mouseX); 
            
            //如果進入死亡動畫
            if(HP.currentHP == 0)
            {
                mouselookbool = false;
            }   

            //蒸飯箱引爆
            if(PuzzleObjController.SteamBoxSwitch == true && shake == false && GameData.GameData.Save2 == false)
            {
                shake = true;
                StartCoroutine(Shake(2f,3f));
            }
        }
    }

    IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originPos;
    }
}