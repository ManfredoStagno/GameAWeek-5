using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : ObjectPool<PlatformPool, PlatformObject, Vector3>
{
    public Spawner spawner;
    public float offScreenOffset;
}

public class PlatformObject : PoolObject<PlatformPool, PlatformObject, Vector3>
{
    public Transform transform;
    public Spawner spawner;
    
    public float offScreenOffset;
    public float width;
    
    //this is basically the object start method
    protected override void SetReferences()
    {
        //Assign public fields from this pool object
        transform = instance.transform;
        objectPool.spawner = spawner;
        width = transform.localScale.x / 2;
        offScreenOffset = objectPool.offScreenOffset;

        //assign fields in the objectscript 
        
    }

    public override void WakeUp(Vector3 position)
    {
        transform.position = position;
        instance.SetActive(true);
    }

    public override void Sleep()
    {
        instance.SetActive(false);               
    }
}
