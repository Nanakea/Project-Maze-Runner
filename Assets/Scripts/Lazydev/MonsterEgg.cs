using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(menuName = "Egg")]
public class MonsterEgg : ItemSO
{
    public GameObject monster;

    public override void Use(StateManager player)
    {
        BattleEvents.RaiseOnUseMonsterEgg(player, monster);
        Debug.Log("Called the Use Method to spawn " + monster.name);
    }
}

    
