using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] BoxCollider col;
    private void Start()
    {
        if (col == null)
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
