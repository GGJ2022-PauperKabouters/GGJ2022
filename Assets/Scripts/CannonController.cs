using System;
using System.Collections;
using System.Collections.Generic;
using ThreeEyedGames.DecaliciousExample;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float horizontalSteer = 1;
    public float verticalSteer = 1;
    public int horizontalSteerRange = 90;
    public int verticalSteerRange = 45;
    public float ShotVelocity = 15;
   

    public Transform SteerPivot;
    private Vector3 initialRotation;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;

    public GameObject textPrefab;

    private GameObject floatingtext;
    private bool player_inrange;
    private bool inUse;

    public GameObject playermanager;

    public Camera maincamera;

    public Camera cannoncamera;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = SteerPivot.eulerAngles;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if (other.gameObject == playermanager.GetComponent<CustomPlayerManager>().LocalPlayerInstance)
        {
            Debug.Log("trigger enter");
            player_inrange = true;
            floatingtext = showTooltip();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playermanager.GetComponent<CustomPlayerManager>().LocalPlayerInstance)
        {
            Debug.Log("trigger exit");
            player_inrange = false;
            inUse = false;
            hideTooltip(floatingtext);
        }
    }
    // Update is called once per frame
    void Update()
    {

        checkUseCannon();

        if (inUse)
        {
            Vector3 yRotation = transform.rotation.eulerAngles;
            Vector3 xRotation = SteerPivot.rotation.eulerAngles;

            float yAxis = yRotation.y + GetInputAxis(KeyCode.D, KeyCode.A) * horizontalSteer;
            float xAxis = yRotation.x + GetInputAxis(KeyCode.W, KeyCode.S) * verticalSteer;

            yRotation.y = yAxis;
            yRotation.x = xAxis;

            transform.rotation = Quaternion.Euler(yRotation);

            if (Input.GetKeyDown(KeyCode.Space))
                SpawnBullet();

        }
    }

    private int GetInputAxis(KeyCode posKey, KeyCode negKey)
    {
        int output = 0;
        if (Input.GetKey(posKey)) output++;
        if (Input.GetKey(negKey)) output--;
        return output;
    }

    private void SpawnBullet()
    {
        GameObject cannonBall = BulletPrefab;

        GameObject spawned = Instantiate(cannonBall, BulletSpawnPoint.position, SteerPivot.rotation);

        spawned.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,1,0) * ShotVelocity, ForceMode.VelocityChange);
    }

    private GameObject showTooltip()
    {
        Vector3 tooltippos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
       GameObject tooltip = Instantiate(textPrefab, tooltippos,new Quaternion(0,0,0,0));
        return tooltip;
    }

    private void checkUseCannon()
    {
        if (player_inrange && Input.GetKeyDown(KeyCode.E))
        {
            if (inUse == false)
            {
                inUse = true;
                maincamera.enabled = false;
                cannoncamera.enabled = true;
                playermanager.GetComponent<CustomPlayerManager>().setLocalPlayerCanMove(false);
            }
            else
            {
                inUse = false;
                maincamera.enabled = true;
                cannoncamera.enabled = false;
                playermanager.GetComponent<CustomPlayerManager>().setLocalPlayerCanMove(true);
            }


        }
    }
    private void hideTooltip(GameObject floatingtext)
    {
        if (floatingtext != null)
        {
            Destroy(floatingtext);
        }
    }
}
