using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Camera cam;

    public Vector3 offset;
    public float smoothSpeed;

    public Vector3 lookAtOffset;

    void Start()
    {
        cam = Camera.main;
    }

    
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player.position + lookAtOffset);

    }
}
