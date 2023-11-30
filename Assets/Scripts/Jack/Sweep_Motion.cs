using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep_Motion : MonoBehaviour
{
    public float rotationSpeed = 90f; // Speed at which the hitbox rotates in degrees per second
    public float arcRadius = 2f; // Radius of the arc
    public float attackDuration = 2f; // Duration of the attack
    public int damage = 10; // Damage dealt by the attack
    public LayerMask targetLayers; // Layers to detect (typically set to the player's layer)

    private float attackStartTime;
    private Collider hitboxCollider;

    void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        hitboxCollider.enabled = false; // Disable the hitbox initially
    }

    void Update()
    {
        if (Time.time - attackStartTime < attackDuration)
        {
            // Rotate the hitbox around the boss
            float angle = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, angle);

            // Move the hitbox along an arc
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Sin(Time.time - attackStartTime) * arcRadius;
            transform.position = newPosition;
        }
        else
        {
            // Disable the hitbox when the attack duration is over
            hitboxCollider.enabled = false;
        }
    }

    public void StartAttack()
    {
        attackStartTime = Time.time;
        hitboxCollider.enabled = true;
    }

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
