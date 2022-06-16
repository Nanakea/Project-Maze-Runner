using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class ItemCombatCommands : CombatCommand
    {
        private void Start()
        {
            commandsText = GetComponent<Text>();
        }

        public override void Execute(StateManager states)
        {
            CommandExecutionHandler commandHandler;
            TryGetComponent<CommandExecutionHandler>(out commandHandler);
            if(commandHandler!= null)
            {
                commandHandler.inventory.UIToggle();
            }
        }
    }
}