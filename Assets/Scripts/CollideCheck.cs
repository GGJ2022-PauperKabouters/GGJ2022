using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    private GameObject cannonBall;
    private Rigidbody rigi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GameObject land = PhotonNetwork.Instantiate("LandtileDivided",transform.position,Quaternion.Euler(transform.localEulerAngles));
            land.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.SetActive(false);
            Rigidbody[] bodies = land.GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody b in bodies)
            {
                
                b.AddExplosionForce((-col.relativeVelocity.y * 5000), b.transform.position, 0,2);
            }
        }
    }
}
