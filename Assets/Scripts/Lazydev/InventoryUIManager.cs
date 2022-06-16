using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryUI;

    public StateManager relatedPlayerState;

    void Awake()
    {
        //Create those events and subscribe to them here
    }

    private void OnDestroy()
    {
        //unsubscribe to all events here
    }

    public void UIToggle()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    //call this on the event "OnEnterBattleMode"


    //public delegate void OnEnterBattleModeHandler(StateManager state);
    //public static OnenterBattleModeHandler;
    //public static void RaiseOnEnterBattleMode(StateManager state){
    //  OnEnterBattleMode?.Invoke(state);
    //}


    //check if the state parameter is the same as it's related player
    void HideUI(StateManager state)
    {
        if(relatedPlayerState == state)
        {
            inventoryUI.SetActive(false);
        }
    }
    
    //call this on the event "OnExitBattleMode"
    //check if the state parameter is the same as it's related player
    void ShowUI(StateManager state)
    {
        if (relatedPlayerState == state)
        {
            inventoryUI.SetActive(true);
        }
    }
}
