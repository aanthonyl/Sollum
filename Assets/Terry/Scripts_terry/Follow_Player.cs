using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follow Player State of the Enemy State machine
// An example used mostly for testing
public class Follow_Player : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed = 5.0f;
    Rigidbody rb;
    [SerializeField] EnemyStateMachine enemyStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void WalkToPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
        rb.velocity = transform.forward * enemySpeed;
    }

    public void CheckIfInRange()
    {
        if (Mathf.Abs(player.transform.position.x - rb.transform.position.x ) < 2.0f)
        {
            enemyStateMachine.currrentState = (EnemyStateMachine.State)2;
        }
    }

   
}
