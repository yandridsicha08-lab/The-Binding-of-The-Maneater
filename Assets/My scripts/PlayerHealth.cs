using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public DeathUI deathText;
    public int maxHealth = 100;
    public int currentHealth = 100;

    void Start()
    {

        currentHealth = maxHealth;
        HealthUI.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        HealthUI.instance.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Time.timeScale = 0f;

        DeathUI.instance.UpdateDeath();
        
    }
}
