using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Spawn }
[CreateAssetMenu(menuName ="ItemData")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [Space]
    public Sprite itemIcon;
    [Space]
    public GameObject prefab;

    [Space]
    public ItemType type;

    public virtual void Use(SA.StateManager state)
    {

    }
}
