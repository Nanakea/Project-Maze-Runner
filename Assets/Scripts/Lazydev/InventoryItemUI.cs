using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SA;

public class InventoryItemUI : MonoBehaviour
{
    public ItemSO data;
    public Image itemIcon;

    public Text countText;

    public GameObject useButton;

    public Button iconBtn;
    

    [Space(50)]

    public PlayerBag bag;

    public StateManager relatedPlayerState;

    void Awake()
    {
        BattleEvents.OnBagItemsChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        BattleEvents.OnBagItemsChanged -= UpdateUI;
    }

    void Start()
    {
        UpdateUI();
        useButton.SetActive(false);
    }


    //You wanna call this every time you do something with the inventory
    void UpdateUI()
    {
        itemIcon.sprite = data.itemIcon;
        int n = bag.ItemCount(data);
        countText.text = n.ToString();

        if(n < 1)
        {
            iconBtn.interactable = false;
        }
        else
        {
            iconBtn.interactable = true;
        }
    }

    public void ToggleUse()
    {
        useButton.SetActive(!useButton.activeSelf);
    }

    public void Use()
    {
        data.Use(relatedPlayerState);
        bag.RemoveFromBag(data);
        UpdateUI();
        useButton.SetActive(false);
    }

}
