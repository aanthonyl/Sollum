using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    BoxCollider col;
    private void Start()
    {
        col = transform.parent.GetComponent<BoxCollider>();
    }

    public void EnemyStartAtk()
    {
        col.enabled = true;
    }

    public void EnemyStopAtkCol()
    {
        col.enabled = false;
    }
}
