using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(menuName = "Potion")]
public class HealPotion : ItemSO
{
    public float healPoints = 10;

    public override void Use(StateManager player)
    {
        BattleEvents.RaiseOnUseHeal(player, healPoints);
        Debug.Log("Called the Use Method to Heal by " + healPoints); 
    }
}
