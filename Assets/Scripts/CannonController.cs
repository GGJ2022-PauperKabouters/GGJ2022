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
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = SteerPivot.eulerAngles;
    }

    // Update is called once per frame
    void Update()
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
}
