using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] BoxCollider col;
    private void Start()
    {
        col = this.transform.parent.GetComponent<BoxCollider>();
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
