using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject explosionFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Tile")
        {
            GameObject child = Instantiate(explosionFX, gameObject.transform);
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<Rigidbody>(), 2);
        }
        

    }
}
