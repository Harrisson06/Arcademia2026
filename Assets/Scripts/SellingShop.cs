using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellingShop : Interactable
{
    public Canvas ShopUI;
    private Vitals playerVitals;
    private Movement playerMovement;
    public TextMeshProUGUI MoneyText;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerVitals = player.GetComponent<Vitals>();
            playerMovement = player.GetComponent<Movement>();
        }

    }
    public void RemoveDebuffs()
    {
        playerMovement.maxSpeed = 5f;
        playerMovement.dashForce = 10f;
        playerVitals.maxHealth = 100;
        playerVitals.maxStamina = 100;
    }

    protected override void OnInteract()
    {
        if (ShopUI == null) return;

        bool isOpen = ShopUI.gameObject.activeSelf;

        if (!isOpen)
            OpenShop();
        else
            CloseShop();
    }

    private void OpenShop()
    {
        MoneyText.text = $"${GetItemsWorth()}";
        ShopUI.gameObject.SetActive(true);
        var firstButton = ShopUI.GetComponentInChildren<UnityEngine.UI.Button>();
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            playerMovement.rb.linearVelocity = Vector3.zero;
        }
    }

    private void CloseShop()
    {
        ShopUI.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    public int GetItemsWorth()
    {
        int worth = 0;
        foreach (var item in Inventory.items)
        {
            print("ITEM WORTH:" + item.sellValue);
            worth += item.sellValue;
        }
        return worth;
    }

    public void SellItems()
    {
        playerVitals.AddMoney(GetItemsWorth());
        playerVitals.UpdateMoney();
        Inventory.items.Clear();
        MoneyText.text = "$0";
        Inventory.Instance.text.text  = "Relics\n0";
        RemoveDebuffs();
    }
}