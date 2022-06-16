using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class RunAwayCombatCommands : CombatCommand
    {
        private void Start()
        {
            commandsText = GetComponent<Text>();
        }

        public override void Execute(StateManager states)
        {
            states.ResetToIdleState();
        }
    }
}
