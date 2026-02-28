using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public GameObject HealthBar;
    public Image FillImage;

    [Header("Stamina")]
    public int maxStamina = 100;
    [SerializeField] private int currentStamina;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    private void LateUpdate()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
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
    }

    public void UpdateHealthBar()
    {
        float targetFill = (float)currentHealth / maxHealth;
        FillImage.fillAmount = Mathf.Lerp(FillImage.fillAmount, targetFill, Time.deltaTime * 5f);
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
    }

    public void UpdateStaminaBar()
    {
        // do stamina bar stuff
    }
}
