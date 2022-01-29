using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public GameObject LocalPlayerInstance;
    private bool canMove = true;

    #region private fields



    #endregion
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;

    private GameObject playermanager;
    void Start()
    {
        // Start is called before the first frame update
    }


        private void Awake()
        {
            m_Rigidbody = gameObject.GetComponent<Rigidbody>();
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                //Camera playercam = gameObject.AddComponent<Camera>();
                // playercam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 30, gameObject.transform.position.z - 20);
                //transform.LookAt(gameObject.transform, Vector3.up);
                //playercam.tag = "MainCamera";
                // playercam.enabled = true;
                LocalPlayerInstance = this.gameObject;
            LocalPlayerInstance.tag = "Player";
            {

            }

            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {


            if (canMove)
            {
                if (Input.GetAxis("Vertical") > 0)
                {

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

        }
    public void setLocalPlayerCanMove(bool value)
    {
        LocalPlayerInstance.GetComponent<PlayerController>().canMove = value;
    }
}
