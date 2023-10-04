using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public State currrentState;
    [SerializeField] GameObject enemyKnockbackClass;
    [SerializeField] GameObject followPlayerClass;
    EnemyKnockback knockbackInteraction;
    public enum State
    {
        FollowPlayer,
        KnockedBack,
        AttackPlayer,
        NoUpdate
    }
    Follow_Player followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        knockbackInteraction= enemyKnockbackClass.GetComponent<EnemyKnockback>();
        followPlayer = followPlayerClass.GetComponent<Follow_Player>();
        currrentState= 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currrentState)
        {
            case State.FollowPlayer:
                followPlayer.WalkToPlayer();
                break;
            case State.KnockedBack:
                knockbackInteraction.Knockback();
                currrentState = (State)3;
                break;
            case State.AttackPlayer:
                break;
            case State.NoUpdate: 
                break;

                

        }
    }
    
}


