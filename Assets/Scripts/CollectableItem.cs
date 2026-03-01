using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CollectableItem : Interactable
{
    public ItemData itemData;
    public string uniqueID;

    // Shared across all instances, resets when game restarts
    private static HashSet<string> collectedItems = new HashSet<string>();

    private void Awake()
    {
        if (collectedItems.Contains(uniqueID))
        {
            Destroy(gameObject);
        }
    }

    protected override void OnInteract()
    {
        InteractablePromptUI.Instance?.HidePrompt();
        Inventory.Instance.AddItem(itemData);

        if (itemData.collectSound != null)
            AudioSource.PlayClipAtPoint(itemData.collectSound, transform.position);

        collectedItems.Add(uniqueID);
        Destroy(gameObject);
    }
}