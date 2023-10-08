using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAttack : MonoBehaviour
{
    public Transform chargeTarget;
    private Boss bossComponent;

    private void Awake()
    {
        bossComponent = GetComponent<Boss>();
    }

    public void TriggerChargeAttack()
    {
        if (chargeTarget && bossComponent && bossComponent.currentPhase == 2) 
        {
            StartCoroutine(ChargeRoutine());
        }
    }

    private IEnumerator ChargeRoutine()
    {
        yield return new WaitForSeconds(2f); 

        float chargeSpeed = 5f;
        while (Vector3.Distance(transform.position, chargeTarget.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, chargeTarget.position, chargeSpeed * Time.deltaTime);
            yield return null;
        }

        if (Vector3.Distance(transform.position, chargeTarget.position) <= 0.1f)
        {
            bossComponent.Die();
        }
    }

    private IEnumerator BossDeathCharge()
    {
        yield return new WaitForSeconds(1f);

        Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + directionAwayFromPlayer * 10f; // temporary target position for charge

        float chargeSpeed = 7f; 
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, chargeSpeed * Time.deltaTime);
            yield return null;
        }
        bossComponent.Die();
    }
}
