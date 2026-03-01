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
        if (GameManager.Instance != null)
        {
            maxHealth = GameManager.Instance.maxHealth;
            currentHealth = GameManager.Instance.currentHealth;
            maxStamina = GameManager.Instance.maxStamina;
            currentStamina = GameManager.Instance.currentStamina;
            money = GameManager.Instance.money;
        }
    }

    private void SyncToManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveVitals(currentHealth, (int)currentStamina);
            GameManager.Instance.money = money;
            GameManager.Instance.maxHealth = maxHealth;   // in case debuff changed it
            GameManager.Instance.maxStamina = maxStamina;
        }
    }

    private void LateUpdate()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateMoney();
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
        SyncToManager();
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
        SyncToManager();
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
        SyncToManager();
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
        SyncToManager();
    }

    public void UpdateStaminaBar()
    {
        float targetFill = (float)currentStamina / maxStamina;
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, targetFill, Time.deltaTime * 5f);
    }

    public void TakeMoney(int amount)
    {
        SyncToManager();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SyncToManager();
    }

    public void UpdateMoney()
    {
        moneyText.text = $"Money\n${money}";
    }
}
