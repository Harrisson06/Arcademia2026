using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button defaultButton;
    [SerializeField] private AudioClip clip;
    private AudioSource source;
    void Start()
    {
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.volume = 0f;
        source.Play();
        StartCoroutine(FadeIn(1.5f)); // fade in over 1.5 seconds
    }

    public IEnumerator FadeIn(float duration, float targetVolume = 1f)
    {
        float timer = 0f;
        while (timer < duration)
        {
            source.volume = Mathf.Lerp(0f, targetVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        source.volume = targetVolume;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SafeRoom");
    }

    public void OptionsMenu()
    {

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
