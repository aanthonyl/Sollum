using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    public Image HealthBar;
    private bool invincible;
    [HideInInspector]
    public bool isDead = false;

    private void Start()
    {
        //respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        invincible = false;
        currentHealth = maxHealth;
    }

    public void SetInvincibility(bool state)
    {
        invincible = state;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!invincible)
        {
            // Reduce the player's health by the damage amount.
            currentHealth -= damageAmount;
            UpdateHealthBar();
        }

        if (currentHealth <= 30) //Set to 30 to work around player not dying for 3 hits after meter is empty
        {
            Debug.Log("Die");
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);
        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        SetInvincibility(false);
    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / (float)maxHealth;
        if (fillAmount > 1)
        {
            fillAmount = 1.0f;
        }

        HealthBar.fillAmount = fillAmount;
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(RespawnManager.instance.RespawnPlayer(gameObject));
            invincible = true;
        }
    }
}
