using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed = 5.0f;
    Rigidbody rb;

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

   
}
