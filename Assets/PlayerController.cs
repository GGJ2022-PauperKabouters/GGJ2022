using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    [HideInInspector]
    public GameObject LocalPlayerInstance;
    private bool canMove = true;

    public float moveModifier;
    public float jumpModifier;
    public float raycastDist;

    public ShipController shipController;

    #region private fields



    #endregion
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private HarpoonController m_HarpoonController;

    private GameObject playermanager;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_HarpoonController = GetComponentInChildren<HarpoonController>();
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
                if (Input.GetAxis("Vertical") > 0 )
                {
                    m_Rigidbody.MovePosition(m_Rigidbody.position + Vector3.forward * (Time.fixedDeltaTime + moveModifier));


                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    m_Rigidbody.MovePosition(m_Rigidbody.position -Vector3.forward * (Time.fixedDeltaTime + moveModifier));
                }
                if (Input.GetAxis("Horizontal") > 0 )
                {
                    m_Rigidbody.MovePosition(m_Rigidbody.position + Vector3.right * (Time.fixedDeltaTime + moveModifier));
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    m_Rigidbody.MovePosition(m_Rigidbody.position + Vector3.left * (Time.fixedDeltaTime + moveModifier));
                }

               
                
            }
        }
        
        //meshTransform.LookAt(m_Rigidbody.velocity);
        //var dir = m_Rigidbody.velocity;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //meshTransform.rotation = Quaternion.Euler(new Vector3(0,0, 0));
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (canMove)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    setLocalPlayerCanMove(false);
                    SetHarpoonMode(true);
                }
                if (Input.GetKeyDown(KeyCode.Space) && _isGrounded())
                {
                    var velocity = m_Rigidbody.velocity;
                    velocity.y = 0;
                    m_Rigidbody.velocity = velocity;
                    m_Rigidbody.AddForce(Vector3.up * jumpModifier, ForceMode.VelocityChange);
                    m_Animator.SetTrigger("Jump");
                }
            }
        }
    }

    private bool _isGrounded()
    {
        return transform.position.y > 14.8;
    }
    
    public void setLocalPlayerCanMove(bool value)
    {
        LocalPlayerInstance.GetComponent<PlayerController>().canMove = value;
    }

    public void SetHarpoonMode(bool value)
    {
        m_Animator.SetTrigger(value ? "HarpoonActivate" : "HarpoonDeactivate");
        StartCoroutine(HarpoonTransition(value));
    }

    private IEnumerator HarpoonTransition(bool activated)
    {
        m_HarpoonController.harpoonCamera.gameObject.SetActive(activated);
        shipController.GetComponentInChildren<Camera>().enabled = !activated;
        
        // Wait for the transition animation to complete before the player can move again
        yield return new WaitForSeconds(0.25f);

        if (activated)
        {
            m_HarpoonController.SetActive(true);
        }
        else
        {
            setLocalPlayerCanMove(true);
        }
    }

    public void OnTileObtained()
    {
        Debug.Log("Player obtained a tile!");
        shipController.AddTile();
    }
}
