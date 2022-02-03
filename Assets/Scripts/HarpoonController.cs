using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    public float horizontalSteer;
    public float verticalSteer;
    
    public float shotCooldown;
    public float shotDistance;
    public float shotDelay;

    private float shotCooldownCounter = 0;
    
    public Camera harpoonCamera;
    public Transform harpoonCrosshair;

    private bool _inUse = false;
    private bool _isShooting = false;

    private PlayerController playerController;

    private Animator m_Animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        m_Animator = playerController.GetComponent<Animator>();
    }

    void Update()
    {
        if (_inUse && !_isShooting)
        {
            //For vertical aiming, move the crosshair position up/down
            Vector3 aimPosition = harpoonCrosshair.localPosition;
            aimPosition.y = aimPosition.y + Input.GetAxis("Vertical") * (verticalSteer/2f) * Time.deltaTime;

            //For horizontal aiming, rotate the player object.
            Vector3 yRotation = playerController.transform.rotation.eulerAngles;
            yRotation.y += Input.GetAxis("Horizontal") * horizontalSteer * Time.deltaTime;
            yRotation.x += Input.GetAxis("Vertical") * -horizontalSteer * Time.deltaTime;

            harpoonCrosshair.localPosition = aimPosition;
            playerController.transform.eulerAngles = yRotation;

            //Set the harpoon blend animation parameter to blend between idle/moving animations
            var blend = m_Animator.GetFloat("HarpoonBlend");
            if (Input.GetAxis("Horizontal") != 0)
            {
                m_Animator.SetFloat("HarpoonBlend", Mathf.Min(blend + 3 * Time.deltaTime, 0.85f));
            }
            else
            {
               m_Animator.SetFloat("HarpoonBlend", Mathf.Max(blend - 3 * Time.deltaTime, 0));
            }

            //Deactivate the harpoon
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetActive(false);
                playerController.SetHarpoonMode(false);
            }

            //Shoot it yey
            if (Input.GetKeyDown(KeyCode.Space) && shotCooldownCounter == 0)
            {
                StartCoroutine(Shoot());
            }

            if (shotCooldown > 0)
            {
                shotCooldownCounter = Mathf.Max(shotCooldownCounter - Time.deltaTime, 0);
            }
        }
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        
        Vector3 aimDirection = harpoonCrosshair.transform.position - harpoonCamera.transform.position;

        RaycastHit hit;
        float boxExtents = 0.05f;
        if (Physics.BoxCast(harpoonCrosshair.transform.position, new Vector3(boxExtents, boxExtents, boxExtents), aimDirection, out hit, Quaternion.identity, shotDistance, LayerMask.GetMask("Floor")))
        {
            hit.transform.GetComponent<FloatingTile>().OnHarpoonHit(playerController.transform);
            Debug.Log("hit tile");            
            yield return new WaitForSeconds(shotDelay);
        }
        
        
        _isShooting = false;
        shotCooldownCounter = shotCooldown;
    }

    public void SetActive(bool value)
    {
        _inUse = value;
    }
}
