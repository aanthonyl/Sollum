using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce the player's health by the damage amount.
        currentHealth -= damageAmount;

        
        if (currentHealth <= 0)
        {
            Debug.Log("Die");
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);
        
    }
}
