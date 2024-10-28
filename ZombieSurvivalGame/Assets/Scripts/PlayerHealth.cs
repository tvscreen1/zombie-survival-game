using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    public GameOverManager gameOverManager;
    void Start()
    {
        currentHealth = maxHealth;

        // Initialize health bar UI
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        // Update health bar UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update health bar UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log("Player healed. Current health: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Player died.");

        // Call the GameOver method
        if (gameOverManager != null)
        {
            gameOverManager.GameOver();
        }
    }

}