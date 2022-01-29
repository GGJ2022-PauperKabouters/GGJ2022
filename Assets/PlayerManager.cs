using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    

    #region public fields
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    #endregion

    #region private fields



    #endregion
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    void Start()
    {
       
    }

    private void Awake()
    {
       
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                Debug.LogError("photon view is not mine");
                return;
            }
            else if (photonView.IsMine == true && PhotonNetwork.IsConnected == true)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    m_Rigidbody.AddForce(Vector3.forward);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    m_Rigidbody.AddForce(Vector3.left);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    m_Rigidbody.AddForce(Vector3.right);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    m_Rigidbody.AddForce(-Vector3.forward);
                }
            }
        }
    
}
