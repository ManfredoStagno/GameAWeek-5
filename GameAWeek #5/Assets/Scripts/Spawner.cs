using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public PlatformPool platformPool;
    
    public List<PlatformObject> activePlatforms = new List<PlatformObject>();


    public Transform startingPlatform;
    public int startingCount;

    public float despawnDistance;
    public float yGap;
    public float xGap;

    private PlatformObject firstPlatform;
    private PlatformObject lastPlatform;

    private void Start()
    {
        PlatformObject platform = platformPool.Pop(startingPlatform.transform.position);
        firstPlatform = platform;
        lastPlatform = platform;

        for (int i = 0; i < startingCount; i++)
        {
            Add(platformPool.Pop(DetermineSpawnPosition(lastPlatform)));
        }
    }

    private void Update()
    {
        if (player.position.x > firstPlatform.transform.position.x + despawnDistance)
        {
            PlatformObject platform = platformPool.Pop(DetermineSpawnPosition(lastPlatform));
            Add(platform);
            Remove(firstPlatform);
        }
    }

    private Vector3 DetermineSpawnPosition(PlatformObject lastPlat)
    {
        float minX = lastPlat.width + lastPlat.width;

        float y = lastPlat.transform.position.y + Random.Range(-yGap, yGap);
        float x = lastPlatform.transform.position.x + minX + Random.Range(0f, xGap);

        return new Vector3(x, Random.Range(-yGap, yGap), 0f);
    }

    public void Add(PlatformObject platform)        
    {
        activePlatforms.Add(platform);

        lastPlatform = platform;        
    }

    public void Remove(PlatformObject platform)
    {
        activePlatforms.Remove(platform);
        firstPlatform = activePlatforms[0];
        platform.ReturnToPool();
    }


}
