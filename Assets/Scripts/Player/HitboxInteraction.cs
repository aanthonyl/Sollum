using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxInteraction : MonoBehaviour
{
    [SerializeField] BlockParryController bpc;
    [SerializeField] PlayerKnockback pk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy") 
        {
            GameObject enemy = collider.gameObject;
            if (bpc.isParrying()) {
                
            } else if (bpc.isBlocking()) {
                pk.BlockRecoil();
            } else if (bpc.isAttacking()) {
                
            }
        }
    }

    void Attack(GameObject enemy) {
        
    }
    void Block(GameObject enemy) {

    }
    void Parry(GameObject enemy) {

    }
}


