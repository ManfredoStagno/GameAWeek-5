using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPool : ObjectPool<MyPool, MyObject>
{
   
}

public class MyObject : PoolObject<MyPool, MyObject>
{
    public Transform transform;
    //Components that the object definitely has
        //i.e. rigidbodies or component scripts

    //this is basically the object start method
    protected override void SetReferences() // when does this get called?????
    {
        transform = instance.transform;
        //Assign public fields from this pool object
        //assigna reference of this pool object into the object
        //assign fields from the component script on the prefab
    }

    public override void WakeUp() //cange class if u need information to be passed here
    {
        //set active
        //set position, set force, velocity whatever
    }

    public override void Sleep()
    {
        //set inactive
        //reset values that don't override on WakeUp
    }
}