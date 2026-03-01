using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public enum Debuffs
    {
        None = 0,
        Slow,
        LessHealth,
        IncreasedHunger
    }

    public string itemName;
    public AudioClip collectSound;
    public int sellValue;
    public Debuffs Debuff;
}
