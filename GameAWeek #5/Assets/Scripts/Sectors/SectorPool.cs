using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorPool : ObjectPool<SectorPool, SectorObject, Vector3>
{
    public string prefabKey;
}

public class SectorObject : PoolObject<SectorPool, SectorObject, Vector3>
{
    public Transform transform;
    public Sector sector;
    public Transform leftBoundary;
    public Transform rightBoundary;

    protected override void SetReferences()
    {
        //Assign public fields from this pool object
        transform = instance.transform;
        sector = instance.GetComponent<Sector>();

        //Assign on this object the fields coming from the prefab
        leftBoundary = sector.leftBoundary;
        rightBoundary = sector.rightBoundary;

        //assign fields in the objectscript 

    }

    public override void WakeUp(Vector3 position)
    {
        Vector3 offset = transform.position - leftBoundary.position;
        transform.position = position + offset;
        instance.SetActive(true);
    }

    public override void Sleep()
    {
        instance.SetActive(false);
    }
}
