using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace outsidetheschoolPUN
{
    public class ServerConnect : MonoBehaviourPunCallbacks
    {
        PhotonView view;
        public InputField roomID;
        public GameObject lobby,WaittingPlayer,Joinfail;
        public Animation anim;
        public AnimationClip[] animclips;
        private bool gamestart = false;
        private bool joinfailtext,returnbug = false;
        public static bool PlayerP1,PlayerP2 = false;
        public static int PlayerID;
 
        // Start is called before the first frame update
        void Start()
        {
            if(PuzzleObjController.End == true)
            {
                PhotonNetwork.LeaveRoom();
            }
            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }

            PhotonNetwork.ConnectUsingSettings();

            view = this.gameObject.GetComponent<PhotonView>();
            anim = Joinfail.GetComponent<Animation>();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Update()
        {
            if(roomID.IsActive())
            {   
                roomID.ActivateInputField();
                StartCoroutine(ReturnBug());

                if(Input.GetKeyDown(KeyCode.Return) && returnbug == true)
                {
                    JoinOrCreatePrivateRoom();
                }
            }
            else
            {
                returnbug = false;
            }

            if(PlayerP1 == true && PlayerP2 == true && gamestart == false)
            {
                gamestart = true;
                PhotonNetwork.LoadLevel("前導");
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("成功連線至伺服器");
        }
        public void JoinOrCreatePrivateRoom()
        {
            //空白房間ID
            if(roomID.text == "")
            {
                if(joinfailtext == false)
                {
                    joinfailtext = true;
                    Joinfail.SetActive(true);
                    StartCoroutine(Joinfailcount());
                }
            }
            
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
            PhotonNetwork.JoinOrCreateRoom(roomID.text,roomOptions,null);
            roomOptions.IsVisible = false;
        }
        public override void OnJoinedRoom()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount < 2) 
            {
                lobby.SetActive(false);
                WaittingPlayer.SetActive(true);
            }
            
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PlayerID = 1;
                view.RPC("PlayerCreateRoom",RpcTarget.All);
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PlayerID = 2;
                view.RPC("PlayerJoinRoom",RpcTarget.All);
            }
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            if(joinfailtext == false)
            {
                joinfailtext = true;
                Joinfail.SetActive(true);
                StartCoroutine(Joinfailcount());
            }
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            if(joinfailtext == false)
            {
                joinfailtext = true;
                Joinfail.SetActive(true);
                StartCoroutine(Joinfailcount());
            }
        }

        //加入失敗文字動畫
        IEnumerator Joinfailcount()
        {
            anim.clip = animclips[1];
            anim.Play();
            yield return new WaitForSeconds(3f);
            Joinfail.SetActive(false);
            joinfailtext = false;
        }

        //防止Enter輸入兩次
        IEnumerator ReturnBug()
        {
            yield return new WaitForEndOfFrame();
            returnbug = true;
        }

        [PunRPC]
        void PlayerCreateRoom()
        {
            PlayerP1 = true;
            //Debug.Log("玩家1");
        }
    
        [PunRPC]
        void PlayerJoinRoom()
        {
            PlayerP2 = true;
            //Debug.Log("玩家2");
        }
    }
}

