using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public static List<ItemData> items = new();
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        text.text = items.Count.ToString();
    }
}
