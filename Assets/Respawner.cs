using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    public List<GameObject> platformTiles;
 
    void Start()
    {

        initializePlatformLocations();

    }
    
    public Vector3 scale = new Vector3(1f, 1f, 1f);
    void initializePlatformLocations()
    {
        foreach (Transform t in transform)
        {
            if (t.tag == "Tile")
            {
                platformTiles.Add(t.gameObject);
            }
        }



    }

    public void spawnTile()
    {
        foreach(GameObject t in platformTiles)
        {
            if (!t.activeInHierarchy)
            {
                t.SetActive(true);
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
