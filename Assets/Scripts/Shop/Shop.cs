using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;
    private bool PlayerInRange;
    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ShopUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) PlayerInRange = true;
    }

    void OnTrigerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) PlayerInRange = false;
    }
}
