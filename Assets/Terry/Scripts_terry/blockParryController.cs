using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BlockParryController : MonoBehaviour
{
    float keyDownTime = 0.0f;
    float keyHoldTime = 0.0f;
    public float parryModeTime = 0.3f; 
    bool parryWindow = false;
    bool blockPressed = false;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    Parry parry;


    void Start()
    {
        parry = parryClass.GetComponent<Parry>();
    }
    void Update()
    {

        if (Input.GetKeyDown("space")){
            keyDownTime = Time.time;
            parryWindow = true;
            blockPressed = true;
        }
        if (Input.GetKey("space"))
            keyHoldTime = Time.time - keyDownTime;
        if (keyHoldTime > parryModeTime )
            parryWindow = false;
        if (Input.GetKeyUp("space")){ 
            parryWindow = false;
            blockPressed = false;
        }
    }

    void OnTriggerStay(Collider other){
        if (other.tag == "Projectile" && blockPressed){
            Rigidbody projectile_rb = other.GetComponent<Rigidbody>();
            if(!parryWindow){
                Debug.Log("blocked");     
                Destroy(other.gameObject);
            }
            else if(parryWindow){
                Debug.Log("parried");
                Destroy(other.gameObject);
                parry.PlayerShoot();
            }

        }

    }
}

