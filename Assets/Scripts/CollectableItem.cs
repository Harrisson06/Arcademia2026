using UnityEngine;

public class CollectableItem : Interactable
{
    protected override void OnInteract()
    {
        InteractablePromptUI.Instance?.HidePrompt();
        print("COLLECTED");
        Destroy(gameObject);
    }
}
