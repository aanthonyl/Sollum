using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_character : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed = 10.0f;
    Rigidbody rb;
    bool knockedBackByPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void lookAtPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
    }

    void stepForward()
    {
        rb.velocity = transform.forward * enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        knockedBackByPlayer = player.GetComponentInChildren<knockback>().knockedBack;
        if (!knockedBackByPlayer)
        {
            lookAtPlayer();
            stepForward();
        }
            
        knockedBackByPlayer = false;
        
    }
}
