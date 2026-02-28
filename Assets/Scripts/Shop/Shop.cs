using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;
    public bool PlayerInRange;
    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            print("Activated");
            ShopUI.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) PlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) PlayerInRange = false;
    }
    public void Close()
    {
        ShopUI.SetActive(false);
    }
}
