using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] GameObject stateMachineClass;
    EnemyStateMachine enemyStateMachine;
    Rigidbody rb;
    public float push_time = 0.1f;
    public float pushVelocity = 50.0f;
    float stunTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyStateMachine = stateMachineClass.GetComponent<EnemyStateMachine>();
    }

    public void Knockback()
    {
        //Debug.Log("Knockedback");
        StartCoroutine(stopPush());

    }
   
    IEnumerator stopPush()
    {
        rb.velocity = -gameObject.transform.forward * pushVelocity;
        yield return new WaitForSeconds(push_time);
        rb.velocity = Vector3.zero;
        StartCoroutine(startMoving());

    }
    IEnumerator startMoving()
    {
        yield return new WaitForSeconds(stunTime);
        enemyStateMachine.currrentState = 0;
    }
}
