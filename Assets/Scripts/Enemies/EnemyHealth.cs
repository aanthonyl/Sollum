/*
    Script Added by Aurora Russell
	10/05/2023
	// ENEMY HEALTH SYSTEM //
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // TYPES OF ENEMIES THAT CAN BE SELECTED AND CUSTOMIZED
    public enum EnemyType
    {
        GruntEnemy,
        ThrowEnemy,
        ShootEnemy,
    }
    public EnemyType enemyType = new EnemyType();

    private SpriteRenderer sprite;

    public float enemyHealth;
    // WHIP DAMAGE AMOUNT CAN BE CHANGED IF NEED BE
    public float whipDamageAmount = 10;

    private bool damageCoolDown = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        // DIFFERENT ENEMY HEALTH AMOUNTS
        if (enemyType == EnemyType.GruntEnemy)
        {
            enemyHealth = 20;
        }
        else if (enemyType == EnemyType.ThrowEnemy)
        {
            enemyHealth = 40;
        }
        else if (enemyType == EnemyType.ShootEnemy)
        {
            enemyHealth = 60;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // ENTERED WHIP ATTACK TRIGGER
        if (other.gameObject.name == "WhipAttackZone")
        {
            Debug.Log("ENTERED WHIP ZONE");
            TakeWhipDamage();
        }
    }
    
    // ENEMY TAKES DAMAGE AND DIES IF HEALTH HITS 0
    public void TakeWhipDamage()
    {
        if (damageCoolDown == false)
        {
            Debug.Log("ENEMY TAKE WHIP DAMAGE");

            enemyHealth -= whipDamageAmount;

            StartCoroutine(FlashRed());

            if (enemyHealth <= 0)
            {
                EnemyDie();
            }
        }
    }

    // ENEMY DIES, OBJECT DESTROYED
    public void EnemyDie()
    {
        Debug.Log("ENEMY DIE");
        Destroy(this.gameObject);
    }

    // ENEMY FLASHES RED TO SIGNIFY DAMAGE TAKEN
    private IEnumerator FlashRed()
    {
        damageCoolDown = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.8f);
        damageCoolDown = false;
    }
}