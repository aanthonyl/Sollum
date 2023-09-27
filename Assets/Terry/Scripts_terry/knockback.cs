using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float thrust = 2000f;
    public float push_time = 0.1f;
    public bool knockedBack = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void knockBackForPeriod()
    {

    }

    void OnTriggerStay(Collider other){
        if (other.tag == "Enemy" && Input.GetKeyDown("a")){
            Rigidbody enemy_rb = other.GetComponent<Rigidbody>();
            enemy_rb.AddForce(transform.up * thrust, ForceMode.VelocityChange);
            knockedBack = true;
            StartCoroutine(stopPush(push_time, enemy_rb));
        }
    }

    IEnumerator stopPush(float secs, Rigidbody enemy_rb){
        yield return new WaitForSeconds(secs);
        knockedBack = false;
        enemy_rb.velocity = Vector3.zero;
    }
}
