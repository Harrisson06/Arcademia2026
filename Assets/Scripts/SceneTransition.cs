using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
