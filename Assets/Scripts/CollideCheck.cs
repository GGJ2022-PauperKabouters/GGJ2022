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
        rigi = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
       if (col.gameObject.tag == "Bullet")
        {
            rigi.isKinematic = false;
            Destroy(col.gameObject);
        }

    }
}
