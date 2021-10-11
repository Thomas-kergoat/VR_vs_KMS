﻿using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace WS3
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {

        public static NetworkManager Instance;

        public GameObject teleporting;

        [Tooltip("The prefab to use for representing the user on a PC. Must be in Resources folder")]
        public GameObject playerPrefabPC;

        [Tooltip("The prefab to use for representing the user in VR. Must be in Resources folder")]
        public GameObject playerPrefabVR;


        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. 
        /// </summary>
        public override void OnLeftRoom()
        {
            // TODO: load the Lobby Scene
            SceneManager.LoadScene("LobbyScene");
        }

        /// <summary>
        /// Called when Other Player enters the room and Only other players
        /// </summary>
        /// <param name="other"></param>
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
            // TODO: 
            
        }
        
        /// <summary>
        /// Called when Other Player leaves the room and Only other players
        /// </summary>
        /// <param name="other"></param>
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
            // TODO: 
        }
        #endregion


        #region Public Methods

        /// <summary>
        /// Our own function to implement for leaving the Room
        /// </summary>
        public void LeaveRoom()
        {
            // TODO: 
            PhotonNetwork.LeaveRoom();
        }

        private void updatePlayerNumberUI()
        {
            // TODO: Update the playerNumberUI

        }

        void Start()
        {
            Instance = this;

            var UserDeviceManager = GetComponent<UserDeviceManager>();

            if (playerPrefabPC == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefabPC Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                // TODO: Instantiate the prefab representing my own avatar only if it is UserMe
                if (UserManager.UserMeInstance == null)
                {
                    if (UserDeviceManager.GetDeviceUsed() == UserDeviceType.HTC)
                    {
                        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                        PhotonNetwork.Instantiate("Prefabs/" + playerPrefabVR.name, new Vector3(this.transform.position.x, 1.5f, this.transform.position.z), Quaternion.identity, 0);
                        teleporting.SetActive(true);
                    }
                    else if (UserDeviceManager.GetDeviceUsed() == UserDeviceType.PC)
                    {
                        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                        PhotonNetwork.Instantiate("Prefabs/" + playerPrefabPC.name, new Vector3(this.transform.position.x, 1.5f, this.transform.position.z), Quaternion.identity, 0);
                    }

                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        private void Update()
        {

            if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                // Code to leave the room by pressing CTRL + the Leave button
                if (Input.GetButtonUp("Leave"))
                {
                    Debug.Log("Leave event");
                    LeaveRoom();
                }
            }
            
        }
        #endregion
    }
}