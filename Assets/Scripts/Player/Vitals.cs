using UnityEngine;
using UnityEngine.Rendering;

public class Vitals : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Stamina")]
    public int maxStamina = 100;
    private int currentStamina;

    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void TakeHealth(int amount)
    {
        if (currentHealth - amount <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= amount;
        }
        UpdateHealthBar();
    }

    public void GiveHealth(int amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // do health bar stuff
    }

    void Die()
    {

    }

    public void TakeStamina(int amount)
    {
        if (currentStamina - amount <= 0)
        {
            currentStamina = 0;
        }
        else
        {
            currentStamina -= amount;
        }
        UpdateStaminaBar();
    }

    public void GiveStamina(int amount)
    {
        if (currentStamina + amount >= maxStamina)
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += amount;
        }
        UpdateStaminaBar();
    }

    public void UpdateStaminaBar()
    {
        // do stamina bar stuff
    }
}
