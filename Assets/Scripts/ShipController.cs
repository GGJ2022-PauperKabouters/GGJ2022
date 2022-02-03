using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public GameObject floorTilePrefab;
    public GameObject cannonTilePrefab;
    
    public GameObject SpawnPointParent;
    
    private List<TileSpawnPoint> _tileSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void AddTile()
    {
       

        GetComponent<Respawner>().spawnTile();
    }

    // Returns a spawn point with the highest spawn priority that is not occupied and also adjacent to an occupied spawn point.
    
}
