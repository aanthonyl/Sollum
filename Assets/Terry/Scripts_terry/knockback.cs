using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// handles entering the knockback state of the statemachine
// used for testing
public class knockback : MonoBehaviour
{
    [SerializeField] GameObject stateMachineClass;
    EnemyStateMachine enemyStateMachine;
    void Start()
    {
        enemyStateMachine = stateMachineClass.GetComponent<EnemyStateMachine>();
    }

    void OnTriggerStay(Collider other){
        if (other.tag == "Enemy" && Input.GetKeyDown("a")){
            enemyStateMachine.currrentState = (EnemyStateMachine.State)1;
        }
    }

    
}
