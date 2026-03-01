using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Stamina")]
    public int maxStamina = 100;
    public float currentStamina = 100f;

    [Header("Money")]
    public int money = 0;

    [Header("Movement")]
    public float maxSpeed = 5f;
    public float dashForce = 10f;

    [Header("Inventory")]
    public List<ItemData> items = new();

    [Header("Transition")]
    public Vector3 transitionLoc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveVitals(int curHP, int curStamina)
    {
        currentHealth = curHP;
        currentStamina = curStamina;
    }

    public void SaveMovement(float speed, float dash)
    {
        maxSpeed = speed;
        dashForce = dash;
    }

    public void SaveInventory(List<ItemData> newItems)
    {
        items = new List<ItemData>(newItems);
    }
}
