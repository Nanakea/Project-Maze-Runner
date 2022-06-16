using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class BattleEvents : MonoBehaviour
    {
        public delegate void OnPlayerAttackHandler(float damage, StateManager states, bool isPlayersBattle);
        public static OnPlayerAttackHandler OnPlayerAttack;
        public static void RaiseOnPlayerAttack(float d, StateManager states, bool isPlayersBattle)
        {
            OnPlayerAttack?.Invoke(d, states, isPlayersBattle);
        }

        public delegate void OnEnemyAttackHandler(float damage, StateManager states);
        public static OnEnemyAttackHandler OnEnemyAttack;
        public static void RaiseOnEnemyAttack(float damage, StateManager states)
        {
            OnEnemyAttack?.Invoke(damage, states);
        }

        public delegate void OnEnemyDieHandler(List<ItemSO> items, Vector3 deathPosition);
        public static OnEnemyDieHandler OnEnemyDie;
        public static void RaiseOnEnemyDie(List<ItemSO> items, Vector3 deathPosition)
        {
            OnEnemyDie?.Invoke(items, deathPosition);
        }

        public delegate void SpawnItemAtPositionHandler(ItemSO item, Vector3 position);
        public static SpawnItemAtPositionHandler OnSpawnItemAtPostion;
        public static void RaiseOnSpawnItemAtPosition(ItemSO item, Vector3 pos)
        {
            OnSpawnItemAtPostion?.Invoke(item, pos);
        }


        public delegate void OnUseHealHandler(StateManager state, float hp);
        public static OnUseHealHandler OnUseHeal;
        public static void RaiseOnUseHeal(StateManager state,float hp)
        {
            OnUseHeal?.Invoke(state, hp);
        }

        public delegate void OnUseMonsterEggHandler(StateManager state,GameObject monster);
        public static OnUseMonsterEggHandler OnUseMonsterEgg;
        public static void RaiseOnUseMonsterEgg(StateManager state, GameObject monster)
        {
            OnUseMonsterEgg?.Invoke(state, monster);
        }


        public delegate void OnBagItemsChangedHandler();
        public static OnBagItemsChangedHandler OnBagItemsChanged;
        public static void RaiseOnBagItemsChanged()
        {
            OnBagItemsChanged?.Invoke();
        }
    }
}