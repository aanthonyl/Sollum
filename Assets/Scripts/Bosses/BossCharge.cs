using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : MonoBehaviour
{
    // recoil after collision or slide past?
    public float chargeSpeed = 10f;
    public float recoilSpeed = 5f;
    public float recoilDistance = 3f;
    private Vector3 chargeTarget;
    private Vector3 recoilTarget;
    public Transform playerTarget;
    private bool isCharging = false;
    private bool isRecoiling = false;
    private float chargeCooldown = 10f;
    private float damageCooldown = 5f;
    private bool canCharge = true;


    void Update()
    {
        if (isCharging)
        {
            float step = chargeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, chargeTarget, step);

            if (Vector3.Distance(transform.position, chargeTarget) < 0.001f)
            {
                isCharging = false;
                StartRecoil();
            }
        }
        else if (isRecoiling)
        {
            float step = recoilSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, recoilTarget, step);

            if (Vector3.Distance(transform.position, recoilTarget) < 0.001f)
            {
                isRecoiling = false;
            }
        }
    }

    public void ChargeTowards()
    {
        if (!canCharge) return;

        isCharging = true;
        chargeTarget = playerTarget.position;

        StartCoroutine(ChargeCooldownRoutine());
    }

    private void StartRecoil()
    {
        isRecoiling = true;
        Vector3 recoilDirection = (transform.position - chargeTarget).normalized;
        recoilTarget = transform.position + recoilDirection * recoilDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        // add tag to player for collision
        if (isCharging && other.gameObject.CompareTag("ChargeTarget"))
        {
            isCharging = false;
            StartRecoil();
        }
    }

    private IEnumerator ChargeCooldownRoutine()
    {
        canCharge = false;
        yield return new WaitForSeconds(chargeCooldown);
        canCharge = true;
        
        // Automatically initiate a charge after the cooldown
        ChargeTowards();
    }

    public void TriggerDamageCooldown()
    {
        StartCoroutine(DamageCooldownRoutine());
    }

    private IEnumerator DamageCooldownRoutine()
    {
        canCharge = false;
        yield return new();
    }
}
