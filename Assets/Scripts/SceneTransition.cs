using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTransition : MonoBehaviour
{
    public string sceneName;
    public Vector3 Location;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.transitionLoc = Location;
        }
        SceneManager.LoadScene(sceneName);
    }
}
