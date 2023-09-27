/*
	Script Added by Aurora Russell
	09/26/2023
	// CANARY FOLLOW PLAYER //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaryFollow : MonoBehaviour
{
    public float MinDistance = 3;
    public float MaxDistance = 1;
    public float Speed = 3;
    public Transform Player;

    private float moveX;
    private float moveY;

    void Update()
    {
        // CONTROLS 
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        // FOLLOW 
        if (Vector3.Distance(transform.position, Player.position) >= MinDistance)
        {
            Vector3 follow = Player.position;

            this.transform.position = Vector3.MoveTowards(this.transform.position, follow, Speed * Time.deltaTime);
        }

        // DIRECTION 
        if (moveX < 0.0f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        else if (moveX > 0.0f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
}
