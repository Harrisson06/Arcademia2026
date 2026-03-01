using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private Vitals vitals;
    private Movement movement;
    private Dictionary<ItemData.Debuffs, System.Action> debuffActions;

    private void Start()
    {
        vitals = GetComponent<Vitals>();
        movement = GetComponent<Movement>();

        debuffActions = new Dictionary<ItemData.Debuffs, System.Action>
        {
            { ItemData.Debuffs.Slow,            () => ChangeMaxSpeed(-2f) },
            { ItemData.Debuffs.LessHealth,      () => ChangeMaxHealth(-20) },
            { ItemData.Debuffs.IncreasedHunger, () => Debug.Log("Hunger increased") },
        };
    }

    

    public void ApplyDebuff(ItemData.Debuffs debuff)
    {
        if (debuff == ItemData.Debuffs.None) return;

        if (debuffActions.TryGetValue(debuff, out System.Action action))
            action.Invoke();
    }

    private void ChangeMaxSpeed(float amount) {
        movement.maxSpeed += amount;
        movement.dashForce += amount * 2;
        movement.SyncToManager();
    }
    private void ChangeMaxHealth(int amount)
    {
        vitals.maxHealth += amount;
    }
}