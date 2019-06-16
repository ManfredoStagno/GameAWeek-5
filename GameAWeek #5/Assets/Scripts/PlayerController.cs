using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;    

    Vector3 moveVector = Vector3.zero;
    public float speed;
    public float jumpSpeed;

    public float gravity;
    [Range(0,1)]
    public float highJumpMultiplier;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        MoveAndJump();  
    }

    void MoveAndJump()
    {
        float appliedGravity = gravity * Time.deltaTime; //delta time not needed, but useful for tuning

        if (Input.GetKey(KeyCode.Space))
        {
            if (cc.isGrounded)
            {
                moveVector.y = jumpSpeed;
            }

            if (moveVector.y > 0)
            {
                appliedGravity *= highJumpMultiplier;
            }
        }

        moveVector.x = speed;
        moveVector.y -= appliedGravity;

        cc.Move(moveVector * Time.deltaTime);
    } 
}
