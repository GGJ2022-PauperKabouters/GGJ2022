using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TileSpawnPoint : MonoBehaviour
{
    [Tooltip("Sets the importance of this tile for the survivability of the ship to make it respawn quicker.")]
    public SpawnPriority spawnPriority;

    public GameObject LightAnimPrefab;
    
    // The tile object (if any) that is currently occupying this spawn point
    public GameObject TileObject;

    public bool isCannonTile;
    
    // Start is called before the first frame update
    void Start()
    {   
        isCannonTile = TileObject != null && (TileObject.GetComponentInChildren<CannonController>() || TileObject.GetComponentInChildren<CannonControllerSimple>());
    }

    public bool IsOccupied()
    {
        return TileObject != null;
    }

    // Find adjacent spawn tiles using raycast
    public List<TileSpawnPoint> GetAdjacentSpawnPoints(bool filterOccupied)
    {
        Vector3[] directions = new Vector3[]
        {
            Vector3.back,
            Vector3.forward,
            Vector3.left,
            Vector3.right
        };

        Vector3 origin = transform.position + GetComponent<BoxCollider>().center;
        List<TileSpawnPoint> output = new List<TileSpawnPoint>();
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(origin, directions[i]), out hit, 1f, LayerMask.GetMask("TileSpawnPoint")))
            {
                TileSpawnPoint point = hit.transform.GetComponent<TileSpawnPoint>();
                
                if (!filterOccupied || !point.IsOccupied())
                    output.Add(point);
            }
        }
        
        return output;
    }

    public void SpawnTile(GameObject tilePrefab, Transform parent)
    {
        TileObject = Instantiate(tilePrefab, transform.position, Quaternion.identity, parent);

        // Do the bouncing scale Y animation
        var initialScale = TileObject.transform.localScale;

        TileObject.transform.localScale = new Vector3(initialScale.x, 0, initialScale.z);

        
        // Do the light circle animation
        GameObject LightFX = Instantiate(LightAnimPrefab, transform.position, Quaternion.identity, parent);
        
        var lightScale = LightFX.transform.GetChild(0).localScale;
        LightFX.transform.GetChild(0).localScale = new Vector3(lightScale.x, 0f, lightScale.z);

        Sequence seq = DOTween.Sequence();

        seq.Append(LightFX.transform.GetChild(0).DOScaleY(-.5f, .75f).SetEase(Ease.OutCirc));
        seq.AppendInterval(0.5f);
        seq.Append(TileObject.transform.DOScaleY(initialScale.y, .75f).SetEase(Ease.OutBack));
        seq.AppendInterval(0.5f);
        seq.Append(LightFX.transform.GetChild(0).DOScaleY(0, .75f).SetEase(Ease.InCirc));

        seq.onComplete += () =>
        {
            Destroy(LightFX);
        };
        
        seq.Play();
    }

}

public enum SpawnPriority
{
    High = 1,
    Medium = 2,
    Low = 3
}
