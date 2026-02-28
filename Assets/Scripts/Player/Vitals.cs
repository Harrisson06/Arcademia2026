using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public Image healthFill;

    [Header("Stamina")]
    public int maxStamina = 100;
    [SerializeField] private float currentStamina;
    public Image staminaFill;

    private Inventory inventory;


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
            //Die();
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
        if (targetFill > 0.5f){
            healthFill.color = Color.green;
        }
        else if (targetFill > 0.3f)
        {
            healthFill.color = Color.yellow;
        } else
        {
            healthFill.color = Color.red;
        }
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, targetFill, Time.deltaTime * 5f);
    }

    public bool TakeStamina(int amount)
    {
        if (currentStamina - amount < 0)
            return false;        
        
        currentStamina -= amount;
        return true;
    }

    public void GiveStamina(float amount)
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
        float targetFill = (float)currentStamina / maxStamina;
        staminaFill.fillAmount = Mathf.Lerp(staminaFill.fillAmount, targetFill, Time.deltaTime * 5f);
    }
}
