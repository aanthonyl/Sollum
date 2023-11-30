using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep_Hitbox : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the attack
    public LayerMask targetLayers; // Layers to detect (typically set to the player's layer)

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            // Check if the object collided with is on the target layers
            // Deal damage to the player or other objects
            PlayerHealth targetHealth = other.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }
}
