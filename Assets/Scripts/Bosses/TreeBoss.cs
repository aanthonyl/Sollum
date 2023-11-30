using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : EnemyHealth
{
    //I hate copying this like this, bad hack bad hack
    public GateKey key;

    public override void EnemyDie()
    {
        Debug.Log("ENEMY DIE");
        key.Activate();
        Destroy(this.gameObject);
    }
}
