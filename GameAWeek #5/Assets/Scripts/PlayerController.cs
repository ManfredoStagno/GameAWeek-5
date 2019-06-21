using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    public float deathTimer = 500;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        MoveAndJump();
        GameOver();
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

        if(!cc.isGrounded)
            moveVector.y -= appliedGravity;

        cc.Move(moveVector * Time.deltaTime);
    }

    private float counter = 0;
    private void GameOver()
    {
        if (!cc.isGrounded && moveVector.y < 0)
        {
            counter++;
            Debug.Log("counter");
        }
        else
        {
            counter = 0;
        }

        if (moveVector.y < 0 && counter > deathTimer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            counter = 0;
        }
    }
}
