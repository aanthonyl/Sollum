using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    float keyDownTime = 0.0f;
    float keyHoldTime = 0.0f;
    public float parryModeTime = 0.5f; 
    bool parry = false;
    bool blockPressed = false;
    public float parryVelocity = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            keyDownTime = Time.time;
            parry = true;
            blockPressed = true;
        }
        if (Input.GetKey("space"))
            keyHoldTime = Time.time - keyDownTime;
        if (keyHoldTime > parryModeTime )
            parry = false;
        if (Input.GetKeyUp("space"))
            blockPressed = false;
    }

    void OnTriggerStay(Collider other){
        if (other.tag == "Projectile" && blockPressed){
            Rigidbody projectile_rb = other.GetComponent<Rigidbody>();
            if(!parry){
                Debug.Log("block");     
                projectile_rb.velocity = Vector3.zero;
            }
            else{
                Debug.Log("parry");
                projectile_rb.velocity = new Vector3(parryVelocity, 0.0f, 0.0f);
            }

        }

    }
}

