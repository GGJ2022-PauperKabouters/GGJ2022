using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{

    #region public fields
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    #endregion

  
    //public float m_Thrust = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void Awake()
    {
     
    }

    // Update is called once per frame

}
