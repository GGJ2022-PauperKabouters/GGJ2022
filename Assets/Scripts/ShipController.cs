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
        _tileSpawnPoints = SpawnPointParent.GetComponentsInChildren<TileSpawnPoint>().ToList();
    }

    public void AddTile()
    {
        var nextSpawn = _getNextSpawnPoint();

        if (nextSpawn == null)
        {
            Debug.Log("ShipController: Trying to add a tile failed because there's no empty tile spawn points remaining.");
            return;
        }

        GameObject tileObject = nextSpawn.isCannonTile ? cannonTilePrefab : floorTilePrefab;
        nextSpawn.SpawnTile(tileObject, transform);
    }

    // Returns a spawn point with the highest spawn priority that is not occupied and also adjacent to an occupied spawn point.
    private TileSpawnPoint _getNextSpawnPoint()
    {
        List<TileSpawnPoint> occupiedPoints = _tileSpawnPoints.Where(x => x.IsOccupied()).ToList();

        if (occupiedPoints.Count == 0)
            return null;
        
        List<TileSpawnPoint> availablePoints = new List<TileSpawnPoint>();
        
        occupiedPoints.ForEach(x =>
        {
            availablePoints.AddRange(x.GetAdjacentSpawnPoints(true));
        });

        if (availablePoints.Count == 0)
        {
            return _tileSpawnPoints.First(x => !x.IsOccupied());
        }

        SpawnPriority bestPrio = availablePoints.OrderBy(x => (int) x.spawnPriority).First().spawnPriority;
        var options = availablePoints.Where(x => x.spawnPriority == bestPrio).ToArray();

        return options[Random.Range(0, options.Length)];

    }
}
