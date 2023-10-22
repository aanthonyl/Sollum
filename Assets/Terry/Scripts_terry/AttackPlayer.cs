using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy script for testing
// Attack state of the enemy state machine
public class AttackPlayer : MonoBehaviour
{
    public bool attacking = false;
    float waitTime = 2.0f;
    Rigidbody rb;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject player;
    [SerializeField] EnemyStateMachine enemyStateMachine;
    [SerializeField] GameObject sp;
    SpriteRenderer sprite;
    AttackPlayer attackPlayer;

    void Start()
    {
        sprite = sp.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void Attack()
    {
        rb.velocity = Vector3.zero;
        sprite.color = Color.yellow;
        enemyStateMachine.currrentState = (EnemyStateMachine.State)3;
        StartCoroutine(Attacking());

    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(waitTime);
        sprite.color = Color.red;
        attacking = true;
        yield return new WaitForSeconds(waitTime);
        sprite.color = Color.white;
        yield return new WaitForSeconds(waitTime);
        if (Mathf.Abs(player.transform.position.x - rb.transform.position.x) > 2.0f)
            enemyStateMachine.currrentState = 0;
        else
            Attack();


    }

    void OnTriggerStay(Collider other)
    {
        if (attacking)
        {
            // this needs to be tweaked 
            // makes the melee block and parry a little unreliable
            // possibly wait a few seconds before setting bool false
            attacking = false;
        }
    }
}
