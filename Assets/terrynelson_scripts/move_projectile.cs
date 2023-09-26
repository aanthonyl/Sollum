using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_projectile : MonoBehaviour
{
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-100, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
