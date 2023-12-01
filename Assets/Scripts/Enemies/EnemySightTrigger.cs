using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    EnemyStateManager esm;
    void Start()
    {
        esm = transform.parent.GetComponent<EnemyStateManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        //If enemy sees player, chase
        if (col.gameObject.CompareTag("Player") && !esm.isPlayerHidden) //if what enters the collider is the player AND the player is not hidden
        {
            esm.SawPlayer(col.gameObject.transform);
        }
    }
}
