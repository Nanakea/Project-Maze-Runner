using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

public class PlayerBag : MonoBehaviour
{

    public List<ItemSO> itemsInBag = new List<ItemSO>();

    public bool Contains(ItemSO item)
    {
        return itemsInBag.Contains(item);
    }

    public void AddToBag(ItemSO newItem)
    {
        itemsInBag.Add(newItem);
        BattleEvents.RaiseOnBagItemsChanged();
    }

    public void RemoveFromBag(ItemSO usedItem)
    {
        if (Contains(usedItem))
        {
            itemsInBag.Remove(usedItem);
            BattleEvents.RaiseOnBagItemsChanged();
        }

    }

    public int ItemCount(ItemSO item)
    {
        int count = 0;

        for (int i = 0; i < itemsInBag.Count; i++)
        {
            if (itemsInBag[i] == item)
            {
                count++;
            }
        }

        return count;
    }
}
