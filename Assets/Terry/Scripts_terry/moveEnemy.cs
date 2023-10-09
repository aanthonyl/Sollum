using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// used for testing. moves an enemy on one axis for x amount of time and then back 
// for the same amount of time. this script is used in combination with the 
// enemy projectile shooting mechanic to test the player's ability to parry and 
// block projectiles at different angles
public class moveEnemy : MonoBehaviour
{
    public float enemySpeed = 10.0f;
    public GameObject player;
    Rigidbody rb;
    float walkTime = 2.0f;
    bool rightMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void lookAtPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
    }

    void walkComplete()
    {
        if(rightMove)
            rightMove = false;
        else
            rightMove = true;
        walkTime = 3.0f;
    }

    void moveRight() {rb.velocity = new Vector3(0 , 0, 10);}
    void moveLeft() {rb.velocity = new Vector3(0, 0, -10); }
    void moveDirection()
    {
        if (rightMove)
        {
            moveRight();
            return;
        }
        moveLeft();
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        moveDirection();
        walkTime -= Time.deltaTime;
        if (walkTime < 0.0f)
            walkComplete();
    }
}
