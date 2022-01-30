using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{   
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region public fields

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;


        #endregion
        #region Private Methods


        private void Start()
        {
            
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            {
                Debug.Log("We are Instantiating LocalPlayer from {0}");
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate

                byte count = PhotonNetwork.CurrentRoom.PlayerCount;
                Transform floor =  GameObject.Find("PlayerLand (" + count + ")").transform;
                Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
                for (int i = 0; i < cameras.Length; i++)
                {
                    Camera cam = cameras[i];
                    if (cam.name == "Camera (" + count + ")")
                    {
                        cam.enabled = true;
                    }
                    else
                    {
                        cam.enabled = false;
                    }
                }
                
                
                GameObject playerObj = PhotonNetwork.Instantiate(this.playerPrefab.name,new Vector3(floor.localPosition.x,floor.localPosition.y + 5f, floor.position.z +1f) , Quaternion.identity, 0);
                playerObj.GetComponentInChildren<PlayerController>().shipController = floor.GetComponent<ShipController>();

            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }




        #endregion

        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        #region Photon Callbacks



        #endregion

        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        
        #endregion

    }
}