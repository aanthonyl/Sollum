using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] BoxCollider col;
    
    EnemyMelee enemyMelee;
    private void Start()
    {
        enemyMelee = col.gameObject.GetComponent<EnemyMelee>();
    }

    public void EnemyStartAtk()
    {
        col.enabled = true;
    }

    public void EnemyStopAtkCol()
    {
        StartCoroutine(StopAttack());
    }

    public IEnumerator StopAttack() {
        yield return new WaitForEndOfFrame();
        enemyMelee.HandleInteraction();
        col.enabled = false;

    }
}
