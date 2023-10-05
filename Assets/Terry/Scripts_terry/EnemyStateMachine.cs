using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public State currrentState;
    [SerializeField] GameObject enemyKnockbackClass;
    [SerializeField] GameObject followPlayerClass;
    EnemyKnockback knockbackInteraction;
    Follow_Player followPlayer;
    [SerializeField] GameObject attackPlayerClass;
    AttackPlayer attack;
    EnemyStateMachine esm;
    public enum State
    {
        FollowPlayer,
        KnockedBack,
        AttackPlayer,
        NoUpdate
    }
    // Start is called before the first frame update
    void Start()
    {
        knockbackInteraction= 
            enemyKnockbackClass.GetComponent<EnemyKnockback>();
        followPlayer = 
            followPlayerClass.GetComponent<Follow_Player>();
        attack = 
            attackPlayerClass.GetComponent<AttackPlayer>();
        currrentState= 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currrentState)
        {
            case State.FollowPlayer:
                followPlayer.WalkToPlayer();
                followPlayer.CheckIfInRange();
                break;
            case State.KnockedBack:
                knockbackInteraction.Knockback();
                currrentState = (State)3;
                break;
            case State.AttackPlayer:
                attack.Attack();
                break;
            case State.NoUpdate: 
                break;

                

        }
    }
    
}


