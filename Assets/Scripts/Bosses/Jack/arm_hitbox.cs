using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class arm_hitbox : MonoBehaviour
{
    private int count;
    private EnemyMelee dealDamage;
    private void Start()
    {
        dealDamage = GetComponent<EnemyMelee>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            count++;
            Debug.Log("Player was Hit! " + count);
            dealDamage.AttackPlayer();
        }
    }
}