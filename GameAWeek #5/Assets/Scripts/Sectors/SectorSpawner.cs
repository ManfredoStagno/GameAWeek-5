using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSpawner : MonoBehaviour
{    
    public Transform player;
    
    private Dictionary<string, SectorPool> poolDictionary = new Dictionary<string, SectorPool>();
    private SectorPool[] pools;
    private List<string> keys = new List<string>();
    private List<SectorObject> activeSectors = new List<SectorObject>();

    public Transform startingPosition;
    public int startingCount;

    private SectorObject firstSector;
    private SectorObject lastSector;

    public float despawnDistance;
    public float gap;

    public int beforeRest;

    //public List<string> verbs;
    //public List<string> counterVerbs;
    //private Dictionary<string, string> language;

    private void Start()
    {
        //Initialize dictionaries and lists
        pools = GetComponents<SectorPool>();
        foreach (SectorPool pool in pools)
        {
            poolDictionary.Add(pool.prefabKey, pool);
            keys.Add(pool.prefabKey);
        }
        //Initialize activeSectors
        SpawnRandomSector(startingPosition.transform.position);
        firstSector = lastSector;
        //Spawn first batch of sectors
        for (int i = 0; i < startingCount; i++)
        {
            SpawnRandomSector(DetermineSpawnPosition(lastSector));
        }
    }

    private int counter = 0;
    private void Update()
    {        
        if (player.position.x > firstSector.transform.position.x + despawnDistance)
        {
            if (counter == beforeRest)
            {
                SpawnSector(DetermineSpawnPosition(lastSector), "RunFlat");
                counter = 0;
                beforeRest++;
            }
            else
            {
                SpawnRandomSector(DetermineSpawnPosition(lastSector));
            }
            if (lastSector.objectPool.prefabKey != "RunFlat")
            {
                counter++;
            }

            Remove(firstSector);
        }
    }

    private void SpawnRandomSector(Vector3 position)
    {
        string randomKey = keys[Random.Range(0, keys.Count - 1)];
        SectorObject sector = poolDictionary[randomKey].Pop(position);
        Add(sector);
    }

    private void SpawnSector(Vector3 position, string key)
    {
        SectorObject sector = poolDictionary[key].Pop(position);
        Add(sector);
    }

    private Vector3 DetermineSpawnPosition(SectorObject lastSector)
    {
        Vector3 position = Vector3.right * gap;
        position += lastSector.rightBoundary.position;
        return position;
    }

    public void Add(SectorObject sector)
    {
        activeSectors.Add(sector);
        lastSector = sector;
    }

    public void Remove(SectorObject sector)
    {
        activeSectors.Remove(sector);
        firstSector = activeSectors[0];
        sector.ReturnToPool();
    }
}
