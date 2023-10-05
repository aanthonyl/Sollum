using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public enum EnemyType
    {
        GruntEnemy,
        ThrowEnemy,
        ShootEnemy,
    }
    public EnemyType enemyType = new EnemyType();

    private SpriteRenderer sprite;

    public float enemyHealth;
    public float whipDamageAmount = 10;

    private bool damageCoolDown = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

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
        if (other.gameObject.name == "WhipAttackZone")
        {
            Debug.Log("ENTERED WHIP ZONE");
            TakeWhipDamage();
        }
    }
    
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

    public void EnemyDie()
    {
        Debug.Log("ENEMY DIE");
        Destroy(this.gameObject);
    }

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