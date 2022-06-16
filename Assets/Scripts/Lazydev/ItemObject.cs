using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemSO data;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBag bag = other.GetComponent<PlayerBag>();
            if (bag != null)
            {
                Debug.Log(data.name);
                bag.AddToBag(data);
                gameObject.SetActive(false);
            }
        }
    }
}
