using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GateKey key;

    public void ActivateKey()
    {
        key.Activate();
    }
}
