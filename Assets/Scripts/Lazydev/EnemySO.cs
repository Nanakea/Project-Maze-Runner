using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")]
public class EnemySO : ScriptableObject
{
    public GameObject gfxPrefab;
    public float maxHealth;
    public float damage;

    public List<ItemSO> itemsSpawnedOnDeath = new List<ItemSO>();
}
