using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    [Range(0, 100)]
    [SerializeField] private int currentHealth;
    public Image healthBar;

    [Header("Stamina")]
    public int maxStamina = 100;
    [Range(0, 100)]
    [SerializeField] private float currentStamina;
    public Image staminaBar;

    [Header("Money")]
    private int money = 0;
    public TextMeshProUGUI moneyText;

    public Inventory inventory;

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
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFill, Time.deltaTime * 5f);
    }

    void Die()
    {

    }

    public bool TakeStamina(float amount)
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
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, targetFill, Time.deltaTime * 5f);

    }

    public void TakeMoney(int amount)
    {

    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void UpdateMoney()
    {
        moneyText.text = $"Money\n${money}";
    }

    private void CollectItem()
    {
        
    }
}
