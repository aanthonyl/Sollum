using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasolColDetection : MonoBehaviour
{
    BlockParryController controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<BlockParryController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            if (controller.isParrying())
            {
                Debug.Log("parried");
                Destroy(other.gameObject);
                controller.ParryProj();
                controller.KnockPlayer();
            }
            else if (controller.isBlocking())
            {
                Debug.Log("blocked");
                Destroy(other.gameObject);
                controller.KnockPlayer();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (controller.isBlocking())
            {
                // other.gameObject.GetComponent<EnemyStateManager>().KnockedBack(transform.forward);
            }
        }
        else if (other.CompareTag("Break"))
        {
            if (controller.isAttacking())
            {
                other.gameObject.GetComponent<TempBreak>().Break();
            }
        }
    }
}
