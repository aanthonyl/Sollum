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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("EnemyProjectile"))
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
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetType() == typeof(CapsuleCollider)) {
                Debug.Log("is capsule");
                if (controller.isBlocking())
                {
                    Debug.Log("Should be knocked back");
                    controller.KnockPlayer();
                }
            } else {
                Debug.Log("is not capsule");
            }
        }
        else if (other.gameObject.CompareTag("Break"))
        {
            Debug.Log("Breakable");
            if (controller.isAttacking())
            {
                Debug.Log("Should break");
                other.gameObject.GetComponent<TempBreak>().Break();
            }
        }
    }
}
