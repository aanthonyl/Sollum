/*
    Script Added by Aurora Russell
	09/30/2023
	// SUPER BASIC MOVEMENT TO TEST DIALOGUE TRIGGER AND CANARY FOLLOW FEATURES //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float moveVelocity;
    public Rigidbody2D rb;
    public Collider2D col;
    private bool isGrounded = true;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isGrounded == true)
        {
            //JUMP
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jump);
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jump));
                isJumping = true;
            }

            // HORIZONTAL FLIP
            Vector3 characterScale = transform.localScale;

            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        //HORIZONTAL MOVEMENT
        moveVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }
}
