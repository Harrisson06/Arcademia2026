using System.Collections;
using UnityEngine;
using UnityEngine.tvOS;
using UnityEngine.UI;

public class InteractablePromptUI : MonoBehaviour
{
    public static InteractablePromptUI Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Image promptImage;

    [Header("Key Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private KeySpritePair[] keySprites;

    [Header("Animation")]
    [SerializeField] private float fadeSpeed = 5f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    private Vector3 worldTargetPosition;
    private Vector3 worldOffset = new Vector3(0, 1.5f, 0);

    [System.Serializable]
    public struct KeySpritePair
    {
        public KeyCode key;
        public Sprite sprite;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        canvasGroup = promptImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = promptImage.gameObject.AddComponent<CanvasGroup>();

        promptImage.gameObject.SetActive(false);
    }

    public void ShowPrompt(string message, KeyCode key, Vector3 worldPosition)
    {
        promptImage.sprite = GetSpriteForKey(key);
        promptImage.gameObject.SetActive(true);
        worldTargetPosition = worldPosition;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
    }

    private void Update()
    {
        if (!promptImage.gameObject.activeInHierarchy) return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldTargetPosition + worldOffset);
        promptImage.rectTransform.position = screenPos;
    }

    public void HidePrompt()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOutThenDisable());
    }

    private Sprite GetSpriteForKey(KeyCode key)
    {
        foreach (var pair in keySprites)
            if (pair.key == key) return pair.sprite;

        return defaultSprite;
    }

    private IEnumerator FadeTo(float target)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, target))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = target;
    }

    private IEnumerator FadeOutThenDisable()
    {
        yield return FadeTo(0f);
        promptImage.gameObject.SetActive(false);
    }
}