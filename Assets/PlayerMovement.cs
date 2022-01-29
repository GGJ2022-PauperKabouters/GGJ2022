using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    Rigidbody m_Rigidbody;
    //public float m_Thrust = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
       m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        else{
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
