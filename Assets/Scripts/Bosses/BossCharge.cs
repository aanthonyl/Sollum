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
    private bool isCharging = false;
    private bool isRecoiling = false;

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

    public void ChargeTowards(Vector3 target)
    {
        isCharging = true;
        chargeTarget = target;
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
        if (isCharging && other.gameObject.CompareTag("Player"))
        {
            isCharging = false;
            StartRecoil();
        }
    }
}
