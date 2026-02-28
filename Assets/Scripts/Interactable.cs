using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [Header("Prompt Settings")]
    [Tooltip("Label shown in the UI prompt, e.g. '[E] Pick up Sword'")]
    [SerializeField] protected string promptMessage = "Interact";

    [Tooltip("Key the player must press to interact")]
    [SerializeField] protected KeyCode interactKey = KeyCode.E;

    [Header("Events")]
    public UnityEvent onInteractStart;
    public UnityEvent onInteractEnd;
    public UnityEvent onPlayerEnterRange;
    public UnityEvent onPlayerExitRange;

    [Header("Detection")]
    [SerializeField] protected float interactRadius = 2f;
    [SerializeField] protected LayerMask playerLayer;

    protected bool playerInRange;
    protected Transform playerTransform;

    protected virtual void Update()
    {
        CheckForPlayer();

        if (playerInRange && Input.GetKeyDown(interactKey) && IsClosestInteractable())
        {
            Interact();
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius); // still fine for editor visualization
    }

    protected abstract void OnInteract();
    protected virtual void Interact()
    {
        onInteractStart?.Invoke();
        OnInteract();
        onInteractEnd?.Invoke();
    }

    private void CheckForPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, playerLayer);
        bool isInRange = hit != null;

        if (isInRange && !playerInRange)
        {
            playerInRange = true;
            playerTransform = hit.transform;
            OnPlayerEnterRange();
            onPlayerEnterRange?.Invoke();
            InteractablePromptUI.Instance?.ShowPrompt(promptMessage, interactKey, transform.position);
        }
        else if (!isInRange && playerInRange)
        {
            playerInRange = false;
            playerTransform = null;
            OnPlayerExitRange();
            onPlayerExitRange?.Invoke();
            InteractablePromptUI.Instance?.HidePrompt();
        }

        if (playerInRange)
        {
            if (IsClosestInteractable())
                InteractablePromptUI.Instance?.ShowPrompt(promptMessage, interactKey, transform.position);
            else
                InteractablePromptUI.Instance?.HidePrompt();
        }
    }

    private bool IsClosestInteractable()
    {
        float myDistance = Vector2.Distance(transform.position, playerTransform.position);

        // Find all interactables in the scene
        Interactable[] allInteractables = FindObjectsByType<Interactable>(FindObjectsSortMode.None);

        foreach (var other in allInteractables)
        {
            if (other == this) continue;
            if (!other.playerInRange) continue;

            float otherDistance = Vector2.Distance(other.transform.position, playerTransform.position);
            if (otherDistance < myDistance) return false;
        }
        return true;
    }

    protected virtual void OnPlayerEnterRange() { }

    protected virtual void OnPlayerExitRange() { }

    public bool IsPlayerInRange => playerInRange;
    public string PromptMessage
    {
        get => promptMessage;
        set
        {
            promptMessage = value;
            if (playerInRange)
                InteractablePromptUI.Instance?.ShowPrompt(promptMessage, interactKey, transform.position);
        }
    }
}
