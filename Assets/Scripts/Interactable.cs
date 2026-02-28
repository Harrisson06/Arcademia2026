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
    protected bool hasInteracted;
    protected Transform playerTransform;

    protected virtual void Update()
    {
        CheckForPlayer();

        if (playerInRange && !hasInteracted && Input.GetKeyDown(interactKey))
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
