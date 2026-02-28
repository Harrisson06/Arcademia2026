using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CollectableItem : Interactable
{
    public ItemData itemData;

    protected override void OnInteract()
    {
        InteractablePromptUI.Instance?.HidePrompt();
        Inventory.Instance.AddItem(itemData);
        
        if (itemData.collectSound != null)
            AudioSource.PlayClipAtPoint(itemData.collectSound, (Vector3)transform.position);

        Destroy(gameObject);
    }
}
