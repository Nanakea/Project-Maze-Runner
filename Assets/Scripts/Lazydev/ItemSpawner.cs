using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
using System;

public class ItemSpawner : MonoBehaviour
{
    private void Awake()
    {
        BattleEvents.OnEnemyDie += SpawnOnEnemyDeath;
        BattleEvents.OnSpawnItemAtPostion += SpawnItem;
    }

    private void OnDestroy()
    {
        BattleEvents.OnEnemyDie -= SpawnOnEnemyDeath;
        BattleEvents.OnSpawnItemAtPostion -= SpawnItem;
    }

    private void SpawnItem(ItemSO item, Vector3 position)
    {
        GameObject newItem = Instantiate(item.prefab, position, Quaternion.identity);
        newItem.GetComponent<ItemObject>().data = item;
    }

    private void SpawnOnEnemyDeath(List<ItemSO> items, Vector3 deathPosition)
    {
        if (items.Count > 0)
        {
            int n = items.Count;

            for (int i = 0; i < n; i++)
            {
                Vector3 position = deathPosition + (Vector3)UnityEngine.Random.insideUnitSphere * 4;
                position.y = deathPosition.y;
                GameObject newItem = Instantiate(items[i].prefab, position, Quaternion.identity);
                newItem.GetComponent<ItemObject>().data = items[i];
            }

        }
    }

}
