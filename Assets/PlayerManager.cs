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
            Debug.LogError("view is mine");
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ;
            if (Input.GetAxis("Vertical") > 0)
            {
            Debug.LogError("forward");
                m_Rigidbody.AddForce(Vector3.forward * 20);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                m_Rigidbody.AddForce(Vector3.left * 20);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                m_Rigidbody.AddForce(Vector3.right * 20);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                m_Rigidbody.AddForce(-Vector3.forward * 20);
            }
        
    }
    
}
